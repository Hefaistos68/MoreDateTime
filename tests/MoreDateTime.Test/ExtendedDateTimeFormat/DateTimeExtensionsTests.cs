namespace ExtendedDateTimeFormat.Tests
{
	using System;

	using ExtendedDateTimeFormat;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime;
	using MoreDateTime.Extensions;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="DateTimeExtensions"/>.
	/// </summary>
	[TestClass]
	public class DateTimeExtensionsTests
	{
		/// <summary>
		/// Checks that the ToExtendedDateTime method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_ToExtendedDateTimeWithDateTime()
		{
			// Arrange
			var d = new DateTime(2020, 01, 01, 12, 30, 45);

			// Act
			var result = d.ToExtendedDateTime();

			// Assert
			result.ToString().ShouldBe("2020-01-01T12:30:45");
		}

		/// <summary>
		/// Checks that the ToExtendedDateTime method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_ToExtendedDateTimeWithDateOnly()
		{
			// Arrange
			var d = new DateOnly(2020, 01, 01 );

			// Act
			var result = d.ToExtendedDateTime();

			// Assert
			result.ToString().ShouldBe("2020-01-01");
		}

		/// <summary>
		/// Checks that the ToExtendedDateTime maps values from the input to the returned instance.
		/// </summary>
		[TestMethod]
		public void ToExtendedDateTimeWithDateOnly_PerformsMapping()
		{
			// Arrange
			var d = new DateOnly(2020, 01, 01 );

			// Act
			var result = d.ToExtendedDateTime();

			// Assert
			result.Year.ShouldBe(d.Year);
			result.Month.ShouldBe(d.Month);
			result.Day.ShouldBe(d.Day);
			result.DayOfWeek.ShouldBe(d.DayOfWeek);
		}

		/// <summary>
		/// Checks that the ToExtendedDateTime method for DateTimeOffset functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_ToExtendedDateTimeWithDateTimeOffset()
		{
			// Arrange
			var dtoUtc = new DateTimeOffset(2020, 05, 15, 10, 20, 30, TimeSpan.Zero);
			var dtoOffset = new DateTimeOffset(2020, 05, 15, 10, 20, 30, new TimeSpan(2, 30, 0));

			// Act
			var resultUtc = dtoUtc.ToExtendedDateTime();
			var resultOffset = dtoOffset.ToExtendedDateTime();
			var now = ExtendedDateTime.Now;

			// Assert
			resultUtc.Year.ShouldBe(2020);
			resultUtc.Month.ShouldBe(5);
			resultUtc.Day.ShouldBe(15);
			resultUtc.Hour.ShouldBe(10);
			resultUtc.Minute.ShouldBe(20);
			resultUtc.Second.ShouldBe(30);
			resultOffset.UtcOffset.ShouldBe(new TimeSpan(2, 30, 0));
			now.ShouldNotBeNull();
		}
	}
}
