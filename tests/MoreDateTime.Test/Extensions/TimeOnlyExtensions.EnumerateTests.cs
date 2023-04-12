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
		/// Checks that the ToDateRanges method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil()
		{
			// Arrange
			var @from = _startTime;
			var to = _startTime.AddHours(10);
			var distance = new TimeSpan(DateTimeExtensions.TicksPerHour);

			// Act
			var result = from.EnumerateInStepsUntil(to, distance);

			// Assert
			var dif = to.Distance(from).Add(new TimeSpan(1,0,0));
			long l = dif.Ticks / distance.Ticks;

			result.Count().ShouldBe((int)l);

			var prev = result.First();
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.Add(distance));

				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithDistance3Hour()
		{
			// Arrange
			var distance = TimeSpan.FromHours(3);

			// Act
			var result = _startTime.EnumerateInStepsUntil(_endTime, distance);

			// Assert
			result.Count().ShouldBe(4); // 1h, 4h, 7h, 10h are the steps
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithDistance3HourBackwards()
		{
			// Arrange
			var distance = TimeSpan.FromHours(-3);

			// Act
			var result = _endTime.EnumerateInStepsUntil(_startTime, distance);

			// Assert
			result.Count().ShouldBe(4); // 1h, 4h, 7h, 10h are the steps
		}

		/// <summary>
		/// Checks that the ToDateRanges method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithEvaluator()
		{
			// Arrange
			var distance = TimeSpan.FromHours(1);

			// Act
			var result = _startTime.EnumerateInStepsUntil(_endTime, distance, Skipper);

			// Assert
			result.Count().ShouldBe(_hoursInStartTimeToEndTime - 1);

			foreach (var d in result)
			{
				d.Hour.ShouldNotBe(5);
			}

			bool Skipper(TimeOnly arg)
			{
				return arg.Hour != 5;
			}
		}

		/// <summary>
		/// Checks that the ToDateRanges method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithEvaluatorBackwards()
		{
			// Arrange
			var distance = TimeSpan.FromHours(-1);

			// Act
			var result = _endTime.EnumerateInStepsUntil(_startTime, distance, Skipper);

			// Assert
			result.Count().ShouldBe(_hoursInStartTimeToEndTime - 1);

			foreach (var d in result)
			{
				d.Hour.ShouldNotBe(5);
			}

			bool Skipper(TimeOnly arg)
			{
				return arg.Hour != 5;
			}
		}

		/// <summary>
		/// Checks that the ToDateRanges method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithEvaluator_10minuteSteps()
		{
			// Arrange
			var distance = new TimeSpan(DateTimeExtensions.TicksPerMinute * 10);

			// Act
			var result = _startTime.EnumerateInStepsUntil(_startTime.AddHours(2), distance, Skipper);

			// Assert
			result.Count().ShouldBe(6 * 2 + 1);

			foreach (var d in result)
			{
				d.Minute.ShouldNotBe(0);
			}

			bool Skipper(TimeOnly arg)
			{
				return arg.Minute != 0;
			}
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithEvaluatorFalse()
		{
			// Arrange
			var distance = TimeSpan.FromSeconds(215);
			Func<TimeOnly, bool> evaluator = x => false;

			// Act
			var result = _startTime.EnumerateInStepsUntil(_endTime, distance, evaluator);

			// Assert
			result.Count().ShouldBe(0);
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithEvaluatorTrue()
		{
			// Arrange
			var distance = TimeSpan.FromHours(1);
			Func<TimeOnly, bool> evaluator = x => true;

			// Act
			var result = _startTime.EnumerateInStepsUntil(_endTime, distance, evaluator);

			// Assert
			result.Count().ShouldBe(_hoursInStartTimeToEndTime);
		}

		/// <summary>
		/// Checks that the ToDateRanges method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_CheckDistance()
		{
			// Arrange
			var @from = _startTime;
			var to = _startTime.AddHours(10);
			var distance = new TimeSpan(DateTimeExtensions.TicksPerHour);

			// Act
			var result = from.EnumerateInStepsUntil(to, distance);

			// Assert
			var dif = to.Distance(from).Add(new TimeSpan(1,0,0));
			long l = dif.Ticks / distance.Ticks;

			result.Count().ShouldBe((int)l);

			var prev = result.First();

			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.Add(distance));

				prev = d;
			}
		}
		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithDistanceGreaterThanDifference()
		{
			Should.Throw<ArgumentException>(() => _startTime.EnumerateInStepsUntil(_startTime.AddSeconds(1), TimeSpan.FromSeconds(10)).Count());
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithDistanceZero()
		{
			Should.Throw<ArgumentException>(() => _startTime.EnumerateInStepsUntil(_startTime.AddSeconds(1), TimeSpan.FromSeconds(0)).Count());
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithEvaluatorAndDistanceZero()
		{
			Should.Throw<ArgumentException>(() => _startTime.EnumerateInStepsUntil(_startTime.AddSeconds(1), TimeSpan.FromSeconds(0), (x) => true).Count());
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithFromAndToAndDistanceAndEvaluator_WithNullEvaluator()
		{
			Should.Throw<ArgumentNullException>(() => _startTime.EnumerateInStepsUntil(_endTime, TimeSpan.FromMinutes(2), default(Func<TimeOnly, bool>)!).Count());
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithDistanceGreaterThanDifferenceAndEvaluator()
		{
			Should.Throw<ArgumentException>(() => _startTime.EnumerateInStepsUntil(_startTime.AddMinutes(1), TimeSpan.FromMinutes(10), (x) => true).Count());
			;
		}
	}
}