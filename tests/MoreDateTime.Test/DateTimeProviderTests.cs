namespace MoreDateTime.Tests
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime;
	using MoreDateTime.Interfaces;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="DateTimeProvider"/>.
	/// </summary>
	[TestClass]
	public class DateTimeProviderTests
	{
		private DateTimeProvider _testClass = null!;
		private bool _nowIsUtc;
		private DateTime _datetimeNow = new DateTime(2017, 1, 2, 3, 4, 5, DateTimeKind.Local);
		private DateTime _datetimeUtc = new DateTime(2018, 2, 3, 4, 5, 6, DateTimeKind.Utc);

		/// <summary>
		/// Sets up the dependencies required for the tests for <see cref="DateTimeProvider"/>.
		/// </summary>
		[TestInitialize]
		public void SetUp()
		{
			this._nowIsUtc = true;
			this._testClass = new DateTimeProvider(this._nowIsUtc);
		}

		/// <summary>
		/// Checks that instance construction works.
		/// </summary>
		[TestMethod]
		public void CanConstruct()
		{
			// Act
			var instance = new DateTimeProvider(this._nowIsUtc);

			// Assert
			instance.ShouldNotBeNull();
		}

		/// <summary>
		/// Checks that the SetMockDateTime method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SetMockDateTime()
		{
			// Arrange
			var dtNow = _datetimeNow;
			var dtUtc = _datetimeUtc;

			// Act
			DateTimeProvider.SetMockDateTime(dtNow, dtUtc);

			// Assert
			DateTimeProvider.Current!.UtcNow.ShouldBe(DateTimeProvider.Current.Now);
		}

		/// <summary>
		/// Checks that the SetUtcHandling method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_SetUtcHandling()
		{
			// Arrange
			var dtNow = _datetimeNow;
			var dtUtc = _datetimeUtc;
			DateTimeProvider.SetMockDateTime(dtNow, dtUtc);

			// Act
			this._testClass.SetUtcHandling(true);
			var result1 = DateTimeProvider.Current!.Now;
			this._testClass.SetUtcHandling(false);
			var result2 = DateTimeProvider.Current.Now;

			// Assert
			result1.ShouldBe(_datetimeUtc);
			result2.ShouldNotBe(_datetimeUtc);
		}

		/// <summary>
		/// Checks that the Current property can be read from.
		/// </summary>
		[TestMethod]
		public void CanGet_Current()
		{
			// Assert
			DateTimeProvider.Current.ShouldNotBeNull();
			DateTimeProvider.Current.ShouldBeAssignableTo<IDateTimeProvider>();
		}

		/// <summary>
		/// Checks that the Now property can be read from.
		/// </summary>
		[TestMethod]
		public void CanGet_Now()
		{
			// Assert
			this._testClass.Now.ShouldBeOfType<DateTime>();
		}

		/// <summary>
		/// Checks that the Today property can be read from.
		/// </summary>
		[TestMethod]
		public void CanGet_Today()
		{
			// Assert
			this._testClass.Today.ShouldBeOfType<DateTime>();
		}

		/// <summary>
		/// Checks that the UtcToday property can be read from.
		/// </summary>
		[TestMethod]
		public void CanGet_UtcToday()
		{
			// Assert
			this._testClass.UtcToday.ShouldBeOfType<DateTime>();
		}

		/// <summary>
		/// Checks that the UtcNow property can be read from.
		/// </summary>
		[TestMethod]
		public void CanGet_UtcNow()
		{
			// Assert
			this._testClass.UtcNow.ShouldBeOfType<DateTime>();
		}
	}
}