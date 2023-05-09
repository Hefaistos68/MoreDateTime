using System.Text;
using MoreDateTime;

namespace MoreDateTime.Internal.Serializers
{
    /// <summary>
    /// The extended date time serializer.
    /// </summary>
    internal static class ExtendedDateTimeSerializer
    {
        /// <summary>
        /// Serializes the ExtendedDateTime.
        /// </summary>
        /// <param name="extendedDateTime">The extended date time.</param>
        /// <returns>A string.</returns>
        internal static string Serialize(ExtendedDateTime extendedDateTime)
        {
            if (extendedDateTime.IsUnknown)
            {
                return "";
            }

            if (extendedDateTime.IsOpen)
            {
                return "..";
            }

            string result = SerializeImplicit(extendedDateTime);

            if (extendedDateTime.IsOpenLeft)
            {
                result = ".." + result;
            }
            if (extendedDateTime.IsOpenRight)
            {
                result = result + "..";
            }

            return result;
        }

        /// <summary>
        /// Serializes the object for implicit representation
        /// </summary>
        /// <param name="edt">The edt.</param>
        /// <returns>A string.</returns>
        internal static string SerializeImplicit(ExtendedDateTime edt)
        {
            var sb = new StringBuilder();

            //////////////////////////////////////////////////////////////////////////
            //
            // insert the Year with precision and exponent
            // 
            if (edt.Precision >= ExtendedDateTimePrecision.Year && edt.Year?.IsNone == false)
            {
                // if PrefixQualifier is true, then we need to prefix the qualifier before anything else
                sb.AppendIfTrue(edt.Year.PrefixQualifier == true && !edt.Year.IsExact, FormatFlags(edt.Year));

                sb.AppendIfTrue(!edt.Year.IsRegularYear || edt.YearExponent.HasValue || edt.Year.IsLong, 'Y');

                // sb.Append(edt.YearExponent.HasValue ? edt.Year.ToString() : edt.Year.ToNumberString());
                sb.Append(edt.Year.ToNumberString());     // how we decide to make it 4 digits?

                // insert flags as suffix, if PrefixQualifier is false
                sb.AppendIfTrue(edt.Year.PrefixQualifier == false && !edt.Year.IsExact, FormatFlags(edt.Year));

                // append the suffix
                //sb.AppendIfTrue(edt.Year.IsExact && !edt.YearExponent.HasValue, "Y");

                // append the quantifiers
                sb.AppendIfTrue(edt.YearExponent.HasValue, "E", edt.YearExponent.ToString());
                sb.AppendIfTrue(edt.YearPrecision.HasValue, "S", edt.YearPrecision.ToString());
            }

            if (edt.Precision >= ExtendedDateTimePrecision.Season && edt.Season?.IsNone == false)
            {
                sb.Append('-');

                // either prefix or suffix the qualifiers
                sb.AppendIfTrue(edt.Season.QualifierFlags != edt.Year?.QualifierFlags && !edt.Season.IsExact, FormatFlags(edt.Season));
                sb.Append((int)edt.Season);
                sb.AppendIfTrue(edt.Season.QualifierFlags == edt.Year?.QualifierFlags && !edt.Season.IsExact, FormatFlags(edt.Season));
            }

            if (edt.Precision >= ExtendedDateTimePrecision.Month && edt.Month?.IsNone == false)
            {
                sb.AppendIfTrue(sb.Length > 0, '-');        // only if the string is not empty, we already have a part of the date

                // either prefix or suffix the qualifiers
                sb.AppendIfTrue(edt.Month.QualifierFlags != edt.Year?.QualifierFlags && !edt.Month.IsExact, FormatFlags(edt.Month));
                sb.Append(edt.Month.ToNumberString());
                sb.AppendIfTrue(edt.Month.QualifierFlags == edt.Year?.QualifierFlags && !edt.Month.IsExact, FormatFlags(edt.Month));
            }

            if (edt.Precision >= ExtendedDateTimePrecision.Day && edt.Day?.IsNone == false)
            {
                sb.AppendIfTrue(sb.Length > 0, '-');    // only if the string is not empty, we already have a part of the date

                // either prefix or suffix the qualifiers
                sb.AppendIfTrue(edt.Day.QualifierFlags != edt.Month?.QualifierFlags && !edt.Day.IsExact, FormatFlags(edt.Day));
                sb.Append(edt.Day.ToNumberString());
                sb.AppendIfTrue(edt.Day.QualifierFlags == edt.Month?.QualifierFlags && !edt.Day.IsExact, FormatFlags(edt.Day));
            }

            if (edt.Precision >= ExtendedDateTimePrecision.Hour && edt.Hour?.IsNone == false)
            {
                sb.AppendFormat("T{0:D2}", (int)edt.Hour);
            }

            if (edt.Precision >= ExtendedDateTimePrecision.Minute && edt.Minute?.IsNone == false)
            {
                sb.AppendFormat(":{0:D2}", (int)edt.Minute);
            }

            if (edt.Precision >= ExtendedDateTimePrecision.Second && edt.Second?.IsNone == false)
            {
                sb.AppendFormat(":{0:D2}", (int)edt.Second);
            }

            //////////////////////////////////////////////////////////////////////////
            //
            // insert timezone information
            // 
            if (edt.Precision >= ExtendedDateTimePrecision.Hour && edt.Hour?.IsNone == false)
            {
                if (edt.UtcOffset.HasValue)
                {
                    if (edt.UtcOffset.Value.Hours == 0 && edt.UtcOffset.Value.Minutes == 0)
                    {
                        sb.Append('Z');
                    }
                    else
                    {
                        if (edt.UtcOffset.Value.Hours < 0)
                        {
                            sb.Append('-');
                        }
                        else
                        {
                            sb.Append('+');
                        }

                        sb.AppendFormat("{0:D2}", Math.Abs(edt.UtcOffset.Value.Hours));
                    }

                    if (edt.UtcOffset.Value.Minutes != 0)
                    {
                        sb.AppendFormat(":{0:D2}", edt.UtcOffset.Value.Minutes);
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Formats the flags for a DateTimeValue.<br/>
        /// If the value is exact or unknown, null is returned.<br/>
        /// If the value is approximate, a tilde (~) is returned.<br/>
        /// If the value is uncertain, a question mark (?) is returned.<br/>
        /// If the value is approximate and uncertain, a percent sign (%) is returned.<br/>
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>A string.</returns>
        private static string? FormatFlags(DateTimeValue value)
        {
            if (value.IsExact || value.IsUnknown)
                return null;

            return value.IsApproximate ? value.IsUncertain ? "%" : "~" : value.IsUncertain ? "?" : null;
        }
    }
}