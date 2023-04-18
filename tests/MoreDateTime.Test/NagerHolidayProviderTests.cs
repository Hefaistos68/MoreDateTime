namespace MoreDateTime.Tests
{
	using System;
	using System.Globalization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime;
	using MoreDateTime.Interfaces;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="NagerHolidayProvider"/>.
	/// </summary>
	[TestClass]
	public class NagerHolidayProviderTests
	{
		private NagerHolidayProvider _testClass = null!;

		/// <summary>
		/// Sets up the dependencies required for the tests for <see cref="NagerHolidayProvider"/>.
		/// </summary>
		[TestInitialize]
		public void SetUp()
		{
			this._testClass = new NagerHolidayProvider();
		}

		/// <summary>
		/// Checks that the IsPublicHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPublicHolidayWithDateTimeAndCultureInfo()
		{
			// Arrange
			var date = new DateTime(2020, 5, 1);
			var cultureInfo = CultureInfo.GetCultureInfo("DE");

			// Act
			try
			{
				var result1 = this._testClass.IsPublicHoliday(date, cultureInfo);
				var result2 = this._testClass.IsPublicHoliday(new DateTime(2020, 1, 1), cultureInfo);
	
				// Assert
				result1.ShouldBeTrue();
				result2.ShouldBeFalse();
			}
			catch (Nager.Date.NoLicenseKeyException)
			{
				// can not test without license, so skip test	
			}
		}

		/// <summary>
		/// Checks that the IsPublicHoliday method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsPublicHolidayWithDateOnlyAndCultureInfo()
		{
			// Arrange
			var date = new DateTime(2020, 5, 1);
			var cultureInfo = CultureInfo.GetCultureInfo("DE");

			// Act
			try
			{
				var result1 = this._testClass.IsPublicHoliday(date, cultureInfo);
				var result2 = this._testClass.IsPublicHoliday(new DateOnly(2020, 2, 2), cultureInfo);
	
				// Assert
				result1.ShouldBeFalse();
				result2.ShouldBeFalse();
			}
			catch (Nager.Date.NoLicenseKeyException)
			{
				// can not test without license, so skip test
			}
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
			try
			{
				var result = ((IHolidayProvider)this._testClass).NumberOfKnownHolidays(year, cultureInfo);
	
				// Assert
				result.ShouldBe(0);
			}
			catch (Nager.Date.NoLicenseKeyException)
			{
				// can not test without license, so skip test
			}
		}
	}
}