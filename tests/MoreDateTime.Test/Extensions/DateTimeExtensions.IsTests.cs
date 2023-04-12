namespace MoreDateTime.Tests.Extensions
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateTimeExtensionsTests
	{
		/// <summary>
		/// Checks that the IsAfternoon method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsAfternoon()
		{
			// Arrange
			var dt1 = _startDate.Add(new TimeSpan(15, 0, 0));
			var dt2 = _startDate.Add(new TimeSpan(10, 0, 0));
			var dt3 = _startDate.Add(new TimeSpan(3, 0, 0));

			// Act/Assert
			dt1.IsAfternoon().ShouldBeTrue();
			dt2.IsAfternoon().ShouldBeFalse();
			dt3.IsAfternoon().ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsBetween method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsBetween_DateTime()
		{
			// Arrange

			// Act / Assert
			_midDate.IsBetween(_startDate, _endDate).ShouldBeTrue();
			_startDate.IsBetween(_midDate, _endDate).ShouldBeFalse();
			Should.Throw(() => _midDate.IsBetween(_endDate, _startDate), typeof(ArgumentException));
			_endDate.IsBetween(_startDate, _midDate).ShouldBeFalse();
			DateTime.Today.IsBetween(_startDate, _endDate).ShouldBeFalse();
			DateTime.Today.IsBetween(_startDate, DateTime.MaxValue).ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsBetween method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsBetween_Integer()
		{
			// Arrange
			var me = 5;

			// Act
			me.IsBetween(1, 6).ShouldBeTrue();
			me.IsBetween(-5, 60000).ShouldBeTrue();
			Should.Throw(() => me.IsBetween(1000, 6), typeof(ArgumentException));
			me.IsBetween(10, 60).ShouldBeFalse();
			me.IsBetween(5, 6).ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsWithin method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsWithin()
		{
			// Arrange

			// Act / Assert
			_midDate.IsWithin(_startDate, _endDate).ShouldBeTrue();
			_startDate.IsWithin(_midDate, _endDate).ShouldBeFalse();
			Should.Throw(() => _midDate.IsWithin(_endDate, _startDate), typeof(ArgumentException));
			_endDate.IsWithin(_startDate, _midDate).ShouldBeFalse();
			DateTime.Today.IsWithin(_startDate, _endDate).ShouldBeFalse();
			DateTime.Today.IsWithin(_startDate, DateTime.MaxValue).ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsWithin method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsWithin_DateOnly()
		{
			// Arrange

			// Act / Assert
			_midDate.IsWithin(_startDateOnly, _endDateOnly).ShouldBeTrue();
			_startDate.IsWithin(_midDateOnly, _endDateOnly).ShouldBeFalse();
			Should.Throw(() => _midDate.IsWithin(_endDateOnly, _startDateOnly), typeof(ArgumentException));
			_endDate.IsWithin(_startDateOnly, _midDateOnly).ShouldBeFalse();
			DateTime.Today.IsWithin(_startDateOnly, _endDateOnly).ShouldBeFalse();
			DateTime.Today.IsWithin(_startDateOnly, DateOnly.MaxValue).ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsMidday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsMidday()
		{
			// Arrange
			var dt1 = _startDate.Add(new TimeSpan(12, 0, 0));
			var dt2 = _startDate.Add(new TimeSpan(15, 0, 0));
			var dt3 = _startDate.Add(new TimeSpan(3, 0, 0));

			// Act/Assert
			dt1.IsMidday().ShouldBeTrue();
			dt2.IsMidday().ShouldBeFalse();
			dt3.IsMidday().ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsMidnight method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsMidnight()
		{
			// Arrange
			var dt1 = _startDate.Add(new TimeSpan(0, 10, 0));
			var dt2 = _startDate.Add(new TimeSpan(23, 59, 59));
			var dt3 = _startDate.Add(new TimeSpan(3, 0, 0));

			// Act/Assert
			dt1.IsMidnight().ShouldBeTrue();
			dt2.IsMidnight().ShouldBeFalse();
			dt3.IsMidnight().ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsMorning method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsMorning()
		{
			// Arrange
			var dt1 = _startDate.Add(new TimeSpan(10, 0, 0));
			var dt2 = _startDate.Add(new TimeSpan(15, 0, 0));
			var dt3 = _startDate.Add(new TimeSpan(3, 0, 0));

			// Act/Assert
			dt1.IsMorning().ShouldBeTrue();
			dt2.IsMorning().ShouldBeFalse();
			dt3.IsMorning().ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsNight method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsNight()
		{
			// Arrange
			var dt1 = _startDate.Add(new TimeSpan(3, 0, 0));
			var dt2 = _startDate.Add(new TimeSpan(21, 0, 0));
			var dt3 = _startDate.Add(new TimeSpan(10, 0, 0));

			// Act/Assert
			dt1.IsNight().ShouldBeTrue();
			dt2.IsNight().ShouldBeTrue();
			dt3.IsNight().ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsNight method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPublicHoliday()
		{
			// Arrange
			var dt1 = _startDate;
			var dt2 = new DateTime(2020, 12, 25);
			var dt3 = new DateTime(2020, 1, 1);

			DateTimeExtensions.SetHolidayProvider(new DefaultHolidayProvider());

			// Act/Assert
			dt1.IsPublicHoliday(_cultureInfo).ShouldBeFalse();
			dt2.IsPublicHoliday(_cultureInfo).ShouldBeTrue();
			dt3.IsPublicHoliday(_cultureInfo).ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsWeekend method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsWeekend()
		{
			// Arrange
			var dt1 = _startDate;
			var dt2 = _startDate.AddDays(1);

			// Act
			var result1 = dt1.IsWeekend();
			var result2 = dt2.IsWeekend();

			// Assert
			result1.ShouldBeFalse();
			result2.ShouldBeTrue();
		}
	}
}