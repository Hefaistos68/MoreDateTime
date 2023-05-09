using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoreDateTime.Internal.Converters.Json
{
    /// <summary>
    /// The extended date time converter for JSON
    /// </summary>
    public class ExtendedDateTimeIntervalJsonConverter : JsonConverter<ExtendedDateTimeInterval>
    {
        /// <inheritdoc/>
        public override ExtendedDateTimeInterval Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ExtendedDateTimeInterval.Parse(reader.GetString() ?? string.Empty);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, ExtendedDateTimeInterval value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}