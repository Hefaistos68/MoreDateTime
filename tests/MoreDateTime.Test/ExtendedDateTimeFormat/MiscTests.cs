namespace MoreDateTime.Tests.ExtendedDateTimeFormat
{
    using System;
    using System.ComponentModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MoreDateTime;
    using MoreDateTime.Exceptions;

    using Shouldly;

    /// <summary>
    /// Miscellaneous unit tests for ExtendedDateTimeFormat types and edge cases.
    /// </summary>
    [TestClass]
    public class MiscTests
    {
        /// <summary>
        /// Verifies that an invalid year prefix throws ParseException.
        /// </summary>
        [TestMethod]
        public void TestStatusAndPartStatusSync()
        {
            const string dateString = "X199";

            Should.Throw<ParseException>(() => ExtendedDateTime.Parse(dateString));
        }

        /// <summary>
        /// Verifies that an invalid month throws InvalidDateException.
        /// </summary>
        [TestMethod]
        public void TestStatusMonthInvalid()
        {
            const string dateString = "2199-13";

            Should.Throw<InvalidDateException>(() => ExtendedDateTime.Parse(dateString));
        }

        /// <summary>
        /// Verifies that an invalid day in February throws InvalidDateException.
        /// </summary>
        [TestMethod]
        public void TestStatusDayInvalid()
        {
            const string dateString = "2199-02-30";

            Should.Throw<InvalidDateException>(() => ExtendedDateTime.Parse(dateString));
        }

        /// <summary>
        /// Verifies that Feb 29 on a non-leap year throws InvalidDateException.
        /// </summary>
        [TestMethod]
        public void TestStatusDayInvalidLeap()
        {
            const string dateString = "2019-02-29";

            Should.Throw<InvalidDateException>(() => ExtendedDateTime.Parse(dateString));
        }

        /// <summary>
        /// Verifies that an invalid time throws InvalidTimeException.
        /// </summary>
        [TestMethod]
        public void TestStatusTimeInvalid()
        {
            const string dateString = "2199-01-01T25:61:60";

            Should.Throw<InvalidTimeException>(() => ExtendedDateTime.Parse(dateString));
        }

        /// <summary>
        /// Verifies that an invalid hour throws InvalidTimeException.
        /// </summary>
        [TestMethod]
        public void TestStatusHourInvalid()
        {
            const string dateString = "2199-01-01T25:00:00";

            Should.Throw<InvalidTimeException>(() => ExtendedDateTime.Parse(dateString));
        }

        /// <summary>
        /// Verifies that an invalid minute throws InvalidTimeException.
        /// </summary>
        [TestMethod]
        public void TestStatusMinuteInvalid()
        {
            const string dateString = "2199-01-01T23:61:00";

            Should.Throw<InvalidTimeException>(() => ExtendedDateTime.Parse(dateString));
        }

        /// <summary>
        /// Verifies that an invalid second throws InvalidTimeException.
        /// </summary>
        [TestMethod]
        public void TestStatusSecsInvalid()
        {
            const string dateString = "2199-01-01T23:00:60";

            Should.Throw<InvalidTimeException>(() => ExtendedDateTime.Parse(dateString));
        }

        /// <summary>
        /// Verifies constructor validation and factory methods on ExtendedDateTime.
        /// </summary>
        [TestMethod]
        public void TestExtendedDateTimeConstructorsAndFactories()
        {
            var longYear = ExtendedDateTime.FromLongYear(123456);
            var sci = ExtendedDateTime.FromScientificNotation(17, 3, 2);
            var empty = ExtendedDateTime.Parse(string.Empty);

            longYear.IsLongYear.ShouldBeTrue();
            longYear.Year.ShouldBe(123456);
            sci.Year.ShouldBe(17);
            sci.YearExponent.ShouldBe(3);
            sci.YearPrecision.ShouldBe(2);
            empty.IsEmpty.ShouldBeTrue();

            Should.Throw<ArgumentNullException>(() => new ExtendedDateTime(null!));
            Should.Throw<ArgumentOutOfRangeException>(() => new ExtendedDateTime(10000, 0, 0, -1, -1, -1, default(TimeSpan?)));
            Should.Throw<ArgumentOutOfRangeException>(() => new ExtendedDateTime(10000, 0, 0, -1, -1, -1, 0, 0));
            Should.Throw<ArgumentOutOfRangeException>(() => new ExtendedDateTime(2020, 13, 0, -1, -1, -1, default(TimeSpan?)));
            Should.Throw<ArgumentOutOfRangeException>(() => new ExtendedDateTime(2020, 2, 30, -1, -1, -1, default(TimeSpan?)));
            Should.Throw<ArgumentOutOfRangeException>(() => new ExtendedDateTime(2020, 1, 1, 25, -1, -1, default(TimeSpan?)));
            Should.Throw<ArgumentOutOfRangeException>(() => new ExtendedDateTime(2020, 1, 1, 10, 61, -1, default(TimeSpan?)));
            Should.Throw<ArgumentOutOfRangeException>(() => new ExtendedDateTime(2020, 1, 1, 10, 10, 61, default(TimeSpan?)));
            Should.Throw<ArgumentException>(() => ExtendedDateTime.FromScientificNotation(0, 2, 1));
            Should.Throw<ArgumentOutOfRangeException>(() => ExtendedDateTime.FromScientificNotation(10, 0, 1));
            Should.Throw<ArgumentOutOfRangeException>(() => ExtendedDateTime.FromScientificNotation(10, 2, 0));
        }

        /// <summary>
        /// Verifies comparison operators, equality, cloning, and properties on ExtendedDateTime.
        /// </summary>
        [TestMethod]
        public void TestExtendedDateTimeComparisonsAndMethods()
        {
            var dt1 = new ExtendedDateTime("2020-05-15T10:20:30Z");
            var dt2 = new ExtendedDateTime("2020-05-15T10:20:30Z");
            var dt3 = new ExtendedDateTime("2021-05-15T10:20:30Z");
            var yearOnly = new ExtendedDateTime("2020");

            var clone = (ExtendedDateTime)dt1.Clone();

            (dt1 == dt2).ShouldBeTrue();
            (dt1 != dt3).ShouldBeTrue();
            (dt1 < dt3).ShouldBeTrue();
            (dt1 <= dt2).ShouldBeTrue();
            (dt3 > dt1).ShouldBeTrue();
            (dt3 >= dt1).ShouldBeTrue();
            dt1.Equals((object)dt2).ShouldBeTrue();
            dt1.Equals((object)"not-an-edt").ShouldBeFalse();
            dt1.Equals(dt2).ShouldBeTrue();
            dt1.GetHashCode().ShouldBe(dt1.GetHashCode());
            dt1.CompareTo(dt2).ShouldBe(0);
            dt1.CompareTo((object)dt2).ShouldBe(0);
            dt1.CompareTo((object?)null).ShouldBe(1);
            Should.Throw<ArgumentException>(() => dt1.CompareTo("invalid"));
            clone.ShouldBe(dt1);
            dt1.Earliest().ShouldBe(dt1);
            dt1.Latest().ShouldBe(dt1);
            dt1.IsEmpty.ShouldBeFalse();
            dt1.GetSchema().ShouldBeNull();
            Should.Throw<InvalidOperationException>(() => _ = yearOnly.DayOfWeek);
        }

        /// <summary>
        /// Verifies ExtendedDateTimeInterval, ExtendedDateTimeRange, ExtendedDateTimeCollection, ExtendedDateTimePossibilityCollection, and UnspecifiedExtendedDateTime methods.
        /// </summary>
        [TestMethod]
        public void TestIntervalsCollectionsAndUnspecifiedTypes()
        {
            var interval = ExtendedDateTimeInterval.Parse("2020-01-01/2020-01-10");
            var collection = ExtendedDateTimeCollection.Parse("{2020-01-01,2020-01-05..2020-01-10}");
            var possibilities = ExtendedDateTimePossibilityCollection.Parse("[2020-01-01,2020-01-05..2020-01-10]");
            var unspec = UnspecifiedExtendedDateTime.Parse("2020-XX");
            var unspecConstructed = new UnspecifiedExtendedDateTime("2020", "01", "1X");

            interval.Earliest().ToString().ShouldBe("2020-01-01");
            interval.Latest().ToString().ShouldBe("2020-01-10");
            interval.Span().ShouldBe(TimeSpan.FromDays(9));
            interval.GetSchema().ShouldBeNull();

            collection.Earliest().ToString().ShouldBe("2020-01-01");
            collection.Latest().ToString().ShouldBe("2020-01-10");
            collection.GetSchema().ShouldBeNull();

            possibilities.Earliest().ToString().ShouldBe("2020-01-01");
            possibilities.Latest().ToString().ShouldBe("2020-01-10");
            possibilities.GetSpan().ShouldBe(TimeSpan.FromDays(9));
            possibilities.GetSchema().ShouldBeNull();

            unspec.Earliest().Year.ShouldBe(2020);
            unspec.Latest().Year.ShouldBe(2020);
            unspecConstructed.ToString().ShouldBe("2020-01-1X");
            unspec.GetSchema().ShouldBeNull();

            Should.Throw<ArgumentNullException>(() => ExtendedDateTimeInterval.Parse(""));
            Should.Throw<ArgumentNullException>(() => ExtendedDateTimeCollection.Parse(""));
            Should.Throw<ArgumentNullException>(() => ExtendedDateTimePossibilityCollection.Parse(""));
            Should.Throw<ArgumentNullException>(() => UnspecifiedExtendedDateTime.Parse(""));
            Should.Throw<ArgumentException>(() => new UnspecifiedExtendedDateTime("20"));
            Should.Throw<ArgumentException>(() => new UnspecifiedExtendedDateTime("2020", "1"));
            Should.Throw<ArgumentException>(() => new UnspecifiedExtendedDateTime("2020", "01", "1"));
        }

        /// <summary>
        /// Verifies TypeConverters for ExtendedDateTimeFormat types.
        /// </summary>
        [TestMethod]
        public void TestTypeConverters()
        {
            var edtConverter = TypeDescriptor.GetConverter(typeof(ExtendedDateTime));
            var intervalConverter = TypeDescriptor.GetConverter(typeof(ExtendedDateTimeInterval));
            var rangeConverter = TypeDescriptor.GetConverter(typeof(ExtendedDateTimeRange));
            var collectionConverter = TypeDescriptor.GetConverter(typeof(ExtendedDateTimeCollection));
            var possibilityConverter = TypeDescriptor.GetConverter(typeof(ExtendedDateTimePossibilityCollection));
            var unspecConverter = TypeDescriptor.GetConverter(typeof(UnspecifiedExtendedDateTime));

            edtConverter.CanConvertFrom(typeof(string)).ShouldBeTrue();
            edtConverter.CanConvertTo(typeof(string)).ShouldBeTrue();
            ((ExtendedDateTime)edtConverter.ConvertFromString("2020-05-15")!).ToString().ShouldBe("2020-05-15");

            intervalConverter.CanConvertFrom(typeof(string)).ShouldBeTrue();
            intervalConverter.CanConvertTo(typeof(string)).ShouldBeTrue();
            var interval = (ExtendedDateTimeInterval)intervalConverter.ConvertFromString("2020-01-01/2020-01-10")!;
            intervalConverter.ConvertToString(interval).ShouldBe("2020-01-01/2020-01-10");

            rangeConverter.CanConvertFrom(typeof(string)).ShouldBeTrue();
            rangeConverter.CanConvertTo(typeof(string)).ShouldBeTrue();
            var range = (ExtendedDateTimeRange)rangeConverter.ConvertFromString("2020-01-01..2020-01-10")!;
            rangeConverter.ConvertToString(range).ShouldBe("2020-01-01..2020-01-10");

            collectionConverter.CanConvertFrom(typeof(string)).ShouldBeTrue();
            collectionConverter.CanConvertTo(typeof(string)).ShouldBeTrue();
            var collection = (ExtendedDateTimeCollection)collectionConverter.ConvertFromString("{2020-01-01,2020-01-10}")!;
            collectionConverter.ConvertToString(collection).ShouldBe("{2020-01-01,2020-01-10}");

            possibilityConverter.CanConvertFrom(typeof(string)).ShouldBeTrue();
            possibilityConverter.CanConvertTo(typeof(string)).ShouldBeTrue();
            var possibility = (ExtendedDateTimePossibilityCollection)possibilityConverter.ConvertFromString("[2020-01-01,2020-01-10]")!;
            possibilityConverter.ConvertToString(possibility).ShouldBe("[2020-01-01,2020-01-10]");

            unspecConverter.CanConvertFrom(typeof(string)).ShouldBeTrue();
            unspecConverter.CanConvertTo(typeof(string)).ShouldBeTrue();
            var unspec = (UnspecifiedExtendedDateTime)unspecConverter.ConvertFromString("2020-01-XX")!;
            unspecConverter.ConvertToString(unspec).ShouldBe("2020-01-XX");
        }
    }
}
