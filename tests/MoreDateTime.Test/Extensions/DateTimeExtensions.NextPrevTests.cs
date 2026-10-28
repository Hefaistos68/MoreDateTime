namespace MoreDateTime.Tests.Extensions
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateTimeExtensionsTests
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
			result.TimeOfDay.ShouldBe(dt.TimeOfDay);
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
		/// Checks that the NextHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextHour()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.NextHour();

			// Assert
			result.Hour.ShouldBe(1);
		}

		/// <summary>
		/// Checks that the NextMillisecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextMillisecond()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.NextMillisecond();

			// Assert
			result.Millisecond.ShouldBe(1);
		}

		/// <summary>
		/// Checks that the NextMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextMinute()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.NextMinute();

			// Assert
			result.Minute.ShouldBe(1);
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
		/// Checks that the NextSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextSecond()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.NextSecond();

			// Assert
			result.Second.ShouldBe(1);
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
		/// Checks that the NextWorkday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextWorkday()
		{
			// Arrange
			var dt = _startDate;
			var beforeChristmas = new DateTime(2020, 12, 24);

			// Act
			var result = dt.NextWorkday();
			var afterChristmas = beforeChristmas.NextWorkday(_cultureInfo);

			// Assert
			result.DayOfWeek.ShouldBe(DayOfWeek.Monday);
			afterChristmas.ShouldBe(new DateTime(2020, 12, 28));
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
		/// Checks that NextHoliday and PreviousHoliday return DateTime.MaxValue when NullHolidayProvider is active.
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
			next.ShouldBe(DateTime.MaxValue);
			prev.ShouldBe(DateTime.MaxValue);
		}

		/// <summary>
		/// Checks that the PreviousHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousHour()
		{
			// Arrange
			var dt = _startDate.AddHours(5);

			// Act
			var result = dt.PreviousHour();

			// Assert
			result.Hour.ShouldBe(4);
		}

		/// <summary>
		/// Checks that the PreviousMillisecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousMillisecond()
		{
			// Arrange
			var dt = _startDate.AddMilliseconds(5);

			// Act
			var result = dt.PreviousMillisecond();

			// Assert
			result.Millisecond.ShouldBe(4);
		}

		/// <summary>
		/// Checks that the PreviousMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousMinute()
		{
			// Arrange
			var dt = _startDate.AddMinutes(5);

			// Act
			var result = dt.PreviousMinute();

			// Assert
			result.Minute.ShouldBe(4);
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
		/// Checks that the PreviousSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousSecond()
		{
			// Arrange
			var dt = _startDate.AddSeconds(5);

			// Act
			var result = dt.PreviousSecond();

			// Assert
			result.Second.ShouldBe(4);
		}

		/// <summary>
		/// Checks that the PreviousWorkday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousWorkday()
		{
			// Arrange
			var dt = _startDate;
			var afterNewYear = new DateTime(2021, 01, 04);

			// Act
			var result = dt.PreviousWorkday();
			var beforeWeekendAndHoliday = afterNewYear.PreviousWorkday(_cultureInfo);

			// Assert
			result.DayOfWeek.ShouldBe(DayOfWeek.Thursday);
			beforeWeekendAndHoliday.ShouldBe(new DateTime(2020, 12, 31));
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
		/// Checks that the NextFullSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextFullSecond()
		{
			// Arrange

			// Act
			var result = _startDateTime.AddMilliseconds(200).NextFullSecond();

			// Assert
			result.ShouldBe(_startDateTime.NextSecond());
		}

		/// <summary>
		/// Checks that the NextFullMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextFullMinute()
		{
			// Arrange

			// Act
			var result = _startDate.NextFullMinute();

			// Assert
			result.ShouldBe(_startDate.AddMinutes(1).TruncateToMinute());
		}

		/// <summary>
		/// Checks that the NextFullHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextFullHour()
		{
			// Arrange
			var result = _startDate.NextFullHour();

			// Assert
			result.ShouldBe(_startDate.AddHours(1).TruncateToHour());
		}

		/// <summary>
		/// Checks that the NextFullDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextFullDay()
		{
			// Arrange
			var result = _startDate.NextFullDay();

			// Assert
			result.ShouldBe(_startDate.AddDays(1).TruncateToDay());
		}

		/// <summary>
		/// Checks that the NextWeekend method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextWeekend()
		{
			// Arrange
			var saturday = new DateTime(2020, 5, 16);

			// Act
			var result = _startDate.NextWeekend();
			var fromWeekendResult = saturday.NextWeekend();

			// Assert
			result.ShouldBe(new DateTime(2020, 5, 16));
			fromWeekendResult.ShouldBe(new DateTime(2020, 5, 23));
		}

		/// <summary>
		/// Checks that the PreviousWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousWeek()
		{
			// Arrange
			var result = _startDate.PreviousWeek();

			// Assert
			result.ShouldBe(new DateTime(2020, 5, 15 - 7));
		}

		/// <summary>
		/// Checks that the PreviousWeekend method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousWeekend()
		{
			// Arrange
			var saturday = new DateTime(2020, 5, 16);

			// Act
			var result = _startDate.PreviousWeekend();
			var fromWeekendResult = saturday.PreviousWeekend();

			// Assert
			result.ShouldBe(new DateTime(2020, 5, 10));  // sunday
			fromWeekendResult.ShouldBe(new DateTime(2020, 5, 10));
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
			result.ShouldBe(_startDate.AddDays(2));	 // Sunday
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
			result.ShouldBe(_startDate.AddDays(1));	 // saturday
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
