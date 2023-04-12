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
		/// Checks that the NumberOfHoursUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfHoursUntil()
		{
			// Arrange

			// Act
			var result = _startTime.NumberOfHoursUntil(_endTime);

			// Assert
			result.ShouldBe((_endTime - _startTime).TotalHours);
		}

		/// <summary>
		/// Checks that the NumberOfMillisecondsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfMillisecondsUntil()
		{
			// Arrange

			// Act
			var result = _startTime.NumberOfMillisecondsUntil(_endTime);

			// Assert
			result.ShouldBe((_endTime - _startTime).TotalMilliseconds);
		}

		/// <summary>
		/// Checks that the NumberOfMinutesUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfMinutesUntil()
		{
			// Arrange

			// Act
			var result = _startTime.NumberOfMinutesUntil(_endTime);

			// Assert
			result.ShouldBe((_endTime - _startTime).TotalMinutes);
		}

		/// <summary>
		/// Checks that the NumberOfSecondsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfSecondsUntil()
		{
			// Arrange

			// Act
			var result = _startTime.NumberOfSecondsUntil(_endTime);

			// Assert
			result.ShouldBe((_endTime - _startTime).TotalSeconds);
		}
	}
}