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
    /// The extended date time interval.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExtendedDateTimeIntervalConverter))]
    public class ExtendedDateTimeInterval : IExtendedDateTimeIndependentType, ISerializable, IXmlSerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimeInterval"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public ExtendedDateTimeInterval(ISingleExtendedDateTimeType start, ISingleExtendedDateTimeType end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimeInterval"/> class.
        /// </summary>
        internal ExtendedDateTimeInterval()
        {
            Start = ExtendedDateTime.MinValue;
            End = ExtendedDateTime.MaxValue;
        }

        /// <summary>
        /// Spans the.
        /// </summary>
        /// <returns>A TimeSpan.</returns>
        public TimeSpan Span()
        {
            return End.Latest() - Start.Earliest();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTimeInterval"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ExtendedDateTimeInterval(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Start = ExtendedDateTime.MinValue;
            End = ExtendedDateTime.MaxValue;

            Parse((string)(info.GetValue("edtiStr", typeof(string)) ?? ""), this);
        }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        public ISingleExtendedDateTimeType End { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public ISingleExtendedDateTimeType Start { get; set; }

        /// <summary>
        /// Gets a value indicating whether this pair is dotted interval (both start and end are valid and separated by ..)
        /// </summary>
        public bool IsDottedInterval { get; set; }

        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="extendedDateTimeIntervalString">The extended date time interval string.</param>
        /// <returns>An ExtendedDateTimeInterval.</returns>
        public static ExtendedDateTimeInterval Parse(string extendedDateTimeIntervalString)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeIntervalString))
            {
                throw new ArgumentNullException(nameof(extendedDateTimeIntervalString));
            }

            return ExtendedDateTimeIntervalParser.Parse(extendedDateTimeIntervalString);
        }

        /// <summary>
        /// Earliests the.
        /// </summary>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime Earliest()
        {
            return Start.Earliest();
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("edtiStr", ToString());
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
            return End.Latest();
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
            return ExtendedDateTimeIntervalSerializer.Serialize(this);
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
        /// <param name="extendedDateTimeIntervalString">The extended date time interval string.</param>
        /// <param name="extendedDateTimeInterval">The extended date time interval.</param>
        /// <returns>An ExtendedDateTimeInterval.</returns>
        internal static ExtendedDateTimeInterval Parse(string extendedDateTimeIntervalString, ExtendedDateTimeInterval? extendedDateTimeInterval = null)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeIntervalString))
            {
                throw new ArgumentNullException(nameof(extendedDateTimeIntervalString));
            }

            return ExtendedDateTimeIntervalParser.Parse(extendedDateTimeIntervalString, extendedDateTimeInterval);
        }
    }
}