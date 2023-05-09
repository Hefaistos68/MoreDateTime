using System.ComponentModel;
using MoreDateTime.Interfaces;
using MoreDateTime.Internal.Converters;
using MoreDateTime.Internal.Parsers;
using MoreDateTime.Internal.Serializers;

namespace MoreDateTime
{
    /// <summary>
    /// The extended date time range.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExtendedDateTimeRangeConverter))]
    public class ExtendedDateTimeRange : IExtendedDateTimeCollectionChild, IExtendedDateTimeIndependentType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimeRange"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public ExtendedDateTimeRange(ISingleExtendedDateTimeType? start, ISingleExtendedDateTimeType? end)
        {
            Start = start ?? ExtendedDateTime.MinValue;
            End = end ?? ExtendedDateTime.MaxValue;

            IsOpen = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimeRange"/> class.
        /// </summary>
        internal ExtendedDateTimeRange()
        {
            Start = ExtendedDateTime.MinValue;
            End = ExtendedDateTime.MaxValue;

            IsOpen = false;
        }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        public ISingleExtendedDateTimeType End { get; set; }

        /// <summary>
        /// Gets a value indicating whether End has a valid value
        /// </summary>
        public bool HasEnd => !End.Equals(ExtendedDateTime.MaxValue);

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public ISingleExtendedDateTimeType Start { get; set; }

        /// <summary>
        /// Gets a value indicating whether Start has a valid value
        /// </summary>
        public bool HasStart => !Start.Equals(ExtendedDateTime.MinValue);

        /// <summary>
        /// Gets a value indicating whether this pair is a range (both start and end are valid)
        /// </summary>
        public bool IsRange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is open. (indicated with "..")
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Earliests the.
        /// </summary>
        /// <returns>An ExtendedDateTime</returns>
        public ExtendedDateTime Earliest()
        {
            return Start.Earliest();
        }

        /// <summary>
        /// Latests the.
        /// </summary>
        /// <returns>An ExtendedDateTime</returns>
        public ExtendedDateTime Latest()
        {
            return End.Latest();
        }

        /// <summary>
        /// Tos the string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return ExtendedDateTimeRangeSerializer.Serialize(this);
        }

        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="rangeString">The range string.</param>
        /// <param name="range">The range.</param>
        /// <returns>An ExtendedDateTimeRange.</returns>
        internal static ExtendedDateTimeRange Parse(string rangeString, ExtendedDateTimeRange? range = null)
        {
            if (string.IsNullOrWhiteSpace(rangeString))
            {
                throw new ArgumentNullException(nameof(rangeString));
            }

            return ExtendedDateTimeRangeParser.Parse(rangeString, range);
        }
    }
}