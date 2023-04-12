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
		/// Checks that the EnumerateDaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateDaysUntil()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddDays(10);

			// Act
			var result = from.EnumerateDaysUntil(to);

			// Assert
			result.Count().ShouldBe(10 + 1);
			var prev = result.First();
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.NextDay());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateDaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateDaysUntil_Backwards()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddDays(10);

			// Act
			var result = to.EnumerateDaysUntil(from);

			// Assert
			result.Count().ShouldBe(10 + 1);
			var prev = result.First();
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.PreviousDay());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateHolidaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateHolidaysUntil()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(new DefaultHolidayProvider());
			// Act
			var result = _startDate.EnumerateHolidaysUntil(_endDate, _cultureInfo);

			// Assert
			result.Count().ShouldBe((int)_holidaysInStartDateToEndDateDefault);
		}

		/// <summary>
		/// Checks that the EnumerateHolidaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateHolidaysUntil_Backwards()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(new DefaultHolidayProvider());
			// Act
			var result = _endDate.EnumerateHolidaysUntil(_startDate, _cultureInfo);

			// Assert
			result.Count().ShouldBe((int)_holidaysInStartDateToEndDateDefault);
		}

		/// <summary>
		/// Checks that the ToDateRanges method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddDays(10);
			var distance = new TimeSpan(DateTimeExtensions.TicksPerDay);

			// Act
			var result = from.EnumerateInStepsUntil(to, distance);

			// Assert
			var dif = to.Distance(from).Add(new TimeSpan(1,0,0,0)); // add one day, to make sure we get the last one
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
		public void CanCall_EnumerateInStepsUntil_WithDistance12Hour()
		{
			// Arrange
			var distance = TimeSpan.FromHours(12);

			// Act
			var result = _startDate.EnumerateInStepsUntil(_endDate, distance);

			// Assert
			result.Count().ShouldBe(_totalDaysInStartDateToEndDate * 2);
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithDistanceDay()
		{
			// Arrange
			var distance = TimeSpan.FromDays(1);

			// Act
			var result = _startDate.EnumerateInStepsUntil(_endDate, distance);

			// Assert
			result.Count().ShouldBe(_totalDaysInStartDateToEndDate);
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithDistanceDayBackwards()
		{
			// Arrange
			var distance = TimeSpan.FromDays(-1);

			// Act
			var result = _endDate.EnumerateInStepsUntil(_startDate, distance);

			// Assert
			result.Count().ShouldBe(_totalDaysInStartDateToEndDate);
		}

		/// <summary>
		/// Checks that the ToDateRanges method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithEvaluator()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddDays(10);
			var distance = new TimeSpan(DateTimeExtensions.TicksPerDay);

			// Act
			var result = _startDate.EnumerateInStepsUntil(_startDate.AddDays(10), distance, SkipMonday);

			// Assert
			result.Count().ShouldBe(9);

			foreach (var d in result)
			{
				d.DayOfWeek.ShouldNotBe(DayOfWeek.Monday);
			}

			bool SkipMonday(DateOnly arg)
			{
				return arg.DayOfWeek != DayOfWeek.Monday;
			}
		}
		/// <summary>
		/// Checks that the ToDateRanges method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateInStepsUntil_WithEvaluator_Backwards()
		{
			// Arrange
			var distance = new TimeSpan(DateTimeExtensions.TicksPerDay);

			// Act
			var result = _startDate.AddDays(10).EnumerateInStepsUntil(_startDate, -distance, SkipMonday);

			// Assert
			result.Count().ShouldBe(9);

			foreach (var d in result)
			{
				d.DayOfWeek.ShouldNotBe(DayOfWeek.Monday);
			}

			bool SkipMonday(DateOnly arg)
			{
				return arg.DayOfWeek != DayOfWeek.Monday;
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
			Func<DateOnly, bool> evaluator = x => false;

			// Act
			var result = _startDate.EnumerateInStepsUntil(_endDate, distance, evaluator);

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
			var distance = TimeSpan.FromDays(1);
			Func<DateOnly, bool> evaluator = x => true;

			// Act
			var result = _startDate.EnumerateInStepsUntil(_endDate, distance, evaluator);

			// Assert
			result.Count().ShouldBe(_totalDaysInStartDateToEndDate);
		}

		/// <summary>
		/// Checks that the EnumerateMonthsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateMonthsUntil()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddMonths(10);

			// Act
			var result = from.EnumerateMonthsUntil(to);

			// Assert
			result.Count().ShouldBe(10 + 1);
			
			var prev = result.First();
			
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.NextMonth());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateMonthsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateMonthsUntil_Backwards()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddMonths(10);

			// Act
			var result = to.EnumerateMonthsUntil(from);

			// Assert
			result.Count().ShouldBe(10 + 1);
			
			var prev = result.First();
			
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.PreviousMonth());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateWorkdaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateWorkdaysUntil()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddDays(7);

			// Act
			var result = from.EnumerateWorkdaysUntil(to, _cultureInfo);

			// Assert
			result.Count().ShouldBe(5 + 1);
			
			var prev = result.First();
			
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.NextWorkday());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateWorkdaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateWorkdaysUntil_Backwards()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddDays(7);

			// Act
			var result = to.EnumerateWorkdaysUntil(from, _cultureInfo);

			// Assert
			result.Count().ShouldBe(5 + 1);
			
			var prev = result.First();
			
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.PreviousWorkday());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateWeekendsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateWeekendsUntil()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddWeeks(5);

			// Act
			var result = from.EnumerateWeekendsUntil(to);

			// Assert
			result.Count().ShouldBe(5);
			
			var prev = result.First();
			
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.NextWeekend());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateWeekendsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateWeekendsUntil_Backwards()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddWeeks(5);

			// Act
			var result = to.EnumerateWeekendsUntil(from);

			// Assert
			result.Count().ShouldBe(5 );
			
			var prev = result.First();
			
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.PreviousWeekend());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateYearsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateYearsUntil()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddYears(10);

			// Act
			var result = from.EnumerateYearsUntil(to);

			// Assert
			result.Count().ShouldBe(10 + 1);
			
			var prev = result.First();
			
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.NextYear());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateYearsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_EnumerateYearsUntil_Backwards()
		{
			// Arrange
			var @from = _startDate;
			var to = _startDate.AddYears(10);

			// Act
			var result = to.EnumerateYearsUntil(from);

			// Assert
			result.Count().ShouldBe(10 + 1);
			
			var prev = result.First();
			
			foreach (var d in result.Skip(1))
			{
				d.ShouldBe(prev.PreviousYear());
				prev = d;
			}
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithDistanceGreaterThanDifference()
		{
			Should.Throw<ArgumentException>(() => _startDate.EnumerateInStepsUntil(_startDate.AddDays(1), TimeSpan.FromDays(10)).Count());
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithDistanceGreaterThanDifferenceAndEvaluator()
		{
			Should.Throw<ArgumentException>(() => _startDate.EnumerateInStepsUntil(_startDate.AddDays(1), TimeSpan.FromDays(10), (x) => true).Count());
			;
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithNegativeDistanceForward()
		{
			Should.Throw<ArgumentException>(() => _startDate.EnumerateInStepsUntil(_startDate.AddDays(10), TimeSpan.FromDays(-1)).Count());
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithPositiveDistanceAndEvaluatorBackward()
		{
			Should.Throw<ArgumentException>(() => _startDate.AddDays(10).EnumerateInStepsUntil(_startDate, TimeSpan.FromDays(1), (x) => true).Count());
			;
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithNegativeDistanceAndEvaluatorForward()
		{
			Should.Throw<ArgumentException>(() => _startDate.EnumerateInStepsUntil(_startDate.AddDays(10), TimeSpan.FromDays(-1), (x) => true).Count());
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithPositiveDistanceBackward()
		{
			Should.Throw<ArgumentException>(() => _startDate.AddDays(10).EnumerateInStepsUntil(_startDate, TimeSpan.FromDays(1)).Count());
			;
		}

		/// <summary>
		/// Checks that the EnumerateInStepsUntil method throws when the evaluator parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_EnumerateInStepsUntil_WithFromAndToAndDistanceAndEvaluator_WithNullEvaluator()
		{
			Should.Throw<ArgumentNullException>(() => _startDate.EnumerateInStepsUntil(_endDate, TimeSpan.FromDays(2), default(Func<DateOnly, bool>)!).Count());
		}
	}
}