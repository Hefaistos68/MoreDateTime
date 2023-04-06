namespace MoreDateTime.Tests.Extensions
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateOnlyExtensionsTests
	{
		/// <summary>
		/// Checks that the FirstFridayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_FirstFridayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.FirstFridayOfMonth();

			// Assert
			result.ShouldBe(_startDateFirstFriday);
		}

		/// <summary>
		/// Checks that the FirstMondayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_FirstMondayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.FirstMondayOfMonth();

			// Assert
			result.ShouldBe(_startDateFirstMonday);
		}

		/// <summary>
		/// Checks that the FirstSaturdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_FirstSaturdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.FirstSaturdayOfMonth();

			// Assert
			result.ShouldBe(_startDateFirstSaturday);
		}

		/// <summary>
		/// Checks that the FirstSundayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_FirstSundayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.FirstSundayOfMonth();

			// Assert
			result.ShouldBe(_startDateFirstSunday);
		}

		/// <summary>
		/// Checks that the FirstThursdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_FirstThursdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.FirstThursdayOfMonth();

			// Assert
			result.ShouldBe(_startDateFirstThursday);
		}

		/// <summary>
		/// Checks that the FirstTuesdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_FirstTuesdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.FirstTuesdayOfMonth();

			// Assert
			result.ShouldBe(_startDateFirstTuesday);
		}

		/// <summary>
		/// Checks that the FirstWednesdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_FirstWednesdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.FirstWednesdayOfMonth();

			// Assert
			result.ShouldBe(_startDateFirstWednesday);
		}

		/// <summary>
		/// Checks that the FirstWeekdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_FirstWeekdayOfMonth()
		{
			// Arrange
			var dt = _startDate;

			// Act
			var result1 = dt.FirstWeekdayOfMonth(DayOfWeek.Tuesday);
			var result2 = dt.FirstWeekdayOfMonth(DayOfWeek.Sunday);

			// Assert
			result1.DayOfWeek.ShouldBe(DayOfWeek.Tuesday);
			result1.Day.ShouldBe(5);

			result2.DayOfWeek.ShouldBe(DayOfWeek.Sunday);
			result2.Day.ShouldBe(3);
		}

		/// <summary>
		/// Checks that the LastFridayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_LastFridayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.LastFridayOfMonth();

			// Assert
			result.ShouldBe(_startDateLastFriday);
		}

		/// <summary>
		/// Checks that the LastMondayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_LastMondayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.LastMondayOfMonth();

			// Assert
			result.ShouldBe(_startDateLastMonday);
		}

		/// <summary>
		/// Checks that the LastSaturdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_LastSaturdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.LastSaturdayOfMonth();

			// Assert
			result.ShouldBe(_startDateLastSaturday);
		}

		/// <summary>
		/// Checks that the LastSundayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_LastSundayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.LastSundayOfMonth();

			// Assert
			result.ShouldBe(_startDateLastSunday);
		}

		/// <summary>
		/// Checks that the LastThursdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_LastThursdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.LastThursdayOfMonth();

			// Assert
			result.ShouldBe(_startDateLastThursday);
		}

		/// <summary>
		/// Checks that the LastTuesdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_LastTuesdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.LastTuesdayOfMonth();

			// Assert
			result.ShouldBe(_startDateLastTuesday);
		}

		/// <summary>
		/// Checks that the LastWednesdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_LastWednesdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result = me.LastWednesdayOfMonth();

			// Assert
			result.ShouldBe(_startDateLastWednesday);
		}

		/// <summary>
		/// Checks that the LastWeekdayOfMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_LastWeekdayOfMonth()
		{
			// Arrange
			var me = _startDate;

			// Act
			var result1 = me.LastWeekdayOfMonth(DayOfWeek.Sunday);
			var result2 = me.LastWeekdayOfMonth(DayOfWeek.Monday);

			// Assert
			result1.DayOfWeek.ShouldBe(DayOfWeek.Sunday);
			result1.Day.ShouldBe(31);

			result2.DayOfWeek.ShouldBe(DayOfWeek.Monday);
			result2.Day.ShouldBe(25);
		}
	}
}