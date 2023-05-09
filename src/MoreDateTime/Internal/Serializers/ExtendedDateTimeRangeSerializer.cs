using System.Text;

namespace MoreDateTime.Internal.Serializers
{
    /// <summary>
    /// The extended date time range serializer.
    /// </summary>
    internal static class ExtendedDateTimeRangeSerializer
    {
        /// <summary>
        /// Serializes the.
        /// </summary>
        /// <param name="extendedDateTimeRange">The extended date time range.</param>
        /// <returns>A string.</returns>
        internal static string Serialize(ExtendedDateTimeRange extendedDateTimeRange)
        {
            var stringBuilder = new StringBuilder();

            if (extendedDateTimeRange.HasStart)
            {
                stringBuilder.Append(extendedDateTimeRange.Start.ToString());
            }

            if (extendedDateTimeRange.IsRange && !extendedDateTimeRange.IsOpen)
            {
                stringBuilder.Append('/');
            }
            else if (extendedDateTimeRange.IsRange)
            {
                stringBuilder.Append("..");
            }

            if (extendedDateTimeRange.HasEnd)
            {
                stringBuilder.Append(extendedDateTimeRange.End.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}