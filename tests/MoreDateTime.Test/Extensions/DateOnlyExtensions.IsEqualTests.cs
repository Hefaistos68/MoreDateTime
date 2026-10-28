namespace MoreDateTime.Tests.Extensions
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateOnlyExtensionsTests
	{
		/// <summary>
		/// Checks that the IsEqual method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqual()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddDays(-2);
			var truncateTo = DateTimeExtensions.DateTruncate.Week;

			// Act
			var result = dt.IsEqual(other, truncateTo);
			var invalidTruncateResult = dt.IsEqual(other, DateTimeExtensions.DateTruncate.Hour);

			// Assert
			result.ShouldBeTrue();
			invalidTruncateResult.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsEqualDownToMonth method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToMonth()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddDays(5);

			// Act
			var result = dt.IsEqualDownToMonth(other);
			var falseResult = dt.IsEqualDownToMonth(_startDate.AddMonths(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsEqualDownToDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToDay()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddHours(5);

			// Act
			var result = dt.IsEqualDownToDay(other);
			var falseResult = dt.IsEqualDownToDay(_startDate.AddDays(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsEqualDownToWeek method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToWeek()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddDays(-3);

			// Act
			var result = dt.IsEqualDownToWeek(other, _cultureInfo);
			var falseResult = dt.IsEqualDownToWeek(_startDate.AddDays(7), _cultureInfo);
			var falseYearResult = dt.IsEqualDownToWeek(_startDate.AddYears(1), _cultureInfo);

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
			falseYearResult.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsEqualDownToYear method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToYear()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddMonths(2).AddDays(2);

			// Act
			var result = dt.IsEqualDownToYear(other);
			var falseResult = dt.IsEqualDownToYear(_startDate.AddYears(1));

			// Assert
			result.ShouldBeTrue();
			falseResult.ShouldBeFalse();
		}
	}
}
