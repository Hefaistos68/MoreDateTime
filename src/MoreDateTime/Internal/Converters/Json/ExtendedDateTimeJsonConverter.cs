using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoreDateTime.Internal.Converters.Json
{
    /// <summary>
    /// The extended date time converter for JSON
    /// </summary>
    public class ExtendedDateTimeJsonConverter : JsonConverter<ExtendedDateTime>
    {
        /// <inheritdoc/>
        public override ExtendedDateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ExtendedDateTime.Parse(reader.GetString() ?? string.Empty);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, ExtendedDateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}