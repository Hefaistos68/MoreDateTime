using MoreDateTime.Exceptions;
using MoreDateTime.Interfaces;
using MoreDateTime.Internal.Parsers;

namespace MoreDateTime
{
    /// <summary>
    /// The extended date time format parser.
    /// </summary>
    public static class ExtendedDateTimeFormatParser
    {
		/// <summary>
		/// Parses a EDT formatted string<br/>
		/// "-"	a minus sign represented by the character "-" to indicate a negative value<br/>
		/// 
		/// "E"	the exponent designator, represented by the character "E", preceding the component
		///		which represents the exponential part of a time scale component value<br/>
		/// 
		/// "S"	the significant digit designator, represented by the character "S", preceding the component 
		///		which represents the number of significant digits of the time scale component value<br/>
		///		
		/// "X"	the unspecified digit, used within a date with unspecified part, represented by the character "X", 
		///		indicating that the time scale component value of the specific digit it replaces is unspecified
		///		
		/// "?"	qualification modifier, represented by the character "?", indicating that the time scale component value it applies to is uncertain<br/>
		/// 
		/// "~" qualification modifier, represented by the character indicating that the time scale component value it applies to is approximate<br/>
		/// 
		/// "%"	qualification modifier, represented by the character indicating that the time scale	component value it applies to is both uncertain and approximate<br/>
		/// 
		/// ".."x	indicating "on or after" value x if applied as a suffix to a time scale component<br/>
		/// 
		/// x".."	indicating "between x and y" (inclusive) when applied between two time scale components<br/>
		/// 
		/// </summary>
		/// <param name="EDTFtedString">The EDT format string.</param>
		/// <returns>An IExtendedDateTimeIndependentType.</returns>
		public static IExtendedDateTimeIndependentType Parse(string EDTFtedString)
        {
            if (EDTFtedString is null)
            {
                throw new ParseException("The input string cannot be empty.", "");
            }

            if (EDTFtedString == "")
            {
                return new ExtendedDateTime() { IsUnknown = true };
            }

            return EDTFtedString.First() == '{' ? ExtendedDateTimeCollectionParser.Parse(EDTFtedString)
                : EDTFtedString.First() == '[' ? ExtendedDateTimePossibilityCollectionParser.Parse(EDTFtedString)
                : ParseSecondary(EDTFtedString);
            // : EDTFtedString.Contains('X') ? UnspecifiedExtendedDateTimeParser.Parse(EDTFtedString)
        }

        /// <summary>
        /// Parses other ExtendedDateTimeFormat formats.
        /// </summary>
        /// <param name="EDTFtedString">The EDT formatted string.</param>
        /// <returns>An IExtendedDateTimeIndependentType.</returns>
        private static IExtendedDateTimeIndependentType ParseSecondary(string EDTFtedString)
        {
            if (EDTFtedString.Contains('/'))
            {
                return ExtendedDateTimeIntervalParser.Parse(EDTFtedString);
            }

            if (EDTFtedString.Contains("..") && !EDTFtedString.StartsWith("..") && !EDTFtedString.EndsWith(".."))
            {
                string intermediate = EDTFtedString;

                // return ExtendedDateTimeIntervalParser.Parse(intermediate, null);
                return ExtendedDateTimeRangeParser.Parse(intermediate, null);
            }

            if (EDTFtedString.Contains(',') && EDTFtedString.First() != '[')
            {
                return ExtendedDateTimeCollectionParser.Parse(EDTFtedString);
            }

            return EDTFtedString.Contains('X') ? UnspecifiedExtendedDateTimeParser.Parse(EDTFtedString) : ExtendedDateTimeParser.Parse(EDTFtedString);
        }
    }
}