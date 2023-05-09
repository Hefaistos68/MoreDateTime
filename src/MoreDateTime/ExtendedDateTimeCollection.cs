using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using MoreDateTime.Interfaces;
using MoreDateTime.Internal.Converters;
using MoreDateTime.Internal.Parsers;
using MoreDateTime.Internal.Serializers;

namespace MoreDateTime
{
    /// <summary>
    /// The extended date time collection.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExtendedDateTimeCollectionConverter))]
    public class ExtendedDateTimeCollection : Collection<IExtendedDateTimeCollectionChild>, IExtendedDateTimeIndependentType, ISerializable, IXmlSerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimeCollection"/> class.
        /// </summary>
        public ExtendedDateTimeCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimeCollection"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ExtendedDateTimeCollection(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Parse((string)(info.GetValue("edtcStr", typeof(string)) ?? ""), this);
        }

        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="extendedDateTimeCollectionString">The extended date time collection string.</param>
        /// <returns>An ExtendedDateTimeCollection.</returns>
        public static ExtendedDateTimeCollection Parse(string extendedDateTimeCollectionString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeCollectionString))
            {
                throw new ArgumentNullException(nameof(extendedDateTimeCollectionString));
            }

            return ExtendedDateTimeCollectionParser.Parse(extendedDateTimeCollectionString);
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

            return candidates.First();
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("edtcStr", ToString());
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

            return candidates.Last();
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
        /// Gets or sets a value indicating whether this is a bracketed set. i.e. Should contain [ and ]
        /// </summary>
        public bool IsBracketedSet { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return ExtendedDateTimeCollectionSerializer.Serialize(this);
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
        /// <param name="extendedDateTimeCollectionString">The extended date time collection string.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>An ExtendedDateTimeCollection.</returns>
        internal static ExtendedDateTimeCollection? Parse(string extendedDateTimeCollectionString, ExtendedDateTimeCollection? collection = null)
        {
            return string.IsNullOrWhiteSpace(extendedDateTimeCollectionString)
                ? null
                : ExtendedDateTimeCollectionParser.Parse(extendedDateTimeCollectionString, collection);
        }
    }
}