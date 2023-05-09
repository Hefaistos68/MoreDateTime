using System.Text.RegularExpressions;

using MoreDateTime.Exceptions;

namespace MoreDateTime.Internal.Parsers
{
    /// <summary>
    /// The extended date time interval parser.
    /// </summary>
    internal static class ExtendedDateTimeIntervalParser
    {
        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="extendedDateTimeIntervalString">The extended date time interval string.</param>
        /// <param name="extendedDateTimeInterval">The extended date time interval.</param>
        /// <returns>An ExtendedDateTimeInterval.</returns>
        internal static ExtendedDateTimeInterval Parse(string extendedDateTimeIntervalString, ExtendedDateTimeInterval? extendedDateTimeInterval = null)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeIntervalString))
            {
                throw new ParseException("An interval string cannot be null, empty, or whitespace.", extendedDateTimeIntervalString);
            }

            string[]? intervalPartStrings = null;
            bool isRange = false;

            // lets find out if we have two intervals separatd by / or one by ..
            if (extendedDateTimeIntervalString.Contains("..") && !extendedDateTimeIntervalString.StartsWith("..") && !extendedDateTimeIntervalString.EndsWith(".."))
            {
                var match = new Regex("(.*\\d+)\\.\\.(\\d+.*)").Match(extendedDateTimeIntervalString);
                if (match.Success)
                {
                    if (match.Groups.Count == 3)
                    {
                        intervalPartStrings = new string[2] { match.Groups[1].Value, match.Groups[2].Value };
                        isRange = true;
                    }
                }
            }
            else
            {
                intervalPartStrings = extendedDateTimeIntervalString.Split(new char[] { '/' });
            }

            if (intervalPartStrings?.Length != 2)
            {
                throw new ParseException("An interval string must contain exactly one forward slash or a double dot between the units.", extendedDateTimeIntervalString);
            }

            var startString = intervalPartStrings[0];
            var endString = intervalPartStrings[1];

            if (startString.StartsWith('{'))
            {
                throw new ParseException("An interval cannot contain a collection.", startString);
            }

            if (endString.StartsWith('{'))
            {
                throw new ParseException("An interval cannot contain a collection.", startString);
            }

            extendedDateTimeInterval ??= new ExtendedDateTimeInterval();
            extendedDateTimeInterval.IsDottedInterval = isRange;

            extendedDateTimeInterval.Start = startString == "" ? ExtendedDateTime.Unknown
                                            : startString == ".." ? ExtendedDateTime.Open
                                            : startString[0] == '[' ? ExtendedDateTimePossibilityCollectionParser.Parse(startString)
                                            : startString.Contains('X') ? UnspecifiedExtendedDateTimeParser.Parse(startString)
                                            : ExtendedDateTimeParser.Parse(startString);

            extendedDateTimeInterval.End = endString == "" ? ExtendedDateTime.Unknown
                                            : endString == ".." ? ExtendedDateTime.Open
                                            : endString[0] == '[' ? ExtendedDateTimePossibilityCollectionParser.Parse(endString)
                                            : endString.Contains('X') ? UnspecifiedExtendedDateTimeParser.Parse(endString)
                                            : ExtendedDateTimeParser.Parse(endString);

            return extendedDateTimeInterval;
        }
    }
}