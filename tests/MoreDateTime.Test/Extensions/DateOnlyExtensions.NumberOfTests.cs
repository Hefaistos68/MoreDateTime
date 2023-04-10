namespace MoreDateTime.Tests.Extensions
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime.Extensions;

	using Shouldly;

	/// <inheritdoc/>
	public partial class DateOnlyExtensionsTests
	{
		/// <summary>
		/// Checks that the NumberOfDaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfDaysUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfDaysUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(364.0d);
		}

		/// <summary>
		/// Checks that the NumberOfDecadesUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfDecadesUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfDecadesUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(0.1d);
		}

		/// <summary>
		/// Checks that the NumberOfHolidaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfHolidaysUntil_WithDefaultHolidayProvider()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(null);

			// Act
			var result = _startDate.NumberOfHolidaysUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(_holidaysInStartDateToEndDateDefault);
		}

		/// <summary>
		/// Checks that the NumberOfHolidaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfHolidaysUntil_WithNagerDateHolidayProvider()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(new NagerHolidayProvider());

			// Act
			var result = _startDate.NumberOfHolidaysUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(_holidaysInStartDateToEndDateNagerDate);
		}

		/// <summary>
		/// Checks that the NumberOfHolidaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfHolidaysUntil_WithNullHolidayProvider()
		{
			// Arrange
			DateTimeExtensions.SetHolidayProvider(new NullHolidayProvider());

			// Act
			var result = _startDate.NumberOfHolidaysUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(_holidaysInStartDateToEndDateNull);
		}
		/// <summary>
		/// Checks that the NumberOfHoursUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfHoursUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfHoursUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(364.0d * 24.0d);
		}

		/// <summary>
		/// Checks that the NumberOfMillisecondsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfMillisecondsUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfMillisecondsUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(364.0d * 24.0d * 60.0d * 60.0d * 1000.0d);
		}

		/// <summary>
		/// Checks that the NumberOfMinutesUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfMinutesUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfMinutesUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(364.0d * 24.0d * 60.0d);
		}

		/// <summary>
		/// Checks that the NumberOfMonthsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfMonthsUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfMonthsUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(12.0d);
		}

		/// <summary>
		/// Checks that the NumberOfSecondsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfSecondsUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfSecondsUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(364.0d * 24.0d * 60.0d * 60.0d);
		}

		/// <summary>
		/// Checks that the NumberOfSemestersUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfSemestersUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfSemestersUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(2.0d);
		}

		/// <summary>
		/// Checks that the NumberOfSemestersUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfTrimestersUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfTrimestersUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(4.0d);
		}


		/// <summary>
		/// Checks that the NumberOfWeekendsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfWeekendsUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfWeekendsUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(53.0d);
		}

		/// <summary>
		/// Checks that the NumberOfWeeksUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfWeeksUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfWeeksUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(52.0d);
		}

		/// <summary>
		/// Checks that the NumberOfWorkdaysUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfWorkdaysUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfWorkdaysUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(261.0d);
		}

		/// <summary>
		/// Checks that the NumberOfYearsUntil method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_NumberOfYearsUntil()
		{
			// Arrange

			// Act
			var result = _startDate.NumberOfYearsUntil(_endDate, _cultureInfo);

			// Assert
			result.ShouldBe(1.0d);
		}
	}
}