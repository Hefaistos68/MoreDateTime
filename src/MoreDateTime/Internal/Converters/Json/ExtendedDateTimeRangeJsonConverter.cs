using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoreDateTime.Internal.Converters.Json
{
    /// <summary>
    /// The extended date time converter for JSON
    /// </summary>
    public class ExtendedDateTimeRangeJsonConverter : JsonConverter<ExtendedDateTimeRange>
    {
        /// <inheritdoc/>
        public override ExtendedDateTimeRange Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ExtendedDateTimeRange.Parse(reader.GetString() ?? string.Empty);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, ExtendedDateTimeRange value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}