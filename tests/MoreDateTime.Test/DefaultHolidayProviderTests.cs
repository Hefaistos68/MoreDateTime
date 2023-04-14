namespace MoreDateTime.Tests
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="DefaultHolidayProvider"/>.
	/// </summary>
	[TestClass]
	public class DefaultHolidayProviderTests
	{
		private DefaultHolidayProvider _testClass;

		/// <summary>
		/// Sets up the dependencies required for the tests for <see cref="DefaultHolidayProvider"/>.
		/// </summary>
		[TestInitialize]
		public void SetUp()
		{
			this._testClass = new DefaultHolidayProvider();
		}

		/// <summary>
		/// Checks that the IsPublicHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPublicHolidayWithDateTimeAndCultureInfo()
		{
			// Arrange
			var date = DateTime.UtcNow;
			var cultureInfo = CultureInfo.InvariantCulture;

			// Act
			var result = this._testClass.IsPublicHoliday(date, cultureInfo);

			// Assert
			Assert.Fail("Create or modify test");
		}

		/// <summary>
		/// Checks that the IsPublicHoliday method throws when the cultureInfo parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_IsPublicHolidayWithDateTimeAndCultureInfo_WithNullCultureInfo()
		{
			Should.Throw<ArgumentNullException>(() => this._testClass.IsPublicHoliday(DateTime.UtcNow, default(CultureInfo)));
		}

		/// <summary>
		/// Checks that the IsPublicHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPublicHolidayWithDateOnlyAndCultureInfo()
		{
			// Arrange
			var date = new DateOnly();
			var cultureInfo = CultureInfo.InvariantCulture;

			// Act
			var result = this._testClass.IsPublicHoliday(date, cultureInfo);

			// Assert
			Assert.Fail("Create or modify test");
		}

		/// <summary>
		/// Checks that the IsPublicHoliday method throws when the cultureInfo parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_IsPublicHolidayWithDateOnlyAndCultureInfo_WithNullCultureInfo()
		{
			Should.Throw<ArgumentNullException>(() => this._testClass.IsPublicHoliday(new DateOnly(), default(CultureInfo)));
		}

		/// <summary>
		/// Checks that the NumberOfKnownHolidays method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfKnownHolidays()
		{
			// Arrange
			var year = 205745105;
			var cultureInfo = CultureInfo.CurrentCulture;

			// Act
			var result = this._testClass.NumberOfKnownHolidays(year, cultureInfo);

			// Assert
			Assert.Fail("Create or modify test");
		}
	}
}