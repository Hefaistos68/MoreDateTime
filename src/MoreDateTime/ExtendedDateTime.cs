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
    /// The extended date time according to ISO 8601-2:2019 ExtendedDateTimeFormat Profile
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExtendedDateTimeConverter))]
    public class ExtendedDateTime : ISingleExtendedDateTimeType, IComparable, IComparable<ExtendedDateTime>, IEquatable<ExtendedDateTime>, ISerializable, IXmlSerializable, ICloneable
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static readonly ExtendedDateTime MaxValue = new ExtendedDateTime(9999, 12, 31, 23, 59, 59, 14);
        public static readonly ExtendedDateTime MinValue = new ExtendedDateTime(-9999, 1, 1, 0, 0, 0, -12);
        public static readonly ExtendedDateTime Open = new ExtendedDateTime { _isOpen = true };
        public static readonly ExtendedDateTime Unknown = new ExtendedDateTime { _isUnknown = true };
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable IDE1006 // Naming Styles
        private static ExtendedDateTimeComparer _comparer = null!;
        private DateTimeValue _year = new();
        private DateTimeValue _season = new();
        private DateTimeValue _month = new();
        private DateTimeValue _day = new();
        private DateTimeValue _hour = new();
        private DateTimeValue _minute = new();
        private DateTimeValue _second = new();

        private TimeSpan? _utcOffset;

        private bool _isLongYear;
        private bool _isOpen;
        private bool _isOpenLeft;
        private bool _isOpenRight;
        private bool _isUnknown;
        private bool _isExplicit;
        private ExtendedDateTimePrecision _precision;
        private int? _yearExponent;
        private int? _yearPrecision;
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTime"/> class.
        /// </summary>
        internal ExtendedDateTime()
        {
            _utcOffset = null;

            _year = new DateTimeValue();
            _month = new DateTimeValue();
            _day = new DateTimeValue();
            _hour = new DateTimeValue();
            _minute = new DateTimeValue();
            _second = new DateTimeValue();

            _precision = ExtendedDateTimePrecision.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTime"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="utcOffset">The utc offset.</param>
        public ExtendedDateTime(int year, int month = 0, int day = 0, int hour = -1, int minute = -1, int second = -1, TimeSpan? utcOffset = null) : this()
        {
            Init(year, month, day, hour, minute, second, utcOffset);
        }

        /// <summary>
        /// Initializes the instance from the data given
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="utcOffset">The utc offset.</param>
        private void Init(int year, int month = 0, int day = 0, int hour = -1, int minute = -1, int second = -1, TimeSpan? utcOffset = null)
        {
            _year = year;
            _precision = ExtendedDateTimePrecision.Year;

            if (month != -1)
            {
                if (month < 1 || month > 12)
                {
                    if (month >= (int)MoreDateTime.Season.First && month <= (int)MoreDateTime.Season.Last)
                    {
                        _season = month;
                        _precision = ExtendedDateTimePrecision.Season;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(month), month, $"The argument \"{nameof(month)}\" must be a value from 1 to 12 or if a season is intended, a value from {MoreDateTime.Season.First} to {MoreDateTime.Season.Last}");
                    }
                }
                else
                {
                    _month = month;
                    _precision = ExtendedDateTimePrecision.Month;
                }

                if (day != -1)
                {
                    if (day < 1 || day > ExtendedDateTimeCalculator.DaysInMonth(year, month))
                    {
                        throw new ArgumentOutOfRangeException(nameof(day), day, $"The argument \"{nameof(day)}\" must be a value from 1 to {ExtendedDateTimeCalculator.DaysInMonth(year, month)}");
                    }

                    _day = day;
                    _precision = ExtendedDateTimePrecision.Day;

                    if (hour != -1)
                    {
                        if (hour < 0 || hour > 23)
                        {
                            throw new ArgumentOutOfRangeException(nameof(hour), hour, $"The argument \"{nameof(hour)}\" must be a value from 0 to 23");
                        }

                        _hour = hour;
                        _precision = ExtendedDateTimePrecision.Hour;

                        if (minute != -1)
                        {
                            if (minute < 0 || minute > 59)
                            {
                                throw new ArgumentOutOfRangeException(nameof(minute), minute, $"The argument \"{nameof(minute)}\" must be a value from 0 to 59");
                            }

                            _minute = minute;
                            _precision = ExtendedDateTimePrecision.Minute;

                            if (second != -1)
                            {
                                if (second < 0 || second > 59)
                                {
                                    throw new ArgumentOutOfRangeException(nameof(second), second, $"The argument \"{nameof(second)}\" must be a value from 0 to 59");
                                }

                                _second = second;
                                _precision = ExtendedDateTimePrecision.Second;
                            }
                        }

                        // no need for UTC offset if the time is not specified
                        _utcOffset = utcOffset;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTime"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="utcHourOffset">The utc hour offset.</param>
        /// <param name="utcMinuteOffset">The utc minute offset.</param>
        public ExtendedDateTime(int year, int month = 0, int day = 0, int hour = -1, int minute = -1, int second = -1, int utcHourOffset = 0, int utcMinuteOffset = 0) : this()
        {
            // TODO: fix this, year can be larger than 4 digits, must set IsLongYear
            if (year < -9999 || year > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, $"The argument \"{nameof(year)}\" must be a value from -9999 to 9999");
            }
            TimeSpan? utcOffset = default;

            if (utcHourOffset != 0 && utcMinuteOffset != 0)
            {
                utcOffset = new TimeSpan(utcHourOffset, utcMinuteOffset, 0);
            }

            Init(year, month, day, hour, minute, second, utcOffset);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTime"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ExtendedDateTime(SerializationInfo info, StreamingContext context) : this()
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Parse((string)(info.GetValue("edtStr", typeof(string)) ?? ""), this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDateTime"/> class from a ExtendedDateTimeFormat string
        /// </summary>
        /// <param name="info">The ExtendedDateTimeFormat string</param>
        public ExtendedDateTime(string info) : this()
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Parse(info, this);
        }

        /// <summary>
        /// Gets the comparer.
        /// </summary>
        public static ExtendedDateTimeComparer Comparer
        {
            get
            {
                _comparer ??= new ExtendedDateTimeComparer();

                return _comparer;
            }
        }

        /// <summary>
        /// Gets the now.
        /// </summary>
        public static ExtendedDateTime Now
        {
            get
            {
                return DateTimeOffset.Now.ToExtendedDateTime();
            }
        }

        /// <summary>
        /// Gets the day.
        /// </summary>
        public DateTimeValue Day
        {
            get
            {
                return _isOpen ? DateTimeValue.Empty : _day;
            }

            internal set
            {
                _day = value;
            }
        }

        /// <summary>
        /// Gets the day of week.
        /// </summary>
        public DayOfWeek DayOfWeek
        {
            get
            {
                return Precision < ExtendedDateTimePrecision.Day
                    ? throw new InvalidOperationException("Day of the Week can only be calculated when the precision is of the day or greater.")
                    : ExtendedDateTimeCalculator.DayOfWeek(this);
            }
        }

        /// <summary>
        /// Gets the hour.
        /// </summary>
        public DateTimeValue Hour
        {
            get { return _isOpen ? DateTimeValue.Empty : _hour; }
            internal set { _hour = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the year is long
        /// </summary>
        public bool IsLongYear
        {
            get { return _isLongYear; }
            set { _isLongYear = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this value is open (means unlimited to either up or down)
        /// </summary>
        public bool IsOpen
        {
            get { return _isOpen; }
            internal set { _isOpen = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this value is open (means unlimited to either up or down)
        /// </summary>
        public bool IsOpenRight
        {
            get { return _isOpenRight; }
            internal set { _isOpenRight = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this value is open (means unlimited to either up or down)
        /// </summary>
        public bool IsOpenLeft
        {
            get { return _isOpenLeft; }
            internal set { _isOpenLeft = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this value unknown, similar to IsOpen
        /// </summary>
        public bool IsUnknown
        {
            get { return _isUnknown; }
            internal set { _isUnknown = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this value is explicit (with unit specifier)
        /// </summary>
        public bool IsExplicit
        {
            get { return _isExplicit; }
            internal set { _isExplicit = value; }
        }

        /// <summary>
        /// Gets the minute.
        /// </summary>
        public DateTimeValue Minute
        {
            get { return _isOpen ? DateTimeValue.Empty : _minute; }
            internal set { _minute = value; }
        }

        /// <summary>
        /// Gets the month.
        /// </summary>
        public DateTimeValue Month
        {
            get { return _isOpen ? DateTimeValue.Empty : _month; }
            internal set { _month = value; }
        }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        public ExtendedDateTimePrecision Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        /// <summary>
        /// Gets the season.
        /// </summary>
        public DateTimeValue Season
        { get { return _season; } set { _season = value; } }

        /// <summary>
        /// Gets the second.
        /// </summary>
        public DateTimeValue Second
        {
            get { return _isOpen ? DateTimeValue.Empty : _second; }
            internal set { _second = value; }
        }

        /// <summary>
        /// Gets the utc offset.
        /// </summary>
        public TimeSpan? UtcOffset
        {
            get { return _utcOffset; }
            set { _utcOffset = value; }
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        public DateTimeValue Year
        {
            get { return _isOpen ? DateTimeValue.Empty : _year; }
            internal set { _year = value; }
        }

        /// <summary>
        /// Gets the year exponent.
        /// </summary>
        public int? YearExponent
        {
            get { return _yearExponent; }
            set { _yearExponent = value; }
        }

        /// <summary>
        /// Gets the year precision, the significant digits of the year.
        /// </summary>
        public int? YearPrecision
        {
            get { return _yearPrecision; }
            set { _yearPrecision = value; }
        }

        /// <inheritdoc/>
        public int TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets a value indicating whether all the members are unknown.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return _season.IsNone
                    && _year.IsNone && _month.IsNone && _day.IsNone
                    && _hour.IsNone && _minute.IsNone && _second.IsNone;
            }
        }

        /// <summary>
        /// Creates a ExtendedDateTime with long year.
        /// </summary>
        /// <param name="year">The year</param>
        /// <returns>An ExtendedDateTime.</returns>
        public static ExtendedDateTime FromLongYear(int year)
        {
            return new ExtendedDateTime { _year = year, _isLongYear = true };
        }

        /// <summary>
        /// Creates a ExtendedDateTime from a scientific notation.
        /// </summary>
        /// <param name="significand">The significand</param>
        /// <param name="exponent">The exponent</param>
        /// <param name="precision">The precision</param>
        /// <returns>An ExtendedDateTime.</returns>
        public static ExtendedDateTime FromScientificNotation(int significand, int? exponent = null, int? precision = null)
        {
            if (significand == 0)
            {
                throw new ArgumentException("significand", "The significand must be nonzero.");
            }

            if (exponent < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(exponent), "An exponent must be positive.");
            }

            if (precision < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(precision), "A precision must be positive.");
            }

            return new ExtendedDateTime { _year = significand, _yearExponent = exponent, _yearPrecision = precision };
        }

        /// <summary>
        /// Subtraction operator for TimeSpan
        /// </summary>
        /// <param name="e"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ExtendedDateTime operator -(ExtendedDateTime e, TimeSpan t)
        {
            return ExtendedDateTimeCalculator.Subtract(e, t);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        /// <param name="e2"></param>
        /// <param name="e1"></param>
        /// <returns></returns>
        public static TimeSpan operator -(ExtendedDateTime e2, ExtendedDateTime e1)
        {
            return ExtendedDateTimeCalculator.Subtract(e2, e1);
        }

        /// <summary>
        /// Comparison operator
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public static bool operator !=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) != 0;
        }

        /// <summary>
        /// Addition operator for adding TimeSpan
        /// </summary>
        /// <param name="e"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ExtendedDateTime operator +(ExtendedDateTime e, TimeSpan t)
        {
            return ExtendedDateTimeCalculator.Add(e, t);
        }

        /// <summary>
        /// Less operator
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public static bool operator <(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) < 0;
        }

        /// <summary>
        /// Less or equal operator
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public static bool operator <=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) <= 0;
        }

        /// <summary>
        /// Equal operator
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public static bool operator ==(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) == 0;
        }

        /// <summary>
        /// Greater operator
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public static bool operator >(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) > 0;
        }

        /// <summary>
        /// Greater or equal operator
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public static bool operator >=(ExtendedDateTime e1, ExtendedDateTime e2)
        {
            return Comparer.Compare(e1, e2) >= 0;
        }

        /// <summary>
        /// Parses a string in EDT format
        /// </summary>
        /// <param name="extendedDateTimeString">The extended date time string.</param>
        /// <returns>An ExtendedDateTime.</returns>
        public static ExtendedDateTime Parse(string extendedDateTimeString)
        {
            return string.IsNullOrWhiteSpace(extendedDateTimeString)
                ? new ExtendedDateTime()
                : ExtendedDateTimeParser.Parse(extendedDateTimeString);
        }

        /// <summary>
        /// Adds months
        /// </summary>
        /// <param name="count">The number of months to add</param>
        /// <param name="dayExceedsDaysInMonthStrategy">How days exceeding days in months should be handled</param>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime AddMonths(int count, DayExceedsDaysInMonthStrategy dayExceedsDaysInMonthStrategy = DayExceedsDaysInMonthStrategy.RoundDown)
        {
            return ExtendedDateTimeCalculator.AddMonths(this, count, dayExceedsDaysInMonthStrategy);
        }

        /// <summary>
        /// Adds years
        /// </summary>
        /// <param name="count">The number of years</param>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime AddYears(int count)
        {
            return ExtendedDateTimeCalculator.AddYears(this, count);
        }

        /// <summary>
        /// Clones the object
        /// </summary>
        /// <returns>An object.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Compares to another ExtendedDateTime object
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>An int.</returns>
        public int CompareTo(ExtendedDateTime? other)
        {
            return Comparer.Compare(this, other);
        }

        /// <summary>
        /// Compares the to.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An int.</returns>
        public int CompareTo(object? obj)
        {
            if (obj is null)
            {
                return 1;
            }

            return obj is ExtendedDateTime
                ? Comparer.Compare(this, (ExtendedDateTime)obj)
                : throw new ArgumentException("An extended datetime can only be compared with another extended datetime.");
        }

        /// <summary>
        /// Gets the earliest possible date time
        /// </summary>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime Earliest()
        {
            return this;
        }

        /// <summary>
        /// Compares with an object for equality
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>A bool.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ExtendedDateTime ? Comparer.Compare(this, (ExtendedDateTime)obj) == 0 : false;
        }

        /// <summary>
        /// Compares with an ExtendedDateTime for equality
        /// </summary>
        /// <param name="other">The other object</param>
        /// <returns>A bool.</returns>
        public bool Equals(ExtendedDateTime? other)
        {
            return Comparer.Compare(this, other) == 0;
        }

        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns>An int.</returns>
        public override int GetHashCode()
        {
            return Year.GetHashCode() ^ Month.GetHashCode() ^ Day.GetHashCode();
            // return Year ^ (Month << 28) ^ (Day << 22) ^ (Hour << 14) ^ (Minute << 8) ^ (Second << 6) ^ UtcOffset.GetHashCode();
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("edtStr", ToString());
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
        /// Gets the latest possible date time
        /// </summary>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime Latest()
        {
            return this;
        }

        /// <summary>
        /// Parses from an XmlReader
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void ReadXml(XmlReader reader)
        {
            Parse(reader.ReadString(), this);
        }

        /// <summary>
        /// Subtracts months
        /// </summary>
        /// <param name="count">The number of months</param>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime SubtractMonths(int count)
        {
            return ExtendedDateTimeCalculator.SubtractMonths(this, count);
        }

        /// <summary>
        /// Subtracts years
        /// </summary>
        /// <param name="count">The number of years</param>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime SubtractYears(int count)
        {
            return ExtendedDateTimeCalculator.SubtractYears(this, count);
        }

        /// <summary>
        /// Rounds to the given precision
        /// </summary>
        /// <param name="precision">The precision</param>
        /// <param name="roundUp">If true, round up.</param>
        /// <returns>An ExtendedDateTime.</returns>
        public ExtendedDateTime ToRoundedPrecision(ExtendedDateTimePrecision precision, bool roundUp = false)
        {
            return ExtendedDateTimeCalculator.ToRoundedPrecision(this, precision, roundUp);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return ExtendedDateTimeSerializer.Serialize(this);
        }

        /// <summary>
        /// Writes a string representation to a XmlWriter
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(ToString());
        }

        /// <summary>
        /// Parses a string on EDT format
        /// </summary>
        /// <param name="extendedDateTimeString">The extended date time string.</param>
        /// <param name="container">The container.</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime Parse(string extendedDateTimeString, ExtendedDateTime container)
        {
            return string.IsNullOrWhiteSpace(extendedDateTimeString)
                ? new ExtendedDateTime()
                : ExtendedDateTimeParser.Parse(extendedDateTimeString, container);
        }

        /// <summary>
        /// Copies the flags and precision from the source
        /// </summary>
        /// <param name="other">The other datetime from where to copy</param>
        internal void CopyFlagsAndPrecision(ExtendedDateTime other)
        {
            Precision = other.Precision;
        }
    }
}