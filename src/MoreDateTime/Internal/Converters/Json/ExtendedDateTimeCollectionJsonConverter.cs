using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoreDateTime.Internal.Converters.Json
{
    /// <summary>
    /// The extended date time collection converter for JSON
    /// </summary>
    public class ExtendedDateTimeCollectionJsonConverter : JsonConverter<ExtendedDateTimeCollection>
    {
        /// <inheritdoc/>
        public override ExtendedDateTimeCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ExtendedDateTimeCollection.Parse(reader.GetString() ?? string.Empty);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, ExtendedDateTimeCollection value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}