using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoreDateTime.Internal.Converters.Json
{
    /// <summary>
    /// The extended date time converter for JSON
    /// </summary>
    public class ExtendedDateTimePossibilityCollectionJsonConverter : JsonConverter<ExtendedDateTimePossibilityCollection>
    {
        /// <inheritdoc/>
        public override ExtendedDateTimePossibilityCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ExtendedDateTimePossibilityCollection.Parse(reader.GetString() ?? string.Empty);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, ExtendedDateTimePossibilityCollection value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}