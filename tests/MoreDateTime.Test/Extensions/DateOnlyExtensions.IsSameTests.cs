namespace MoreDateTime.Tests.Extensions
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateOnlyExtensionsTests
	{
		/// <summary>
		/// Checks that the IsSameDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameDay()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddHours(2);

			// Act
			var result = dt.IsSameDay(other);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsSameMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameMonth()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddDays(2);

			// Act
			var result = dt.IsSameMonth(other);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsSameWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameWeek()
		{
			// Arrange
			var dt = _startDate;
			var other1 = _startDate.AddDays(1);
			var other2 = _startDate.AddDays(3);

			// Act
			var result1 = dt.IsSameWeek(other1);
			var result2 = dt.IsSameWeek(other2);

			// Assert
			result1.ShouldBeTrue();
			result2.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsSameYear method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameYear()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddMonths(2);

			// Act
			var result = dt.IsSameYear(other);

			// Assert
			result.ShouldBeTrue();
		}
	}
}