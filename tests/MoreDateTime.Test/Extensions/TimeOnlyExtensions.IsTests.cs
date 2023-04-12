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
		/// Checks that the IsAfternoon method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsAfternoon()
		{
			// Arrange
			var dt1 = _startTime.Add(new TimeSpan(15, 0, 0));
			var dt2 = _startTime.Add(new TimeSpan(10, 0, 0));
			var dt3 = _startTime.Add(new TimeSpan(3, 0, 0));

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
			_midTime.IsBetween(_startTime, _endTime).ShouldBeTrue();
			_startTime.IsBetween(_midTime, _endTime).ShouldBeFalse();
			_endTime.IsBetween(_startTime, _midTime).ShouldBeFalse();
			DateTime.Today.IsBetween(_startTime, _endTime).ShouldBeFalse();
			new DateTime(2020, 5, 1, 20, 21, 22, 250).IsBetween(_startTime, TimeOnly.MaxValue).ShouldBeTrue();
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
		/// Checks that the IsMidday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsMidday()
		{
			// Arrange
			var dt1 = _startTime.Add(new TimeSpan(11, 0, 0));
			var dt2 = _startTime.Add(new TimeSpan(15, 0, 0));
			var dt3 = _startTime.Add(new TimeSpan(3, 0, 0));

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
			var dt1 = _startTime.Add(new TimeSpan(-1, 10, 0));
			var dt2 = _startTime.Add(new TimeSpan(23, 59, 59));
			var dt3 = _startTime.Add(new TimeSpan(3, 0, 0));

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
			var dt1 = _startTime.Add(new TimeSpan(10, 0, 0));
			var dt2 = _startTime.Add(new TimeSpan(15, 0, 0));
			var dt3 = _startTime.Add(new TimeSpan(3, 0, 0));

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
			var dt1 = _startTime.Add(new TimeSpan(3, 0, 0));
			var dt2 = _startTime.Add(new TimeSpan(21, 0, 0));
			var dt3 = _startTime.Add(new TimeSpan(10, 0, 0));

			// Act/Assert
			dt1.IsNight().ShouldBeTrue();
			dt2.IsNight().ShouldBeTrue();
			dt3.IsNight().ShouldBeFalse();
		}
		/// <summary>
		/// Checks that the IsFullHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsFullHour()
		{
			// Arrange

			// Act
			var result1 = _startTime.IsFullHour();
			var result2 = _startTime.IsFullHour(false);
			var result3 = new TimeOnly(5, 0, 0, 250).IsFullHour(true);
			var result4 = new TimeOnly(5, 0, 0, 250).IsFullHour(false);

			// Assert
			result1.ShouldBeFalse();
			result2.ShouldBeFalse();
			result3.ShouldBeTrue();
			result4.ShouldBeFalse();

		}
		/// <summary>
		/// Checks that the IsFullMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsFullMinute()
		{
			// Arrange

			// Act
			var result1 = _startTime.IsFullMinute();
			var result2 = _startTime.IsFullMinute(false);
			var result3 = new TimeOnly(5, 5, 0, 250).IsFullMinute(true);
			var result4 = new TimeOnly(5, 5, 0, 250).IsFullMinute(false);

			// Assert
			result1.ShouldBeFalse();
			result2.ShouldBeFalse();
			result3.ShouldBeTrue();
			result4.ShouldBeFalse();
		}
		/// <summary>
		/// Checks that the IsWithin method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsWithin_WithTimeOnly()
		{
			// Arrange

			// Act
			var result1 = _midTime.IsWithin(_startTime, _endTime);
			var result2 = _midTime.IsWithin(_startTime, _midTime.PreviousMinute());

			// Assert
			result1.ShouldBeTrue();
			result2.ShouldBeFalse();
		}
	}
}