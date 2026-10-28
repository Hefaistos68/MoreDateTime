namespace MoreDateTime.Tests.Extensions
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Nager.Date;

	using Shouldly;

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
			var resultDefault = dt.Weeknumber();
			var weekOfYear = dt.WeekOfYear(cultureInfo);
			var weekOfYearDefault = dt.WeekOfYear();

			// Assert
			result.ShouldBe(20);
			resultDefault.ShouldBe(20);
			weekOfYear.ShouldBe(20);
			weekOfYearDefault.ShouldBe(20);
		}

		/// <summary>
		/// Checks that the Distance method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Distance()
		{
			// Arrange

			// Act
			var result = _startDate.Distance(_endDate);
			var reverseResult = _endDate.Distance(_startDate);

			// Assert
			result.ShouldBe(_endDate.ToDateTime() - _startDate.ToDateTime());
			reverseResult.ShouldBe(_endDate.ToDateTime() - _startDate.ToDateTime());
		}

		/// <summary>
		/// Checks that the Sub method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Sub()
		{
			// Arrange

			// Act
			var resultSeconds = _startDate.Sub(TimeSpan.FromSeconds(5));
			var resultMinutes = _startDate.Sub(TimeSpan.FromMinutes(5));
			var resultHours = _startDate.Sub(TimeSpan.FromHours(5));
			var resultDays = _startDate.Sub(TimeSpan.FromDays(5));

			// Assert
			resultSeconds.ShouldBe(_startDate.AddSeconds(-5));
			resultMinutes.ShouldBe(_startDate.AddMinutes(-5));
			resultHours.ShouldBe(_startDate.AddHours(-5));
			resultDays.ShouldBe(_startDate.AddDays(-5));
		}

		/// <summary>
		/// Checks that the Split method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SplitWithStartDateAndEndDateAndParts()
		{
			// Arrange
			var parts = 5;

			// Act
			var result = _startDate.Split(_endDate, parts);

			// Assert
			result.Count().ShouldBe(parts);

			// verify that all parts are the same size
			var partSize = (_startDate.Distance(_endDate) / parts).TruncateToDay();

			foreach (var part in result)
			{
				part.Distance().ShouldBe(partSize);
			}
		}

		/// <summary>
		/// Checks that the Split method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SplitWithStartDateAndDistanceAndParts()
		{
			// Arrange
			var parts = 5;
			var distance = (_endDate.ToDateTime() - _startDate.ToDateTime());

			// Act
			var result = _startDate.Split(distance, parts);

			// Assert
			result.Count().ShouldBe(parts);

			// verify that all parts are the same size
			var partSize = (_startDate.Distance(_endDate) / parts).TruncateToDay();

			foreach (var part in result)
			{
				part.Distance().ShouldBe(partSize);
			}
		}

		/// <summary>
		/// Checks that the Split method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SplitWithDatesAndParts()
		{
			// Arrange
			var dates = new DateOnlyRange(_startDate, _endDate);
			var parts = 5;

			// Act
			var result = dates.Split(parts);

			// Assert
			// verify that all parts are the same size
			var partSize = (_startDate.Distance(_endDate) / parts).TruncateToDay();

			foreach (var part in result)
			{
				part.Distance().ShouldBe(partSize);
			}
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithDatesAndParts_WithNullDates()
		{
			Should.Throw<ArgumentNullException>(() => default(DateOnlyRange)!.Split(5));
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithDatesAndParts_WithZeroParts()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => new DateOnlyRange(_startDate, _endDate).Split(0));
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithStartDateAndEndDateAndParts_WithZeroParts()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => _startDate.Split(_endDate, 0));
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithStartDateAndDistanceAndParts_WithZeroParts()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => _startDate.Split(TimeSpan.FromMinutes(10), 0));
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithStartDateAndDistanceAndParts_WithDistanceLessThanParts()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => _startDate.Split(TimeSpan.FromTicks(5), 6));
		}

		/// <summary>
		/// Checks that the AddTicks method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_AddTicks()
		{
			// Arrange
			var ticks = TimeSpan.FromDays(5).Ticks;

			// Act
			var result1 = _startDate.AddTicks(5);
			var result2 = _startDate.AddTicks(ticks);

			// Assert
			result1.ShouldBe(_startDate);
			result2.ShouldBe(_startDate.AddDays(5));
		}

		/// <summary>
		/// Checks that the AddWeeks method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_AddWeeks()
		{
			// Arrange

			// Act
			var result = _startDate.AddWeeks(2);

			// Assert
			result.ShouldBe(_startDate.AddDays(14));
		}

		/// <summary>
		/// Checks that the AddMilliseconds method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_AddMilliseconds()
		{
			// Arrange
			var value = DateTimeExtensions.MillisPerDay * 5;

			// Act
			var result1 = _startDate.AddMilliseconds(500);
			var result2 = _startDate.AddMilliseconds(value);

			// Assert
			result1.ShouldBe(_startDate);
			result2.ShouldBe(_startDate.AddDays(5));
		}

		/// <summary>
		/// Checks that the Add, AddHours, AddMinutes, and AddSeconds methods function correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_AddTimeUnits()
		{
			// Act & Assert
			_startDate.Add(TimeSpan.FromHours(12)).ShouldBe(_startDate);
			_startDate.Add(TimeSpan.FromDays(3)).ShouldBe(_startDate.AddDays(3));
			_startDate.AddHours(12).ShouldBe(_startDate);
			_startDate.AddHours(48).ShouldBe(_startDate.AddDays(2));
			_startDate.AddMinutes(30).ShouldBe(_startDate);
			_startDate.AddMinutes(24 * 60 * 2).ShouldBe(_startDate.AddDays(2));
			_startDate.AddSeconds(30).ShouldBe(_startDate);
			_startDate.AddSeconds(24 * 60 * 60 * 2).ShouldBe(_startDate.AddDays(2));
		}

		/// <summary>
		/// Checks that the ToDateTime method with DateTimeKind functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_ToDateTime_WithKind()
		{
			// Act
			var utcResult = _startDate.ToDateTime(DateTimeKind.Utc);
			var localResult = _startDate.ToDateTime(DateTimeKind.Local);

			// Assert
			utcResult.Kind.ShouldBe(DateTimeKind.Unspecified);
			utcResult.Date.ShouldBe(_startDate.ToDateTime().Date);
			localResult.Kind.ShouldBe(DateTimeKind.Unspecified);
		}

		/// <summary>
		/// Checks that the IsWithin method with DateOnlyRange functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsWithin_DateOnlyRange()
		{
			// Arrange
			var orderedRange = new DateOnlyRange(_startDate, _endDate);
			var unorderedRange = new DateOnlyRange(_endDate, _startDate);

			// Act & Assert
			_midDate.IsWithin(orderedRange).ShouldBeTrue();
			_startDate.AddDays(-1).IsWithin(orderedRange).ShouldBeFalse();
			_endDate.AddDays(1).IsWithin(orderedRange).ShouldBeFalse();
			Should.Throw<ArgumentException>(() => _midDate.IsWithin(unorderedRange));
		}

		/// <summary>
		/// Checks that the IsPublicHoliday method with null CultureInfo functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPublicHoliday_WithNullCulture()
		{
			// Arrange
			var christmas = new DateOnly(2020, 12, 25);

			// Act & Assert
			christmas.IsPublicHoliday().ShouldBeTrue();
			_startDate.IsPublicHoliday().ShouldBeFalse();
		}
	}
}
