namespace MoreDateTime.Tests
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime;
	using MoreDateTime.Interfaces;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="NullHolidayProvider"/>.
	/// </summary>
	[TestClass]
	public class NullHolidayProviderTests
	{
		private NullHolidayProvider _testClass = null!;

		/// <summary>
		/// Checks that the IsPublicHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPublicHolidayWithDateOnlyAndCultureInfo()
		{
			// Arrange
			var date = new DateOnly();
			var cultureInfo = CultureInfo.GetCultureInfo("DE");

			// Act
			var result1 = this._testClass.IsPublicHoliday(date, cultureInfo);
			var result2 = this._testClass.IsPublicHoliday(new DateOnly(2020, 2, 2), cultureInfo);

			// Assert
			result1.ShouldBeFalse();
			result2.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the IsPublicHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPublicHolidayWithDateTimeAndCultureInfo()
		{
			// Arrange
			var date = DateTime.UtcNow;
			// Arrange
			var cultureInfo = CultureInfo.GetCultureInfo("DE");

			// Act
			var result1 = this._testClass.IsPublicHoliday(date, cultureInfo);
			var result2 = this._testClass.IsPublicHoliday(new DateTime(2020, 2, 2), cultureInfo);

			// Assert
			result1.ShouldBeFalse();
			result2.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the NumberOfKnownHolidays method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfKnownHolidays()
		{
			// Arrange
			var year = 2020;
			var cultureInfo = CultureInfo.CurrentCulture;

			// Act
			var result = ((IHolidayProvider)this._testClass).NumberOfKnownHolidays(year, cultureInfo);

			// Assert
			result.ShouldBe(0);
		}

		/// <summary>
		/// Sets up the dependencies required for the tests for <see cref="NullHolidayProvider"/>.
		/// </summary>
		[TestInitialize]
		public void SetUp()
		{
			this._testClass = new NullHolidayProvider();
		}
	}
}