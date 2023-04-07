namespace MoreDateTime.Tests.Extensions
{
	using System;

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

			// Act
			var result = dt.NextWorkday();

			// Assert
			result.DayOfWeek.ShouldBe(DayOfWeek.Monday);
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

			// Act
			var result = dt.PreviousWorkday();

			// Assert
			result.DayOfWeek.ShouldBe(DayOfWeek.Thursday);

			// Todo: should also test with holidays
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
	}
}