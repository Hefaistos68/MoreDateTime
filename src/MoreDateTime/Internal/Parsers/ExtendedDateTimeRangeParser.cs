using MoreDateTime.Exceptions;

namespace MoreDateTime.Internal.Parsers
{
    /// <summary>
    /// The extended date time range parser.
    /// </summary>
    internal static class ExtendedDateTimeRangeParser
    {
        /// <summary>
        /// Parses the given string expected to be an extended date time range formatted string
        /// </summary>
        /// <param name="extendedDateTimeRangeString">The extended date time range string.</param>
        /// <param name="extendedDateTimeRange">The extended date time range.</param>
        /// <returns>An ExtendedDateTimeRange.</returns>
        internal static ExtendedDateTimeRange Parse(string extendedDateTimeRangeString, ExtendedDateTimeRange? extendedDateTimeRange = null)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeRangeString))
            {
                throw new ArgumentException(nameof(extendedDateTimeRange));
            }

            var rangeParts = extendedDateTimeRangeString.Split(new string[] { "/" }, StringSplitOptions.None);   // An empty entry indicates a range with only one defined side.

            extendedDateTimeRange ??= new ExtendedDateTimeRange();

            bool bContainsTwoDots = extendedDateTimeRangeString.Contains("..");
            bool bStartsOrEndsWithTwoDots = extendedDateTimeRangeString.StartsWith("..") || extendedDateTimeRangeString.EndsWith("..");

            if (rangeParts.Length != 2)
            {
                if (!bStartsOrEndsWithTwoDots && !bContainsTwoDots)
                {
                    throw new ParseException("A range string must have exactly one \"/\".", extendedDateTimeRangeString);
                }

                rangeParts = extendedDateTimeRangeString.Split(new string[] { ".." }, StringSplitOptions.None);   // An empty entry indicates a range with only one defined side.

                extendedDateTimeRange.IsOpen = bContainsTwoDots ? bStartsOrEndsWithTwoDots ? false : true : false;
                extendedDateTimeRange.IsRange = bContainsTwoDots ? bStartsOrEndsWithTwoDots ? false : true : false;
            }
            else
            {
                extendedDateTimeRange.IsOpen = bContainsTwoDots ? bStartsOrEndsWithTwoDots ? false : true : false;
                extendedDateTimeRange.IsRange = true;
            }

            extendedDateTimeRange.Start = ExtendedDateTimeParser.Parse(rangeParts[0] == "" ? ".." : rangeParts[0]);
            extendedDateTimeRange.End = ExtendedDateTimeParser.Parse(rangeParts[1] == "" ? ".." : rangeParts[1]);

            return extendedDateTimeRange;
        }
    }
}