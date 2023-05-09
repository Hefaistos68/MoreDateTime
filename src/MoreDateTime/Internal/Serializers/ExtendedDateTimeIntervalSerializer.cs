using System.Text;

namespace MoreDateTime.Internal.Serializers
{
    /// <summary>
    /// The extended date time interval serializer.
    /// </summary>
    internal static class ExtendedDateTimeIntervalSerializer
    {
        /// <summary>
        /// Serializes the.
        /// </summary>
        /// <param name="extendedDateTimeInterval">The extended date time interval.</param>
        /// <returns>A string.</returns>
        internal static string Serialize(ExtendedDateTimeInterval extendedDateTimeInterval)
        {
            var stringBuilder = new StringBuilder();

            if (extendedDateTimeInterval.Start == null)
            {
                return "Error: An interval must have both a start and end defined. Use \"ExtendedDateTime.Unknown\" if the date is unknown.";
            }

            stringBuilder.Append(extendedDateTimeInterval.Start.ToString());

            stringBuilder.Append('/');

            if (extendedDateTimeInterval.End == null)
            {
                return "Error: An interval must have both a start and end defined. Use \"ExtendedDateTime.Unknown\" if the date is unknown.";
            }

            stringBuilder.Append(extendedDateTimeInterval.End.ToString());

            return stringBuilder.ToString();
        }
    }
}
