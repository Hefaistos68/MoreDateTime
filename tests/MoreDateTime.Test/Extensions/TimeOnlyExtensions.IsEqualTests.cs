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
		/// Checks that the IsEqual method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqual()
		{
			// Arrange
			var dt = _startTime;
			var other = _startTime.AddHours(1);
			var truncateTo = DateTimeExtensions.DateTruncate.Week;

			// Act
			var result = dt.IsEqual(other, truncateTo);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsEqualDownToHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToHour()
		{
			// Arrange
			var dt = _startTime;
			var other = _startTime.AddMinutes(5);

			// Act
			var result = dt.IsEqualDownToHour(other);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsEqualDownToMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToMinute()
		{
			// Arrange
			var dt = _startTime;
			var other = _startTime.AddSeconds(10);

			// Act
			var result = dt.IsEqualDownToMinute(other);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsEqualDownToSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToSecond()
		{
			// Arrange
			var dt = _startTime;
			var other = _startTime.AddMilliseconds(100);

			// Act
			var result = dt.IsEqualDownToSecond(other);

			// Assert
			result.ShouldBeTrue();
		}
	}
}