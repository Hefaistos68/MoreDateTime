using System.ComponentModel;
using System.Globalization;
using System.Text;

using MoreDateTime.Exceptions;

namespace MoreDateTime.Internal.Converters
{
    /// <summary>
    /// The unspecified extended date time converter.
    /// </summary>
    internal sealed class UnspecifiedExtendedDateTimeConverter : TypeConverter
    {
        /// <summary>
        /// Cans the convert from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>A bool.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Cans the convert to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>A bool.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns>An object.</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
        {
            if (value == null)
            {
                throw GetConvertFromException(value);
            }

            var source = value as string;

            return source != null ? UnspecifiedExtendedDateTime.Parse(source) : base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>An object.</returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType != null && value is UnspecifiedExtendedDateTime)
            {
                var instance = (UnspecifiedExtendedDateTime)value;

                if (destinationType == typeof(string))
                {
                    return instance.ToString();
                }
            }

#pragma warning disable CS8604 // Possible null reference argument.
            return base.ConvertTo(context, culture, value, destinationType);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        /// <summary>
        /// Converts the unspecified to a possibility collection.
        /// </summary>
        /// <param name="unspecifiedExtendedDateTime">The unspecified extended date time.</param>
        /// <returns>An ExtendedDateTimePossibilityCollection.</returns>
        internal static ExtendedDateTimePossibilityCollection ToPossibilityCollection(UnspecifiedExtendedDateTime unspecifiedExtendedDateTime)
        {
            var extendedDateTimePossibilityCollection = new ExtendedDateTimePossibilityCollection();
            var extendedDateTimeRange = new ExtendedDateTimeRange();
            var startExtendedDateTime = new ExtendedDateTime();
            var endExtendedDateTime = new ExtendedDateTime();

            extendedDateTimeRange.Start = startExtendedDateTime;
            extendedDateTimeRange.End = endExtendedDateTime;

            extendedDateTimePossibilityCollection.Add(extendedDateTimeRange);

            string year = unspecifiedExtendedDateTime.Year.ToString();
            string month = unspecifiedExtendedDateTime.Month.ToString();
            string day = unspecifiedExtendedDateTime.Day.ToString();

            if (year.Length != 4)                                 // Year
            {
                throw new ConversionException("An UnspecifiedExtendedDateTime year must be four characters long.");
            }

            StringBuilder yearStartBuffer = new();
            StringBuilder yearEndBuffer = new();

            for (int i = 0; i < 4; i++)
            {
                if (year[0] == 'X')
                {
                    if (i == 0)
                    {
                        yearStartBuffer.Append('-');
                    }

                    yearStartBuffer.Append('9');
                    yearEndBuffer.Append('9');
                }
                else if (year[i] == 'X')
                {
                    yearStartBuffer.Append('0');
                    yearEndBuffer.Append('9');
                }
                else
                {
                    yearStartBuffer.Append(year[i]);
                    yearEndBuffer.Append(year[i]);
                }
            }

            var yearStart = int.Parse(yearStartBuffer.ToString());
            var yearEnd = int.Parse(yearEndBuffer.ToString());

            if (string.IsNullOrEmpty(month))                                    // Month
            {
                startExtendedDateTime.Year = yearStart;
                endExtendedDateTime.Year = yearEnd;

                return extendedDateTimePossibilityCollection;
            }

            if (month.Length != 2)
            {
                throw new ConversionException("A month must be two characters long.");
            }

            StringBuilder monthStartBuffer = new();
            StringBuilder monthEndBuffer = new();

            var isFirstMonthDigitUnspecified = false;

            if (month[0] == 'X')
            {
                monthStartBuffer.Append('0');
                monthEndBuffer.Append('1');

                isFirstMonthDigitUnspecified = true;
            }
            else
            {
                monthStartBuffer.Append(month[0]);
                monthEndBuffer.Append(month[0]);
            }

            if (month[1] == 'X')
            {
                if (isFirstMonthDigitUnspecified)
                {
                    monthStartBuffer.Append('1');
                    monthEndBuffer.Append('2');
                }
                else
                {
                    var firstDigit = int.Parse(month[0].ToString());

                    if (firstDigit == 0)
                    {
                        monthStartBuffer.Append('1');
                        monthEndBuffer.Append('9');
                    }
                    else if (firstDigit == 1)
                    {
                        monthStartBuffer.Append('0');
                        monthEndBuffer.Append('2');
                    }
                    else
                    {
                        throw new ConversionException("A month must be between 1 and 12.");
                    }
                }
            }
            else
            {
                if (isFirstMonthDigitUnspecified)
                {
                    var secondDigit = int.Parse(month[1].ToString());

                    if (secondDigit > 2)                                                                // Month must be in the range of 01 to 09
                    {
                        monthEndBuffer[0] = '0';
                    }
                }

                monthStartBuffer.Append(month[1]);
                monthEndBuffer.Append(month[1]);
            }

            var monthStart = int.Parse(monthStartBuffer.ToString());
            var monthEnd = int.Parse(monthEndBuffer.ToString());

            if (string.IsNullOrEmpty(day))                                              // Day
            {
                startExtendedDateTime.Year = yearStart;
                startExtendedDateTime.Month = monthStart;
                endExtendedDateTime.Year = yearEnd;
                endExtendedDateTime.Month = monthEnd;

                return extendedDateTimePossibilityCollection;
            }

            if (day.Length != 2)
            {
                throw new ConversionException("A day must be two characters long.");
            }

            StringBuilder dayStartBuffer = new();
            StringBuilder dayEndBuffer = new();

            var daysInEndMonth = ExtendedDateTimeCalculator.DaysInMonth(yearEnd, monthEnd);

            var isFirstDayDigitUnspecified = false;

            if (day[0] == 'X')
            {
                dayStartBuffer.Append('0');
                dayEndBuffer.Append(daysInEndMonth.ToString()[0]);

                isFirstDayDigitUnspecified = true;
            }
            else
            {
                dayStartBuffer.Append(day[0]);
                dayEndBuffer.Append(day[0]);
            }

            if (day[1] == 'X')
            {
                if (isFirstDayDigitUnspecified)
                {
                    dayStartBuffer.Append('1');
                    dayEndBuffer.Append(daysInEndMonth.ToString()[1]);
                }
                else
                {
                    var firstDigit = int.Parse(day[0].ToString());

                    if (firstDigit == 0)                   // Day is 01 to 09
                    {
                        dayStartBuffer.Append('1');
                        dayEndBuffer.Append('9');
                    }
                    else if (firstDigit == 1)              // Day is 10 to 19
                    {
                        dayStartBuffer.Append('0');
                        dayEndBuffer.Append('9');
                    }
                    else if (firstDigit == 2)              // Day is 20 to 28 (if end month is February in a non-leap year) or 29
                    {
                        dayStartBuffer.Append('0');

                        if (daysInEndMonth == 28)
                        {
                            dayEndBuffer.Append('8');
                        }
                        else
                        {
                            dayEndBuffer.Append('9');
                        }
                    }
                    else if (firstDigit == 3)              // Day is 30 to 30 or 31 (depending on end month)
                    {
                        dayStartBuffer.Append('0');

                        if (daysInEndMonth == 30)
                        {
                            dayEndBuffer.Append('0');
                        }
                        else
                        {
                            dayEndBuffer.Append('1');
                        }
                    }
                    else
                    {
                        throw new ConversionException("A day must be between 1 and either 28, 29, 30, or 31 depending on the month.");
                    }
                }
            }
            else
            {
                if (isFirstDayDigitUnspecified)
                {
                    var secondDigit = int.Parse(day[1].ToString());

                    if (secondDigit > daysInEndMonth.ToString()[1])                                                // Decrement the first digit of the end day.
                    {
                        dayEndBuffer[0] = (int.Parse(dayEndBuffer[0].ToString()) - 1).ToString()[0];
                    }
                }

                dayStartBuffer.Append(day[1]);
                dayEndBuffer.Append(day[1]);
            }

            var dayStart = int.Parse(dayStartBuffer.ToString());
            var dayEnd = int.Parse(dayEndBuffer.ToString());

            var rangeBuffer = new List<ExtendedDateTime>();                            // Collects consecutive dates, which are then converted into an ExtendedDateTimeRange.

            extendedDateTimePossibilityCollection.Clear();

            for (var nyear = yearStart; nyear <= yearEnd; nyear++)
            {
                for (var nmonth = monthStart; nmonth <= monthEnd; nmonth++)
                {
                    for (var nday = dayStart; nday <= dayEnd; nday++)
                    {
                        if (nday > ExtendedDateTimeCalculator.DaysInMonth(nyear, nmonth))
                        {
                            if (rangeBuffer.Count == 1)
                            {
                                extendedDateTimePossibilityCollection.Add(new ExtendedDateTime(nyear, nmonth, nday, 0, 0, 0, null));

                                rangeBuffer.Clear();
                            }
                            else if (rangeBuffer.Count > 0)
                            {
                                extendedDateTimePossibilityCollection.Add(new ExtendedDateTimeRange(rangeBuffer.First(), rangeBuffer.Last()));

                                rangeBuffer.Clear();
                            }
                        }
                        else
                        {
                            rangeBuffer.Append(new ExtendedDateTime(nyear, nmonth, nday, 0, 0, 0, null));
                        }

                        if (nday == dayEnd)
                        {
                            if (rangeBuffer.Count == 1)
                            {
                                extendedDateTimePossibilityCollection.Add(new ExtendedDateTime(nyear, nmonth, nday, 0, 0, 0, null));

                                rangeBuffer.Clear();
                            }
                            else if (rangeBuffer.Count > 0)
                            {
                                extendedDateTimePossibilityCollection.Add(new ExtendedDateTimeRange(rangeBuffer.First(), rangeBuffer.Last()));

                                rangeBuffer.Clear();
                            }
                        }
                    }
                }
            }

            return extendedDateTimePossibilityCollection;
        }
    }
}