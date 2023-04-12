namespace MoreDateTime.Tests.Extensions
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;
	using Nager.Date;

	/// <summary>
	/// Unit tests for the type <see cref="DateOnlyExtensions"/>.
	/// </summary>
	[TestClass]
	public partial class DateOnlyExtensionsTests
	{
		// setup some test data taken from real calendar
		private readonly DateOnly _startDate = new DateOnly(2020, 05, 15); // Friday

		private readonly DateOnly _midDate = new DateOnly(2021, 02, 20);   // Saturday
		private readonly DateOnly _endDate = new DateOnly(2021, 05, 14);   // Friday

		private readonly DateOnly _startDateFirstDay = new DateOnly(2020, 05, 01);  // Friday
		private readonly DateOnly _startDateLastDay = new DateOnly(2020, 05, 31);       // Sunday

		private readonly DayOfWeek _startDateDayOfWeek = DayOfWeek.Friday;
		private readonly int _startDateWeekNumber = 20;
		private readonly int _totalDaysInStartDateToEndDate = 365;

		private readonly DateOnly _startDateFirstMonday    = new DateOnly(2020, 05, 04);
		private readonly DateOnly _startDateFirstTuesday   = new DateOnly(2020, 05, 05);
		private readonly DateOnly _startDateFirstWednesday = new DateOnly(2020, 05, 06);
		private readonly DateOnly _startDateFirstThursday  = new DateOnly(2020, 05, 07);
		private readonly DateOnly _startDateFirstFriday    = new DateOnly(2020, 05, 01);
		private readonly DateOnly _startDateFirstSaturday  = new DateOnly(2020, 05, 02);
		private readonly DateOnly _startDateFirstSunday    = new DateOnly(2020, 05, 03);

		private readonly DateOnly _startDateLastMonday     = new DateOnly(2020, 05, 25);
		private readonly DateOnly _startDateLastTuesday    = new DateOnly(2020, 05, 26);
		private readonly DateOnly _startDateLastWednesday  = new DateOnly(2020, 05, 27);
		private readonly DateOnly _startDateLastThursday   = new DateOnly(2020, 05, 28);
		private readonly DateOnly _startDateLastFriday     = new DateOnly(2020, 05, 29);
		private readonly DateOnly _startDateLastSaturday   = new DateOnly(2020, 05, 30);
		private readonly DateOnly _startDateLastSunday     = new DateOnly(2020, 05, 31);

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
			DateSystem.LicenseKey = "Thank you for supporting open source projects";
			DateTimeExtensions.SetHolidayProvider(new DefaultHolidayProvider());
		}
		/// <summary>
		/// Checks that the StartOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_StartOfMonth()
		{
			// Arrange
			var DateOnly = _startDate;

			// Act
			var result = DateOnly.StartOfMonth();

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

			// Act
			var result = _startDate.EndOfMonth();

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
			var me = _startDate;

			// Act
			var result = me.ToSqlString();

			// Assert
			result.ShouldBe("2020-05-15");
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
			result.ShouldBe(20);
		}

	}
}