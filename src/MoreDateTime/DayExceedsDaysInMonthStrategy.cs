namespace MoreDateTime
{
    /// <summary>
    /// Strategy for excess days in a month
    /// </summary>
    public enum DayExceedsDaysInMonthStrategy
    {
        /// <summary>Days will be rounded down to the next possible day (e.g. 31 to 30)</summary>
        RoundDown,
        /// <summary>Days will overflow into the next month (e.g. 31 to 01)</summary>
        Overflow
    }
}