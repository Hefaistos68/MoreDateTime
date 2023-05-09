using MoreDateTime;

namespace MoreDateTime.Interfaces
{
    /// <summary>
    /// The single extended date time type.
    /// </summary>
    public interface ISingleExtendedDateTimeType : IExtendedDateTimeCollectionChild, IExtendedDateTimeIndependentType
    {
        /// <summary>
        /// Gets the time zone offset.
        /// </summary>
        public int TimeZoneOffset { get; }

        /// <summary>
        /// Gets the year.
        /// </summary>
        public DateTimeValue Year { get; }

        /// <summary>
        /// Gets the Season
        /// </summary>
        public DateTimeValue Season { get; }

        /// <summary>
        /// Gets the month.
        /// </summary>
        public DateTimeValue Month { get; }

        /// <summary>
        /// Gets the day.
        /// </summary>
        public DateTimeValue Day { get; }

        /// <summary>
        /// Gets the hour.
        /// </summary>
        public DateTimeValue Hour { get; }

        /// <summary>
        /// Gets the minute.
        /// </summary>
        public DateTimeValue Minute { get; }

        /// <summary>
        /// Gets the second.
        /// </summary>
        public DateTimeValue Second { get; }

        /// <summary>
        /// Returns whether this instance is open, means it has defined start or end
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// Returns whether this instance is unknown, has not been defined at all, as in "/2000" where the left part is unknown
        /// </summary>
        bool IsUnknown { get; }
    }
}