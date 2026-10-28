namespace MoreDateTime.Tests.Extensions
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateTimeExtensionsTests
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
			var falseResult = dt.IsSameDay(_startDate.AddDays(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsSameHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameHour()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddMinutes(5);

			// Act
			var result = dt.IsSameHour(other);
			var falseResult = dt.IsSameHour(_startDate.AddHours(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsSameMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameMinute()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddSeconds(5);

			// Act
			var result = dt.IsSameMinute(other);
			var falseResult = dt.IsSameMinute(_startDate.AddMinutes(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
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
			var falseResult = dt.IsSameMonth(_startDate.AddMonths(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsSameSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameSecond()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddMilliseconds(2);

			// Act
			var result = dt.IsSameSecond(other);
			var falseResult = dt.IsSameSecond(_startDate.AddSeconds(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
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
			var falseResult = dt.IsSameYear(_startDate.AddYears(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
		}
	}
}
