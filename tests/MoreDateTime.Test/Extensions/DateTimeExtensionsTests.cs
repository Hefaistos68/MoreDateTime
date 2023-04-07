namespace MoreDateTime.Tests.Extensions
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;
	using MoreDateTime.Interfaces;

	using Nager.Date;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="DateTimeExtensions"/>.
	/// </summary>
	public partial class DateTimeExtensionsTests
	{
		// setup some test data taken from real calendar
		private readonly DateTime _startDate = new DateTime(2020, 05, 15); // Friday

		private readonly DateTime _midDate = new DateTime(2021, 02, 20);   // Saturday
		private readonly DateTime _endDate = new DateTime(2021, 05, 14);   // Friday

		private readonly DateTime _startDateFirstDay = new DateTime(2020, 05, 01);  // Friday
		private readonly DateTime _startDateLastDay = new DateTime(2020, 05, 31);       // Sunday

		private readonly DayOfWeek _startDateDayOfWeek = DayOfWeek.Friday;
		private readonly int _startDateWeekNumber = 20;
		private readonly int _totalDaysInStartDateToEndDate = 365;

		private readonly DateTime _startDateFirstMonday = new DateTime(2020, 05, 04);
		private readonly DateTime _startDateFirstTuesday = new DateTime(2020, 05, 05);
		private readonly DateTime _startDateFirstWednesday = new DateTime(2020, 05, 06);
		private readonly DateTime _startDateFirstThursday = new DateTime(2020, 05, 07);
		private readonly DateTime _startDateFirstFriday = new DateTime(2020, 05, 01);
		private readonly DateTime _startDateFirstSaturday = new DateTime(2020, 05, 02);
		private readonly DateTime _startDateFirstSunday = new DateTime(2020, 05, 03);

		private readonly DateTime _startDateLastMonday = new DateTime(2020, 05, 25);
		private readonly DateTime _startDateLastTuesday = new DateTime(2020, 05, 26);
		private readonly DateTime _startDateLastWednesday = new DateTime(2020, 05, 27);
		private readonly DateTime _startDateLastThursday = new DateTime(2020, 05, 28);
		private readonly DateTime _startDateLastFriday = new DateTime(2020, 05, 29);
		private readonly DateTime _startDateLastSaturday = new DateTime(2020, 05, 30);
		private readonly DateTime _startDateLastSunday = new DateTime(2020, 05, 31);

		private readonly double _holidaysInStartDateToEndDateNull = 0.0d;
		private readonly double _holidaysInStartDateToEndDateDefault = 4.0d;
		private readonly double _holidaysInStartDateToEndDateNagerDate = 13.0d;

		private readonly TimeZoneInfo _timeZoneTest = TimeZoneInfo.CreateCustomTimeZone("UnitTest", new TimeSpan(1, 0, 0), "UnitTest", "UnitTest");
		private readonly CultureInfo _cultureInfo = new CultureInfo("PT-PT");

		/// <summary>
		/// Initialize the test
		/// </summary>
		[TestInitialize()]
		public void SetupOnce()
		{
			DateSystem.LicenseKey = "LostTimeIsNeverFoundAgain";
		}


		/// <summary>
		/// Checks that the AsUtcDate method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_AsUtcDate()
		{
			// Arrange
			var time = _startDate;

			// Act
			var result = time.AsUtcDate(_cultureInfo, _timeZoneTest);

			// Assert
			result.Date.ShouldBe(_startDate.PreviousDay());
		}

		/// <summary>
		/// Checks that the AsUtcTime method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_AsUtcTime()
		{
			// Arrange
			var time = _startDate;

			// Act
			var result = time.AsUtcTime(_cultureInfo, _timeZoneTest);

			// Assert
			result.TimeOfDay.ShouldBe(_startDate.AddHours(23).TimeOfDay);
		}

		/// <summary>
		/// Checks that the StartOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_StartOfMonth()
		{
			// Arrange
			var dateTime = _startDate;

			// Act
			var result = dateTime.StartOfMonth();

			// Assert
			result.Day.ShouldBe(1);
		}

		/// <summary>
		/// Checks that the EndOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EndOfMonth()
		{
			// Arrange
			var dateTime = _startDate;

			// Act
			var result = dateTime.EndOfMonth();

			// Assert
			result.Day.ShouldBe(31); // may has 31 days
		}


		/// <summary>
		/// Checks that the ToSqlString method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_ToSqlString()
		{
			// Arrange
			var me = _startDate.Add(new TimeSpan(0, 1, 2, 3, 4));

			// Act
			var result = me.ToSqlString();

			// Assert
			result.ShouldBe("2020-05-15T01:02:03.0040000");
		}


		/// <summary>
		/// Checks that the Weeknumber method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Weeknumber()
		{
			// Arrange
			var dt = _startDate;
			var cultureInfo = CultureInfo.CurrentCulture;

			// Act
			var result = dt.Weeknumber(cultureInfo);

			// Assert
			result.ShouldBe(_startDateWeekNumber);
		}

		/// <summary>
		/// Checks that the SetHolidayProvider method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SetHolidayProvider_DefaultProvider()
		{
			// Arrange
			IHolidayProvider? holidayProvider = null;
			var knownHoliday = new DateTime(2020, 12, 25);

			// Act
			DateTimeExtensions.SetHolidayProvider(holidayProvider);

			// Assert
			knownHoliday.IsPublicHoliday().ShouldBeTrue();

		}

		/// <summary>
		/// Checks that the SetHolidayProvider method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SetHolidayProvider_WithNullProvider()
		{
			// Arrange
			IHolidayProvider? holidayProvider = new NullHolidayProvider();
			var knownHoliday = new DateTime(2020, 12, 25);

			// Act
			DateTimeExtensions.SetHolidayProvider(holidayProvider);

			// Assert
			knownHoliday.IsPublicHoliday().ShouldBeFalse();

		}
	}
}