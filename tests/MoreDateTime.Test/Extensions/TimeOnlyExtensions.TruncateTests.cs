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
		/// Checks that the TruncateTo method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateTo()
		{
			// implicitly tested through all the other TruncateToX methods
		}

		/// <summary>
		/// Checks that the TruncateToHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToHour()
		{
			// Arrange
			var dt = _startTime.AddHours(2).AddMinutes(5);

			// Act
			var result = dt.TruncateToHour();

			// Assert
			result.ShouldBe(new TimeOnly(2 + _startTime.Hour, 0, 0));
		}

		/// <summary>
		/// Checks that the TruncateToMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToMinute()
		{
			// Arrange
			var dt = _startTime.AddHours(2).AddMinutes(2).AddSeconds(10);

			// Act
			var result = dt.TruncateToMinute();

			// Assert
			result.ShouldBe(new TimeOnly(2 + _startTime.Hour, 2 + _startTime.Minute, 0));
		}

		/// <summary>
		/// Checks that the TruncateToSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_TruncateToSecond()
		{
			// Arrange
			var dt = _startTime.AddHours(2).AddMinutes(2).AddSeconds(10).AddMilliseconds(200);

			// Act
			var result = dt.TruncateToSecond();

			// Assert
			result.ShouldBe(new TimeOnly(2 + _startTime.Hour, 2 + _startTime.Minute, 10 + _startTime.Second));
		}
	}
}