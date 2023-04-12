namespace MoreDateTime.Tests.Extensions
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="TimeSpanExtensions"/>.
	/// </summary>
	[TestClass]
	public class TimeSpanExtensionsTests
	{
		private readonly TimeSpan _timeSpan = new TimeSpan(1, 2, 3, 4, 5);

		/// <summary>
		/// Checks that the IsNegative method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsNegative()
		{
			// Arrange
			var ts = TimeSpan.FromSeconds(135);

			// Act
			var result1 = ts.IsNegative();
			var result2 = (-ts).IsNegative();

			// Assert
			result1.ShouldBeFalse();
			result2.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsPositive method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPositive()
		{
			// Arrange
			var ts = TimeSpan.FromSeconds(328);

			// Act
			var result1 = ts.IsPositive();
			var result2 = (-ts).IsPositive();

			// Assert
			result1.ShouldBeTrue();
			result2.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the RoundTo method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_RoundTo()
		{
			// implicitly tested by the other RoundTo tests
		}

		/// <summary>
		/// Checks that the RoundTo method functions correctly.
		/// </summary>
		[TestMethod]
		public void CannotCall_RoundTo_WithInvalidUnit()
		{
			Should.Throw<ArgumentException>(() => _timeSpan.RoundTo((TimeSpanExtensions.RoundingUnit)99));
		}

		/// <summary>
		/// Checks that the RoundToDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_RoundToDay()
		{
			// Arrange

			// Act
			var result = _timeSpan.RoundToDay();

			// Assert
			result.Days.ShouldBe(_timeSpan.Days);
			result.TotalHours.ShouldBe((_timeSpan.Days * 24));
			result.TotalMinutes.ShouldBe((_timeSpan.Days * 24) * 60);
			result.TotalSeconds.ShouldBe(((_timeSpan.Days * 24) * 60) * 60);
			result.TotalMilliseconds.ShouldBe((((_timeSpan.Days * 24) * 60) * 60) * 1000);
		}

		/// <summary>
		/// Checks that the RoundToHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_RoundToHour()
		{
			// Arrange

			// Act
			var result = _timeSpan.RoundToHour();

			// Assert
			result.Days.ShouldBe(_timeSpan.Days);
			result.TotalHours.ShouldBe((_timeSpan.Days * 24) + _timeSpan.Hours);
			result.TotalMinutes.ShouldBe(((_timeSpan.Days * 24) + _timeSpan.Hours) * 60);
			result.TotalSeconds.ShouldBe((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) * 60);
			result.TotalMilliseconds.ShouldBe(((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) * 60) * 1000);
		}

		/// <summary>
		/// Checks that the RoundToMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_RoundToMinute()
		{
			// Arrange

			// Act
			var result = _timeSpan.RoundToMinute();

			// Assert
			result.Days.ShouldBe(_timeSpan.Days);
			result.Hours.ShouldBe(_timeSpan.Hours);
			result.TotalMinutes.ShouldBe((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes);
			result.TotalSeconds.ShouldBe(((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes) * 60);
			result.TotalMilliseconds.ShouldBe((((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes) * 60) * 1000);
		}

		/// <summary>
		/// Checks that the RoundToSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_RoundToSecond()
		{
			// Arrange

			// Act
			var result = _timeSpan.RoundToSecond();

			// Assert
			result.Days.ShouldBe(_timeSpan.Days);
			result.Hours.ShouldBe(_timeSpan.Hours);
			result.Minutes.ShouldBe(_timeSpan.Minutes);
			result.TotalSeconds.ShouldBe((((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes) * 60) + _timeSpan.Seconds);
			result.TotalMilliseconds.ShouldBe(((((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes) * 60) + _timeSpan.Seconds) * 1000);
		}

		/// <summary>
		/// Checks that the TruncateTo method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateTo()
		{
			// implicitly tested by the other truncate tests
		}

		/// <summary>
		/// Checks that the TruncateToDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToDay()
		{
			// Arrange

			// Act
			var result = _timeSpan.TruncateToDay();

			// Assert
			result.Days.ShouldBe(_timeSpan.Days);
			result.TotalHours.ShouldBe((_timeSpan.Days * 24));
			result.TotalMinutes.ShouldBe(((_timeSpan.Days * 24)) * 60);
			result.TotalSeconds.ShouldBe((((_timeSpan.Days * 24)) * 60) * 60);
			result.TotalMilliseconds.ShouldBe(((((_timeSpan.Days * 24)) * 60) * 60) * 1000);
		}

		/// <summary>
		/// Checks that the TruncateToHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToHour()
		{
			// Arrange

			// Act
			var result = _timeSpan.TruncateToHour();

			// Assert
			result.Days.ShouldBe(_timeSpan.Days);
			result.TotalHours.ShouldBe((_timeSpan.Days * 24) + _timeSpan.Hours);
			result.TotalMinutes.ShouldBe(((_timeSpan.Days * 24) + _timeSpan.Hours) * 60);
			result.TotalSeconds.ShouldBe((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) * 60);
			result.TotalMilliseconds.ShouldBe(((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) * 60) * 1000);
		}

		/// <summary>
		/// Checks that the TruncateToMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToMinute()
		{
			// Arrange

			// Act
			var result = _timeSpan.TruncateToMinute();

			// Assert
			result.Days.ShouldBe(_timeSpan.Days);
			result.Hours.ShouldBe( _timeSpan.Hours);
			result.TotalMinutes.ShouldBe((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes);
			result.TotalSeconds.ShouldBe(((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes) * 60);
			result.TotalMilliseconds.ShouldBe((((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes) * 60) * 1000);
		}

		/// <summary>
		/// Checks that the TruncateToSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToSecond()
		{
			// Arrange

			// Act
			var result = _timeSpan.TruncateToSecond();

			// Assert
			result.Days.ShouldBe(_timeSpan.Days);
			result.Hours.ShouldBe(_timeSpan.Hours);
			result.Minutes.ShouldBe(_timeSpan.Minutes);
			result.TotalSeconds.ShouldBe(((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes) * 60 + _timeSpan.Seconds);
			result.TotalMilliseconds.ShouldBe(((((((_timeSpan.Days * 24) + _timeSpan.Hours) * 60) + _timeSpan.Minutes) * 60) + _timeSpan.Seconds) * 1000);
		}
	}
}