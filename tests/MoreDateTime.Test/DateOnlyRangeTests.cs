namespace MoreDateTime.Tests
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime;
	using MoreDateTime.Extensions;
	using MoreDateTime.Interfaces;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="DateOnlyRange"/>.
	/// </summary>
	[TestClass]
	public class DateOnlyRangeTests
	{
		private DateOnlyRange _testClass;
		private readonly DateOnly _startDate = new DateOnly(2020, 05, 15); // Friday

		private readonly DateOnly _midDate = new DateOnly(2021, 02, 20);   // Saturday
		private readonly DateOnly _endDate = new DateOnly(2021, 05, 14);   // Friday

		/// <summary>
		/// Creates the date time range.
		/// </summary>
		/// <returns>A DateOnlyRange.</returns>
		private DateOnlyRange CreateDateTimeRange()
		{
			return new DateOnlyRange(_startDate, _endDate);
		}

		/// <summary>
		/// Sets the up.
		/// </summary>
		[TestInitialize]
		public void SetUp()
		{
			_testClass = CreateDateTimeRange();
		}

		/// <summary>
		/// Checks that instance construction works.
		/// </summary>
		[TestMethod]
		public void CanConstruct()
		{
			// Act
			var instance = new DateOnlyRange(this._startDate, this._endDate);

			// Assert
			instance.ShouldNotBeNull();
		}

		/// <summary>
		/// Checks that the Distance method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Distance()
		{
			// Act
			var result = this._testClass.Distance();

			// Assert
			result.ShouldBe(_testClass.End.ToDateTime() - _testClass.Start.ToDateTime());
		}

		/// <summary>
		/// Checks that the Offset method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Offset()
		{
			// Arrange
			var timeSpan = TimeSpan.FromDays(60);

			// Act
			var result = this._testClass.Offset(timeSpan);

			// Assert
			result.Start.ShouldBe(_testClass.Start.Add(timeSpan));
			result.End.ShouldBe(_testClass.End.Add(timeSpan));
		}

		/// <summary>
		/// Checks that the Extend method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Extend_FromStart()
		{
			// Arrange
			var timeSpan = TimeSpan.FromDays(60);
			var direction = RangeDirection.Start;

			// Act
			var result = _testClass.Extend(timeSpan, direction);

			// Assert
			result.Start.ShouldBe(_testClass.Start.Sub(timeSpan));
			result.End.ShouldBe(_testClass.End);
		}

		/// <summary>
		/// Checks that the Extend method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Extend_FromEnd()
		{
			// Arrange
			var timeSpan = TimeSpan.FromDays(60);
			var direction = RangeDirection.End;

			// Act
			var result = _testClass.Extend(timeSpan, direction);

			// Assert
			result.Start.ShouldBe(_testClass.Start);
			result.End.ShouldBe(_testClass.End.Add(timeSpan));
		}

		/// <summary>
		/// Checks that the Extend method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Extend_FromBoth()
		{
			// Arrange
			var timeSpan = TimeSpan.FromDays(60);
			var direction = RangeDirection.Both;

			// Act
			var result = _testClass.Extend(timeSpan, direction);

			// Assert
			result.Start.ShouldBe(_testClass.Start.Sub(timeSpan / 2));
			result.End.ShouldBe(_testClass.End.Add(timeSpan / 2));
		}

		/// <summary>
		/// Checks that the Reduce method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Reduce_FromStart()
		{
			// Arrange
			var timeSpan = TimeSpan.FromDays(60);
			var direction = RangeDirection.Start;

			// Act
			var result = _testClass.Reduce(timeSpan, direction);

			// Assert
			result.Start.ShouldBe(_testClass.Start.Add(timeSpan));
			result.End.ShouldBe(_testClass.End);
		}

		/// <summary>
		/// Checks that the Reduce method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Reduce_FromEnd()
		{
			// Arrange
			var timeSpan = TimeSpan.FromDays(60);
			var direction = RangeDirection.End;

			// Act
			var result = _testClass.Reduce(timeSpan, direction);

			// Assert
			result.Start.ShouldBe(_testClass.Start);
			result.End.ShouldBe(_testClass.End.Sub(timeSpan));
		}

		/// <summary>
		/// Checks that the Reduce method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Reduce_FromBoth()
		{
			// Arrange
			var timeSpan = TimeSpan.FromDays(60);
			var direction = RangeDirection.Both;

			// Act
			var result = _testClass.Reduce(timeSpan, direction);

			// Assert
			result.Start.ShouldBe(_testClass.Start.Add(timeSpan / 2));
			result.End.ShouldBe(_testClass.End.Sub(timeSpan / 2));
		}

		/// <summary>
		/// Checks that the Start property can be read from and written to.
		/// </summary>
		[TestMethod]
		public void CanSetAndGet_Start()
		{
			// Arrange
			var testValue = DateTime.UtcNow.ToDateOnly();

			// Act
			this._testClass.Start = testValue;

			// Assert
			this._testClass.Start.ShouldBe(testValue);
		}

		/// <summary>
		/// Checks that the End property can be read from and written to.
		/// </summary>
		[TestMethod]
		public void CanSetAndGet_End()
		{
			// Arrange
			var testValue = DateTime.UtcNow.ToDateOnly();

			// Act
			this._testClass.End = testValue;

			// Assert
			this._testClass.End.ShouldBe(testValue);
		}
	}
}