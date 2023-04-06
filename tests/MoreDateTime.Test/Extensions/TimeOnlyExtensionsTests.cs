namespace MoreDateTime.Tests.Extensions
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="TimeOnlyExtensions"/>.
	/// </summary>
	[TestClass]
	public partial class TimeOnlyExtensionsTests
	{
		private static readonly TimeOnly _startTime = new TimeOnly(1, 2, 3, 4);
		private static readonly TimeOnly _endTime = new TimeOnly(10, 11, 12, 987);

		private readonly int _hoursInStartTimeToEndTime = 10;
		private readonly int _minutesInStartTimeToEndTime = 10 * 60;		// not right
		private readonly int _secondsInStartTimeToEndTime = 10 * 60 * 60;	// not right

		/// <summary>
		/// Checks that the AddMilliseconds method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_AddMilliseconds()
		{
			// Arrange
			var dt = _startTime;
			var milliseconds = 555;

			// Act
			var result = dt.AddMilliseconds(milliseconds);

			// Assert
			result.Millisecond.ShouldBe(_startTime.Millisecond + milliseconds);
		}

		/// <summary>
		/// Checks that the AddSeconds method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_AddSeconds()
		{
			// Arrange
			var dt = _startTime;
			var seconds = 25;

			// Act
			var result = dt.AddSeconds(seconds);

			// Assert
			result.Second.ShouldBe(_startTime.Second + seconds);
		}

	}
}