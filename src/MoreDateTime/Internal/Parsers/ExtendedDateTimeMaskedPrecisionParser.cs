namespace MoreDateTime.Internal.Parsers
{
    /// <summary>
    /// The extended date time masked precision parser.
    /// </summary>
    internal static class ExtendedDateTimeMaskedPrecisionParser
    {
        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="extendedDateTimeMaskedPrecisionString">The extended date time masked precision string.</param>
        /// <returns>An ExtendedDateTimePossibilityCollection.</returns>
        internal static ExtendedDateTimePossibilityCollection Parse(string extendedDateTimeMaskedPrecisionString)
        {
            var extendedDateTimeRange = new ExtendedDateTimeRange();

            var start = new ExtendedDateTime();
            var end = new ExtendedDateTime();

            if (extendedDateTimeMaskedPrecisionString[2] == 'X')
            {
                start.Year = int.Parse(string.Format("{0}{1}00", extendedDateTimeMaskedPrecisionString[0], extendedDateTimeMaskedPrecisionString[1]));
                end.Year = int.Parse(string.Format("{0}{1}99", extendedDateTimeMaskedPrecisionString[0], extendedDateTimeMaskedPrecisionString[1]));
            }
            else
            {
                start.Year = int.Parse(string.Format("{0}{1}{2}0", extendedDateTimeMaskedPrecisionString[0], extendedDateTimeMaskedPrecisionString[1], extendedDateTimeMaskedPrecisionString[2]));
                end.Year = int.Parse(string.Format("{0}{1}{2}9", extendedDateTimeMaskedPrecisionString[0], extendedDateTimeMaskedPrecisionString[1], extendedDateTimeMaskedPrecisionString[2]));
            }

            extendedDateTimeRange.Start = start;
            extendedDateTimeRange.End = end;

            var possibilityCollection = new ExtendedDateTimePossibilityCollection();

            possibilityCollection.Add(extendedDateTimeRange);

            return possibilityCollection;
        }
    }
}