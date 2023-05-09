using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreDateTime;
using MoreDateTime.Exceptions;

namespace MoreDateTime.Tests.ExtendedDateTimeFormat
{
    [TestClass]
    public class MiscTests
    {
        [TestMethod]
        public void TestStatusAndPartStatusSync()
        {
            const string dateString = "X199";
            Should.Throw<ParseException>(() => ExtendedDateTime.Parse(dateString));

        }

        [TestMethod]
        public void TestStatusMonthInvalid()
        {
            const string dateString = "2199-13";
            Should.Throw<InvalidDateException>(() => ExtendedDateTime.Parse(dateString));
        }

        [TestMethod]
        public void TestStatusDayInvalid()
        {
            const string dateString = "2199-02-30";
            Should.Throw<InvalidDateException>(() => ExtendedDateTime.Parse(dateString));
        }

        [TestMethod]
        public void TestStatusDayInvalidLeap()
        {
            const string dateString = "2019-02-29";
            Should.Throw<InvalidDateException>(() => ExtendedDateTime.Parse(dateString));
        }

        [TestMethod]
        public void TestStatusTimeInvalid()
        {
            const string dateString = "2199-01-01T25:61:60";
            Should.Throw<InvalidTimeException>(() => ExtendedDateTime.Parse(dateString));
        }

        [TestMethod]
        public void TestStatusHourInvalid()
        {
            const string dateString = "2199-01-01T25:00:00";
            Should.Throw<InvalidTimeException>(() => ExtendedDateTime.Parse(dateString));
        }

        [TestMethod]
        public void TestStatusMinuteInvalid()
        {
            const string dateString = "2199-01-01T23:61:00";
            Should.Throw<InvalidTimeException>(() => ExtendedDateTime.Parse(dateString));
        }


        [TestMethod]
        public void TestStatusSecsInvalid()
        {
            const string dateString = "2199-01-01T23:00:60";
            Should.Throw<InvalidTimeException>(() => ExtendedDateTime.Parse(dateString));
        }

    }
}