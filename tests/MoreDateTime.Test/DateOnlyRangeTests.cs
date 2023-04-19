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
	public partial class DateOnlyRangeTests
	{
		private DateOnlyRange _testClass = null!;
		private readonly DateOnly _startDate = new DateOnly(2020, 05, 15); // Friday

		private readonly DateOnly _midDate = new DateOnly(2021, 02, 20);   // Saturday
		private readonly DateOnly _endDate = new DateOnly(2021, 05, 14);   // Friday

		private readonly DateTime _startDateTime = new DateTime(2020, 05, 15, 2, 3, 4); // Friday

		private readonly DateTime _endDateTime = new DateTime(2021, 05, 14, 6, 7, 8);   // Friday

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
		public void CanConstruct_FromTwoDateOnly()
		{
			// Act
			var instance = new DateOnlyRange(this._startDate, this._endDate);

			// Assert
			instance.ShouldNotBeNull();
			instance.Start.ShouldBe(_startDate);
			instance.End.ShouldBe(_endDate);
		}

		/// <summary>
		/// Checks that instance construction works.
		/// </summary>
		[TestMethod]
		public void CanConstruct_FromTwoDateTime()
		{
			// Act
			var instance = new DateOnlyRange(this._startDateTime, this._endDateTime);

			// Assert
			instance.ShouldNotBeNull();
			instance.Start.ShouldBe(_startDateTime.ToDateOnly());
			instance.End.ShouldBe(_endDateTime.ToDateOnly());
		}

		/// <summary>
		/// Checks that instance construction works.
		/// </summary>
		[TestMethod]
		public void CanConstruct_FromDateOnlyRange()
		{
			// Act
			var instance = new DateOnlyRange(this._startDate, this._endDate);
			var instance2 = new DateOnlyRange(instance);

			// Assert
			instance2.ShouldNotBeNull();
			instance2.Start.ShouldBe(_startDate);
			instance2.End.ShouldBe(_endDate);
		}

		/// <summary>
		/// Checks that instance construction works.
		/// </summary>
		[TestMethod]
		public void CanConstruct_FromDateTimeRange()
		{
			// Act
			var instance = new DateTimeRange(this._startDateTime, this._endDateTime);
			var instance2 = new DateOnlyRange(instance);

			// Assert
			instance2.ShouldNotBeNull();
			instance2.Start.ShouldBe(_startDateTime.ToDateOnly());
			instance2.End.ShouldBe(_endDateTime.ToDateOnly());
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
		public void CanCall_Reduce_WithTimeSpanLessThanDay()
		{
			// Arrange
			var timeSpan = TimeSpan.FromMinutes(60);
			var direction = RangeDirection.Start;

			// Act
			var result = _testClass.Reduce(timeSpan, direction);

			// Assert
			result.ShouldBeEquivalentTo(_testClass);
		}

		/// <summary>
		/// Checks that the Reduce method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Extend_WithTimeSpanLessThanDay()
		{
			// Arrange
			var timeSpan = TimeSpan.FromMinutes(60);
			var direction = RangeDirection.Start;

			// Act
			var result = _testClass.Extend(timeSpan, direction);

			// Assert
			result.ShouldBeEquivalentTo(_testClass);
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
		
		/// <summary>
		/// Checks that calling Extend with invalid direction throws an exception.
		/// </summary>
		[TestMethod]
		public void CannotCall_ExtendWithInvalidDirection()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => this._testClass.Extend(TimeSpan.FromDays(60), (RangeDirection)99));
		}

		/// <summary>
		/// Checks that calling Reduce with invalid direction throws an exception.
		/// </summary>
		[TestMethod]
		public void CannotCall_ReduceWithInvalidDirection()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => this._testClass.Reduce(TimeSpan.FromDays(60), (RangeDirection)99));
		}

		/// <summary>
		/// Checks that calling Reduce with invalid direction throws an exception.
		/// </summary>
		[TestMethod]
		public void CannotCall_ReduceWithTimeSpanLessThanDistance()
		{
			Should.Throw<ArgumentOutOfRangeException>(() => this._testClass.Reduce(TimeSpan.FromDays(400), RangeDirection.Both));
		}

		/// <summary>
		/// Checks that the IsOrdered method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsOrdered_WithOrdered()
		{
			// Act
			var result = this._testClass.IsOrdered();

			// Assert
			result.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the IsOrdered method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_IsOrdered_WithUnordered()
		{
			// Act
			var result = new DateOnlyRange(_endDate, _startDate).IsOrdered();

			// Assert
			result.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the Order method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Order()
		{
			// Arrange
			var range = new DateOnlyRange(_endDate, _startDate);

			// Act
			var result = this._testClass.Order();

			// Assert
			result.ShouldBeEquivalentTo(new DateOnlyRange(_startDate, _endDate));
		}

		/// <summary>
		/// Checks that the Contains method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_ContainsWithDateTime()
		{
			// Arrange
			var value = _endDate.ToDateTime();

			// Act
			var result1 = this._testClass.Contains(value);
			var result2 = this._testClass.Contains(value.AddDays(2));

			// Assert
			result1.ShouldBeTrue();
			result2.ShouldBeFalse();
		}

		/// <summary>
		/// Checks that the Contains method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_ContainsWithDateOnly()
		{
			// Arrange
			var value = _endDate;

			// Act
			var result1 = this._testClass.Contains(value);
			var result2 = this._testClass.Contains(value.AddDays(2));

			// Assert
			result1.ShouldBeTrue();
			result2.ShouldBeFalse();
		}
		/// <summary>
		/// Checks that the Empty method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Empty()
		{
			// Act
			var result = this._testClass.Empty();

			// Assert
			result.Start.ShouldBe(DateOnly.MinValue);
			result.End.ShouldBe(DateOnly.MinValue);
		}

	}
}