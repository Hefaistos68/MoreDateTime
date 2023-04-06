namespace MoreDateTime.Tests.Extensions
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateTimeExtensionsTests
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

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsEqualDownToDay method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToDay()
		{
			// Arrange
			var dt = _startDate;
			var other = dt.AddHours(1);

			// Act
			var result = dt.IsEqualDownToDay(other);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsEqualDownToHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToHour()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddMinutes(5);

			// Act
			var result = dt.IsEqualDownToHour(other);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsEqualDownToMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToMinute()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddSeconds(10);

			// Act
			var result = dt.IsEqualDownToMinute(other);

			// Assert
			result.ShouldBeTrue();
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

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsEqualDownToSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsEqualDownToSecond()
		{
			// Arrange
			var dt = _startDate;
			var other = _startDate.AddMilliseconds(100);

			// Act
			var result = dt.IsEqualDownToSecond(other);

			// Assert
			result.ShouldBeTrue();
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
			var result = dt.IsEqualDownToWeek(other);

			// Assert
			result.ShouldBeTrue();
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

			// Assert
			result.ShouldBeTrue();
		}
	}
}