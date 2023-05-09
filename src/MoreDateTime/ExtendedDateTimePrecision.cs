namespace MoreDateTime
{
    /// <summary>
    /// The extended date time precision, specifies the precision of the date time object, the higher the precision the more accurate the date time object is.<br/>
    /// For example: a precision of Day means there are no time components, a precision of Hour means there are no minutes or seconds, etc.
    /// </summary>
    public enum ExtendedDateTimePrecision
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        None = 0,
        Century,
        Decade,
        Year,
        Season,
        Month,
        Week,
        Day,
        Hour,
        Minute,
        Second
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}