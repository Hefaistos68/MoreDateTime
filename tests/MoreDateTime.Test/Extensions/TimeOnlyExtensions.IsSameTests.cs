namespace MoreDateTime.Tests.Extensions
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class TimeOnlyExtensionsTests
	{
		/// <summary>
		/// Checks that the IsSameHour method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameHour()
		{
			// Arrange
			var dt = _startTime;
			var other = _startTime.AddMinutes(5);

			// Act
			var result = dt.IsSameHour(other);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsSameMillisecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameMillisecond()
		{
			// Arrange
			var dt = _startTime;
			var other = _startTime.AddMinutes(5);

			// Act
			var result = dt.IsSameMillisecond(other);

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsSameMinute method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameMinute()
		{
			// Arrange
			var dt = _startTime;
			var other = _startTime.AddMilliseconds(5);

			// Act
			var result = dt.IsSameMinute(other);

			// Assert
			result.ShouldBeTrue();
		}
		/// <summary>
		/// Checks that the IsSameSecond method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsSameSecond()
		{
			// Arrange
			var dt = _startTime;
			var other = _startTime.AddMinutes(5);

			// Act
			var result = dt.IsSameSecond(other);

			// Assert
			result.ShouldBeTrue();
		}
	}
}