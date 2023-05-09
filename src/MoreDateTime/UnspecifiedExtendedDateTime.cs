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
    /// The unspecified extended date time.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(UnspecifiedExtendedDateTimeConverter))]
    public class UnspecifiedExtendedDateTime : ISingleExtendedDateTimeType, ISerializable, IXmlSerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnspecifiedExtendedDateTime"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        public UnspecifiedExtendedDateTime(string year, string month, string day) : this(year, month)
        {
            if (day.Length != 2)
            {
                throw new ArgumentException("The day must be two characters long.");
            }

            Day = (DateTimeValue)day;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnspecifiedExtendedDateTime"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        public UnspecifiedExtendedDateTime(string year, string month) : this(year)
        {
            if (month.Length != 2)
            {
                throw new ArgumentException("The month must be two characters long.");
            }

            Month = (DateTimeValue)month;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnspecifiedExtendedDateTime"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        public UnspecifiedExtendedDateTime(string year) : this()
        {
            if (year.Length != 4 && !year.StartsWith("-"))
            {
                throw new ArgumentException("The year must be four characters long except if the year is negative, in which case the year must be five characters long.");
            }

            Year = (DateTimeValue)year;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnspecifiedExtendedDateTime"/> class.
        /// </summary>
        internal UnspecifiedExtendedDateTime()
        {
            IsOpen = false;
            IsUnknown = false;

            Year = new DateTimeValue();
            Season = new DateTimeValue();
            Month = new DateTimeValue();
            Day = new DateTimeValue();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnspecifiedExtendedDateTime"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected UnspecifiedExtendedDateTime(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            IsOpen = false;
            IsUnknown = false;

            Year = new DateTimeValue();
            Season = new DateTimeValue();
            Month = new DateTimeValue();
            Day = new DateTimeValue();

            Parse((string)(info.GetValue("uedtStr", typeof(string)) ?? ""), this);
        }

        /// <inheritdoc/>
        public DateTimeValue Year { get; set; }
        /// <inheritdoc/>
        public DateTimeValue Season { get; set; }
        /// <inheritdoc/>
        public DateTimeValue Month { get; set; }
        /// <inheritdoc/>
        public DateTimeValue Day { get; set; }
        /// <inheritdoc/>
        public DateTimeValue Hour { get { return DateTimeValue.Empty; } }
        /// <inheritdoc/>
        public DateTimeValue Minute { get { return DateTimeValue.Empty; } }
        /// <inheritdoc/>
        public DateTimeValue Second { get { return DateTimeValue.Empty; } }
        /// <inheritdoc/>
        public int TimeZoneOffset { get; set; }
        /// <inheritdoc/>
        public bool IsOpen { get; internal set; }
        /// <inheritdoc/>
        public bool IsUnknown { get; internal set; }

        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="unspecifiedExtendedDateTimeString">The unspecified extended date time string.</param>
        /// <returns>An UnspecifiedExtendedDateTime.</returns>
        public static UnspecifiedExtendedDateTime Parse(string unspecifiedExtendedDateTimeString)
        {
            if (string.IsNullOrWhiteSpace(unspecifiedExtendedDateTimeString))
            {
                throw new ArgumentNullException(nameof(unspecifiedExtendedDateTimeString));
            }

            return UnspecifiedExtendedDateTimeParser.Parse(unspecifiedExtendedDateTimeString);
        }

        /// <summary>
        /// Earliests the.
        /// </summary>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime Earliest()
        {
            return ToPossibilityCollection().Earliest();
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("uedtStr", ToString());
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
            return ToPossibilityCollection().Latest();
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
        /// Tos the possibility collection.
        /// </summary>
        /// <returns>An ExtendedDateTimePossibilityCollection.</returns>
        public ExtendedDateTimePossibilityCollection ToPossibilityCollection()
        {
            return UnspecifiedExtendedDateTimeConverter.ToPossibilityCollection(this);
        }

        /// <summary>
        /// Tos the string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return UnspecifiedExtendedDateTimeSerializer.Serialize(this);
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
        /// <param name="unspecifiedExtendedDateTimeString">The unspecified extended date time string.</param>
        /// <param name="unspecifiedExtendedDateTime">The unspecified extended date time.</param>
        /// <returns>An UnspecifiedExtendedDateTime.</returns>
        internal static UnspecifiedExtendedDateTime Parse(string unspecifiedExtendedDateTimeString, UnspecifiedExtendedDateTime? unspecifiedExtendedDateTime = null)
        {
            if (string.IsNullOrWhiteSpace(unspecifiedExtendedDateTimeString))
            {
                throw new ArgumentNullException(nameof(unspecifiedExtendedDateTimeString));
            }

            return UnspecifiedExtendedDateTimeParser.Parse(unspecifiedExtendedDateTimeString, unspecifiedExtendedDateTime);
        }
    }
}