using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using MoreDateTime;
using MoreDateTime.Interfaces;
using MoreDateTime.Internal.Converters;
using MoreDateTime.Internal.Parsers;
using MoreDateTime.Internal.Serializers;

namespace MoreDateTime
{
    /// <summary>
    /// The extended date time possibility collection.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExtendedDateTimePossibilityCollectionConverter))]
    public class ExtendedDateTimePossibilityCollection : Collection<IExtendedDateTimeCollectionChild>, ISingleExtendedDateTimeType, ISerializable, IXmlSerializable
    {
        /// <inheritdoc/>
        public DateTimeValue Year { get; set; } = new();
        /// <inheritdoc/>
        public DateTimeValue Season { get; set; } = new();
        /// <inheritdoc/>
        public DateTimeValue Month { get; set; } = new();
        /// <inheritdoc/>
        public DateTimeValue Day { get; set; } = new();
        /// <inheritdoc/>
        public DateTimeValue Hour { get; set; } = new();
        /// <inheritdoc/>
        public DateTimeValue Minute { get; set; } = new();
        /// <inheritdoc/>
        public DateTimeValue Second { get; set; } = new();
        /// <inheritdoc/>
        public int TimeZoneOffset { get; set; } = new();

        /// <inheritdoc/>
        public bool IsOpen { get; internal set; }

        /// <inheritdoc/>
        public bool IsUnknown { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimePossibilityCollection"/> class.
        /// </summary>
        public ExtendedDateTimePossibilityCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimePossibilityCollection"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ExtendedDateTimePossibilityCollection(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Parse((string)(info.GetValue("edtpcStr", typeof(string)) ?? ""), this);
        }

        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="possibilityCollectionString">The possibility collection string.</param>
        /// <returns>An ExtendedDateTimePossibilityCollection.</returns>
        public static ExtendedDateTimePossibilityCollection Parse(string possibilityCollectionString)
        {
            if (string.IsNullOrWhiteSpace(possibilityCollectionString))
            {
                throw new ArgumentNullException(nameof(possibilityCollectionString));
            }

            return ExtendedDateTimePossibilityCollectionParser.Parse(possibilityCollectionString);
        }

        /// <summary>
        /// Earliests the.
        /// </summary>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime Earliest()
        {
            var candidates = new List<ExtendedDateTime>();

            foreach (var item in Items)
            {
                candidates.Add(item.Earliest());
            }

            candidates.Sort();

            return candidates[0];
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("edtpcStr", ToString());
        }

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <returns>A XmlSchema.</returns>
        public XmlSchema? GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Gets the span.
        /// </summary>
        /// <returns>A TimeSpan.</returns>
        public TimeSpan GetSpan()
        {
            return Latest() - Earliest();
        }

        /// <summary>
        /// Latests the.
        /// </summary>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime Latest()
        {
            var candidates = new List<ExtendedDateTime>();

            foreach (var item in Items)
            {
                candidates.Add(item.Latest());
            }

            candidates.Sort();

            return candidates[candidates.Count - 1];
        }

        /// <summary>
        /// Reads the xml.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void ReadXml(XmlReader reader)
        {
            Parse(reader.ReadString(), this);
        }

        /// <summary>
        /// Tos the string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return ExtendedDateTimePossibilityCollectionSerializer.Serialize(this);
        }

        /// <summary>
        /// Writes the xml.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(ToString());
        }

        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="possibilityCollectionString">The possibility collection string.</param>
        /// <param name="possibilityCollection">The possibility collection.</param>
        /// <returns>An ExtendedDateTimePossibilityCollection.</returns>
        internal static ExtendedDateTimePossibilityCollection? Parse(string possibilityCollectionString, ExtendedDateTimePossibilityCollection? possibilityCollection = null)
        {
            return string.IsNullOrWhiteSpace(possibilityCollectionString)
                ? null
                : ExtendedDateTimePossibilityCollectionParser.Parse(possibilityCollectionString, possibilityCollection);
        }
    }
}