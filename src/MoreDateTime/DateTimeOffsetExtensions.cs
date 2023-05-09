namespace MoreDateTime
{
    /// <summary>
    /// The date time offset extensions.
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Tos the extended date time.
        /// </summary>
        /// <param name="dateTimeOffset">The dateTimeOffset to convert into an ExtendedDateTime object</param>
        /// <returns>An ExtendedDateTime.</returns>
        public static ExtendedDateTime ToExtendedDateTime(this DateTimeOffset dateTimeOffset)
        {
            return new ExtendedDateTime(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day,
                                        dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second,
                                        dateTimeOffset.Offset.Hours, dateTimeOffset.Offset.Minutes);
        }
    }
}
