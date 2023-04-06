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
		/// Checks that the TruncateTo method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateTo()
		{
			// implicitly tested through all the other TruncateToX methods
		}

		/// <summary>
		/// Checks that the TruncateToDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToDay()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.TruncateToDay();

			// Assert
			result.TimeOfDay.ShouldBe(new TimeSpan(0));
		}

		/// <summary>
		/// Checks that the TruncateToHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToHour()
		{
			// Arrange
			var dt = _startDate.AddHours(2).AddMinutes(5);

			// Act
			var result = dt.TruncateToHour();

			// Assert
			result.TimeOfDay.ShouldBe(new TimeSpan(2, 0, 0));
		}

		/// <summary>
		/// Checks that the TruncateToMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToMinute()
		{
			// Arrange
			var dt = _startDate.AddHours(2).AddMinutes(2).AddSeconds(10);

			// Act
			var result = dt.TruncateToMinute();

			// Assert
			result.TimeOfDay.ShouldBe(new TimeSpan(2, 2, 0));
		}

		/// <summary>
		/// Checks that the TruncateToMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToMonth()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.TruncateToMonth();

			// Assert
			result.Day.ShouldBe(1);
		}

		/// <summary>
		/// Checks that the TruncateToSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToSecond()
		{
			// Arrange
			var dt = _startDate.AddHours(2).AddMinutes(2).AddSeconds(10).AddMilliseconds(200);

			// Act
			var result = dt.TruncateToSecond();

			// Assert
			result.TimeOfDay.ShouldBe(new TimeSpan(2, 2, 10));
		}

		/// <summary>
		/// Checks that the TruncateToWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToWeek()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result = dt.TruncateToWeek(_cultureInfo);

			// Assert
			result.DayOfWeek.ShouldBe(DayOfWeek.Sunday);
		}

		/// <summary>
		/// Checks that the TruncateToYear method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToYear()
		{
			// Arrange
			var dt = _startDate.AddMonths(2).AddDays(2).AddHours(5);

			// Act
			var result = dt.TruncateToYear();

			// Assert
			result.Month.ShouldBe(1);
			result.Day.ShouldBe(1);
			result.TimeOfDay.ShouldBe(new TimeSpan(0));
		}
	}
}