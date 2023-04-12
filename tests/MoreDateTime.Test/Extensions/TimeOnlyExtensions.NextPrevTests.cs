namespace MoreDateTime.Tests.Extensions
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class TimeOnlyExtensionsTests
	{
		/// <summary>
		/// Checks that the NextMillisecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextMillisecond()
		{
			// Arrange
			var dt = _startTime;

			// Act
			var result = dt.NextMillisecond();

			// Assert
			result.Millisecond.ShouldBe(5);
		}

		/// <summary>
		/// Checks that the NextSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextSecond()
		{
			// Arrange
			var dt = _startTime;

			// Act
			var result = dt.NextSecond();

			// Assert
			result.Second.ShouldBe(4);
		}

		/// <summary>
		/// Checks that the NextMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextMinute()
		{
			// Arrange
			var dt = _startTime;

			// Act
			var result = dt.NextMinute();

			// Assert
			result.Minute.ShouldBe(3);
		}

		/// <summary>
		/// Checks that the NextHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextHour()
		{
			// Arrange
			var dt = _startTime;

			// Act
			var result = dt.NextHour();

			// Assert
			result.Hour.ShouldBe(2);
		}

		/// <summary>
		/// Checks that the PreviousMillisecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousMillisecond()
		{
			// Arrange
			var dt = _startTime;

			// Act
			var result = dt.PreviousMillisecond();

			// Assert
			result.Millisecond.ShouldBe(3);
		}

		/// <summary>
		/// Checks that the PreviousSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousSecond()
		{
			// Arrange
			var dt = _startTime;

			// Act
			var result = dt.PreviousSecond();

			// Assert
			result.Second.ShouldBe(2);
		}

		/// <summary>
		/// Checks that the PreviousMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousMinute()
		{
			// Arrange
			var dt = _startTime;

			// Act
			var result = dt.PreviousMinute();

			// Assert
			result.Minute.ShouldBe(1);
		}

		/// <summary>
		/// Checks that the PreviousHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_PreviousHour()
		{
			// Arrange
			var dt = _startTime;

			// Act
			var result = dt.PreviousHour();

			// Assert
			result.Hour.ShouldBe(0);
		}

		/// <summary>
		/// Checks that the NextFullSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextFullSecond()
		{
			// Arrange

			// Act
			var result = _startTime.AddMilliseconds(200).NextFullSecond();

			// Assert
			result.ShouldBe(_startTime.NextSecond().TruncateToSecond());
		}

		/// <summary>
		/// Checks that the NextFullMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextFullMinute()
		{
			// Arrange

			// Act
			var result = _startTime.NextFullMinute();

			// Assert
			result.ShouldBe(_startTime.AddMinutes(1).TruncateToMinute());
		}

		/// <summary>
		/// Checks that the NextFullHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NextFullHour()
		{
			// Arrange
			var result = _startTime.NextFullHour();

			// Assert
			result.ShouldBe(_startTime.AddHours(1).TruncateToHour());
		}

	}
}