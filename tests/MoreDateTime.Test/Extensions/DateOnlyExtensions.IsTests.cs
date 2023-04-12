namespace MoreDateTime.Tests.Extensions
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateOnlyExtensionsTests
	{
		private readonly DateTime _startDateTime = new DateTime(2020, 05, 15); // Friday

		private readonly DateTime _midDateTime = new DateTime(2021, 02, 20);   // Saturday
		private readonly DateTime _endDateTime = new DateTime(2021, 05, 14);   // Friday

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
			DateTime.Today.ToDateOnly().IsBetween(_startDate, _endDate).ShouldBeFalse();
			DateTime.Today.ToDateOnly().IsBetween(_startDate, DateOnly.MaxValue).ShouldBeTrue();
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
			DateTime.Today.ToDateOnly().IsWithin(_startDate, _endDate).ShouldBeFalse();
			DateTime.Today.ToDateOnly().IsWithin(_startDate, DateOnly.MaxValue).ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsWithin method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsWithin_DateTime()
		{
			// Arrange

			// Act / Assert
			_midDate.IsWithin(_startDateTime, _endDateTime).ShouldBeTrue();
			_startDate.IsWithin(_midDateTime, _endDateTime).ShouldBeFalse();
			Should.Throw(() => _midDate.IsWithin(_endDateTime, _startDateTime), typeof(ArgumentException));
			_endDate.IsWithin(_startDateTime, _midDateTime).ShouldBeFalse();
			DateTime.Today.ToDateOnly().IsWithin(_startDateTime, _endDateTime).ShouldBeFalse();
			DateTime.Today.ToDateOnly().IsWithin(_startDateTime, DateTime.MaxValue).ShouldBeTrue();
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
		/// Checks that the IsBetween method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsWithin_Integer()
		{
			// Arrange
			var me = 5;

			// Act
			me.IsWithin(1, 6).ShouldBeTrue();
			me.IsWithin(-5, 60000).ShouldBeTrue();
			Should.Throw(() => me.IsWithin(1000, 6), typeof(ArgumentException));
			me.IsWithin(10, 60).ShouldBeFalse();
			me.IsWithin(5, 6).ShouldBeTrue();
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