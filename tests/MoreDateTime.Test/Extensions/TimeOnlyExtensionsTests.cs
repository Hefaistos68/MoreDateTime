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
		private static readonly TimeOnly _zeroTime = new TimeOnly(0, 0, 0, 0);
		private static readonly TimeOnly _startTime = new TimeOnly(1, 2, 3, 4);
		private static readonly TimeOnly _midTime = new TimeOnly(5, 6, 7, 8);
		private static readonly TimeOnly _endTime = new TimeOnly(10, 11, 12, 987);

		private readonly DateTime _startDateTime = new DateTime(2020, 05, 15, 2, 3, 4); // Friday

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

		/// <summary>
		/// Checks that the Distance method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Distance()
		{
			// Arrange

			// Act
			var result = _startTime.Distance(_endTime);

			// Assert
			result.ShouldBe(_endTime - _startTime);
		}

		/// <summary>
		/// Checks that the Sub method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Sub()
		{
			// Arrange

			// Act
			var resultSeconds = _startTime.Sub(TimeSpan.FromSeconds(5));
			var resultMinutes = _startTime.Sub(TimeSpan.FromMinutes(5));
			var resultHours = _startTime.Sub(TimeSpan.FromHours(5));

			// Assert
			resultSeconds.ShouldBe(_startTime.AddSeconds(-5));
			resultMinutes.ShouldBe(_startTime.AddMinutes(-5));
			resultHours.ShouldBe(_startTime.AddHours(-5));
		}

		/// <summary>
		/// Checks that the Split method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SplitWithstartTimeAndendTimeAndParts()
		{
			// Arrange
			var parts = 5;

			// Act
			var result = _startTime.Split(_endTime, parts);

			// Assert
			result.Count().ShouldBe(parts);

			// verify that all parts are the same size
			var partSize = _startTime.Distance(_endTime) / parts;
			foreach (var part in result)
			{
				part.Distance().ShouldBe(partSize);
			}
		}

		/// <summary>
		/// Checks that the Split method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SplitWithstartTimeAndDistanceAndParts()
		{
			// Arrange
			var parts = 5;
			var distance = (_endTime-_startTime);

			// Act
			var result = _startTime.Split(distance, parts);

			// Assert
			result.Count().ShouldBe(parts);

			// verify that all parts are the same size
			var partSize = _startTime.Distance(_endTime) / parts;
			foreach (var part in result)
			{
				part.Distance().ShouldBe(partSize);
			}
		}

		/// <summary>
		/// Checks that the Split method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SplitWithDatesAndParts()
		{
			// Arrange
			var dates = new TimeOnlyRange(_startTime, _endTime);
			var parts = 5;

			// Act
			var result = dates.Split(parts);

			// Assert
			// verify that all parts are the same size
			var partSize = _startTime.Distance(_endTime) / parts;
			foreach (var part in result)
			{
				part.Distance().ShouldBe(partSize);
			}
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithDatesAndParts_WithNullDates()
		{
			Should.Throw<ArgumentNullException>(() => default(TimeOnlyRange)!.Split(5));
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithDatesAndParts_WithZeroParts()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => new TimeOnlyRange(_startTime, _endTime).Split(0));
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithstartTimeAndendTimeAndParts_WithZeroParts()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => _startTime.Split(_endTime, 0));
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithstartTimeAndDistanceAndParts_WithZeroParts()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => _startTime.Split(TimeSpan.FromMinutes(10), 0));
		}

		/// <summary>
		/// Checks that the Split method throws when the dates parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_SplitWithstartTimeAndDistanceAndParts_WithDistanceLessThanParts()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => _startTime.Split(TimeSpan.FromTicks(5), 6));
		}

	}
}