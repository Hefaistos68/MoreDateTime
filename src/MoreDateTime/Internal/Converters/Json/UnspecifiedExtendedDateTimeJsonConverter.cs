using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoreDateTime.Internal.Converters.Json
{
    /// <summary>
    /// The extended date time converter for JSON
    /// </summary>
    public class UnspecifiedExtendedDateTimeJsonConverter : JsonConverter<UnspecifiedExtendedDateTime>
    {
        /// <inheritdoc/>
        public override UnspecifiedExtendedDateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return UnspecifiedExtendedDateTime.Parse(reader.GetString() ?? string.Empty);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, UnspecifiedExtendedDateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}