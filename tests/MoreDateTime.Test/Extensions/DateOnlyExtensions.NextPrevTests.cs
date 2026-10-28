namespace MoreDateTime.Tests.Extensions
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateOnlyExtensionsTests
	{
		/// <summary>
		/// Checks that the NextDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextDay()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.NextDay();

			// Assert
			result.DayOfWeek.ShouldBe(DayOfWeek.Saturday);
		}

		/// <summary>
		/// Checks that the NextHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextHoliday()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(new DefaultHolidayProvider());

			// Act
			var holidays = _startDate.EnumerateHolidaysUntil(_endDate, _cultureInfo);
			var firstHoliday = holidays.First();
			var secondHoliday = holidays.Skip(1).First();

			var result = firstHoliday.NextHoliday(_cultureInfo);

			// Assert
			result.ShouldBe(secondHoliday);
		}

		/// <summary>
		/// Checks that the NextHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextHoliday_FromMid()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(new DefaultHolidayProvider());

			// Act
			var holidays = _startDate.EnumerateHolidaysUntil(_endDate, _cultureInfo);
			var firstHoliday = holidays.First();
			var secondHoliday = holidays.Skip(1).First();

			var result = _midDate.NextHoliday(_cultureInfo);

			// Assert
			result.ShouldBe(holidays.Skip(3).First());
		}

		/// <summary>
		/// Checks that the NextMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextMonth()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.NextMonth();

			// Assert
			result.Month.ShouldBe(_startDate.Month + 1);
		}

		/// <summary>
		/// Checks that the NextWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextWeek()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.NextWeek();

			// Assert
			result.Weeknumber().ShouldBe(dt.Weeknumber() + 1);
		}

		/// <summary>
		/// Checks that the PreviousWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousWeek()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.PreviousWeek();

			// Assert
			result.Weeknumber().ShouldBe(dt.Weeknumber() - 1);
		}

		/// <summary>
		/// Checks that the NextWorkday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextWorkday()
		{
			// Arrange
			var dt = _startDate;
			var beforeChristmas = new DateOnly(2020, 12, 24); // Thursday, Dec 25 is Friday holiday

			// Act
			var result = dt.NextWorkday();
			var afterChristmas = beforeChristmas.NextWorkday(_cultureInfo);

			// Assert
			result.DayOfWeek.ShouldBe(DayOfWeek.Monday);
			afterChristmas.ShouldBe(new DateOnly(2020, 12, 28));
		}

		/// <summary>
		/// Checks that the NextYear method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextYear()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.NextYear();

			// Assert
			result.Year.ShouldBe(_startDate.Year + 1);
		}

		/// <summary>
		/// Checks that the PreviousDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousDay()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.PreviousDay();

			// Assert
			result.Day.ShouldBe(14);
			result.DayOfWeek.ShouldBe(DayOfWeek.Thursday);
		}

		/// <summary>
		/// Checks that the PreviousHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousHoliday()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(new DefaultHolidayProvider());

			// Act
			var holidays = _startDate.EnumerateHolidaysUntil(_endDate, _cultureInfo);
			var firstHoliday = holidays.First();
			var secondHoliday = holidays.Skip(1).First();

			var result = secondHoliday.PreviousHoliday(_cultureInfo);

			// Assert
			result.ShouldBe(firstHoliday);
		}

		/// <summary>
		/// Checks that NextHoliday and PreviousHoliday return DateOnly.MaxValue when NullHolidayProvider is used.
		/// </summary>
		[TestMethod]
		public void CanCall_NextAndPreviousHoliday_WithNullHolidayProvider()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(new NullHolidayProvider());

			// Act
			var next = _startDate.NextHoliday();
			var prev = _startDate.PreviousHoliday();

			// Assert
			next.ShouldBe(DateOnly.MaxValue);
			prev.ShouldBe(DateOnly.MaxValue);
		}

		/// <summary>
		/// Checks that the NextWeekend and PreviousWeekend methods function correctly when starting on a weekend.
		/// </summary>
		[TestMethod]
		public void CanCall_NextAndPreviousWeekend_FromWeekend()
		{
			// Arrange
			var saturday = new DateOnly(2020, 05, 16);

			// Act
			var nextWeekend = saturday.NextWeekend();
			var prevWeekend = saturday.PreviousWeekend();

			// Assert
			nextWeekend.ShouldBe(new DateOnly(2020, 05, 23));
			prevWeekend.ShouldBe(new DateOnly(2020, 05, 10));
		}

		/// <summary>
		/// Checks that the PreviousMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousMonth()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.PreviousMonth();

			// Assert
			result.Month.ShouldBe(4);
		}

		/// <summary>
		/// Checks that the PreviousWorkday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousWorkday()
		{
			// Arrange
			var dt = _startDate;
			var afterNewYear = new DateOnly(2021, 01, 04); // Monday, Jan 1 was Friday holiday

			// Act
			var result = dt.PreviousWorkday();
			var beforeWeekendAndHoliday = afterNewYear.PreviousWorkday(_cultureInfo);

			// Assert
			result.DayOfWeek.ShouldBe(DayOfWeek.Thursday);
			beforeWeekendAndHoliday.ShouldBe(new DateOnly(2020, 12, 31));
		}

		/// <summary>
		/// Checks that the PreviousYear method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousYear()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.PreviousYear();

			// Assert
			result.Year.ShouldBe(2019);
		}

		/// <summary>
		/// Checks that the EndOfWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EndOfWeek_Europe()
		{
			// Arrange
			var cultureInfo = CultureInfo.GetCultureInfo("de-DE");

			// Act
			var result = _startDate.EndOfWeek(cultureInfo);
			var defaultResult = _startDate.EndOfWeek();

			// Assert
			result.ShouldBe(_startDate.AddDays(2));  // Sunday
			defaultResult.ShouldNotBe(default);
		}

		/// <summary>
		/// Checks that the StartOfWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_StartOfWeek_Europe()
		{
			// Arrange
			var cultureInfo = CultureInfo.GetCultureInfo("de-DE");

			// Act
			var result = _startDate.StartOfWeek(cultureInfo);
			var defaultResult = _startDate.StartOfWeek();

			// Assert
			result.ShouldBe(_startDate.AddDays(-4)); // Monday
			defaultResult.ShouldNotBe(default);
		}

		/// <summary>
		/// Checks that the EndOfWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EndOfWeek_US()
		{
			// Arrange
			var cultureInfo = CultureInfo.GetCultureInfo("en-US");

			// Act
			var result = _startDate.EndOfWeek(cultureInfo);

			// Assert
			result.ShouldBe(_startDate.AddDays(1));  // saturday
		}

		/// <summary>
		/// Checks that the StartOfWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_StartOfWeek_US()
		{
			// Arrange
			var cultureInfo = CultureInfo.GetCultureInfo("en-US");

			// Act
			var result = _startDate.StartOfWeek(cultureInfo);

			// Assert
			result.ShouldBe(_startDate.AddDays(-5)); // sunday
		}

	}
}
