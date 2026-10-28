namespace MoreDateTime.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MoreDateTime;
    using MoreDateTime.Extensions;
    using MoreDateTime.Interfaces;
    using Shouldly;

    /// <summary>
    /// The date time range tests.
    /// </summary>
    [TestClass]
    public class DateTimeRangeTests
    {
        private readonly DateTime _startDate = new DateTime(2000, 05, 15, 6, 12, 30);
        private readonly DateTime _endDate = new DateTime(2001, 02, 20, 9, 30, 59);

        private DateTimeRange _testClass = null!;

        /// <summary>
        /// Creates the date time range.
        /// </summary>
        /// <returns>A DateTimeRange.</returns>
        private DateTimeRange CreateDateTimeRange()
        {
            return new DateTimeRange(_startDate, _endDate);
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
        /// Checks that default instance construction works.
        /// </summary>
        [TestMethod]
        public void CanConstruct_Default()
        {
            // Act
            var instance = new DateTimeRange();

            // Assert
            instance.ShouldNotBeNull();
            instance.Start.ShouldBe(default);
            instance.End.ShouldBe(default);
            instance.IsEmpty.ShouldBeTrue();
        }

        /// <summary>
        /// Checks that instance construction works.
        /// </summary>
        [TestMethod]
        public void CanConstruct_FromTwoDateTime()
        {
            // Act
            var instance = new DateTimeRange(this._startDate, this._endDate);

            // Assert
            instance.ShouldNotBeNull();
            instance.Start.ShouldBe(_startDate);
            instance.End.ShouldBe(_endDate);

        }

        /// <summary>
        /// Checks that instance construction works.
        /// </summary>
        [TestMethod]
        public void CanConstruct_FromTwoDateOnly()
        {
            // Act
            var instance = new DateTimeRange(this._startDate.ToDateOnly(), this._endDate.ToDateOnly());

            // Assert
            instance.ShouldNotBeNull();
            instance.Start.ShouldBe(_startDate.Date);
            instance.End.ShouldBe(_endDate.Date);
        }

        /// <summary>
        /// Checks that instance construction works.
        /// </summary>
        [TestMethod]
        public void CanConstruct_FromDateTimeRange()
        {
            // Arrange
            var instance = new DateTimeRange(this._startDate, this._endDate);

            // Act
            var instance2 = new DateTimeRange(instance);

            // Assert
            instance2.ShouldNotBeNull();
            instance2.ShouldBeEquivalentTo(instance);
            instance2.Start.ShouldBe(_startDate);
            instance2.End.ShouldBe(_endDate);

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
            result.ShouldBe(_testClass.End - _testClass.Start);
        }

        /// <summary>
        /// Checks that the Offset method functions correctly.
        /// </summary>
        [TestMethod]
        public void CanCall_Offset()
        {
            // Arrange
            var timeSpan = TimeSpan.FromSeconds(60);

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
            var timeSpan = TimeSpan.FromSeconds(60);
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
            var timeSpan = TimeSpan.FromSeconds(60);
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
            var timeSpan = TimeSpan.FromSeconds(60);
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
            var timeSpan = TimeSpan.FromSeconds(60);
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
            var timeSpan = TimeSpan.FromSeconds(60);
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
            var timeSpan = TimeSpan.FromSeconds(60);
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
            var testValue = DateTime.UtcNow;

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
            var testValue = DateTime.UtcNow;

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
            Should.Throw<ArgumentOutOfRangeException>(() => this._testClass.Extend(TimeSpan.FromSeconds(60), (RangeDirection)99));
        }

        /// <summary>
        /// Checks that calling Reduce with invalid direction throws an exception.
        /// </summary>
        [TestMethod]
        public void CannotCall_ReduceWithInvalidDirection()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => this._testClass.Reduce(TimeSpan.FromSeconds(60), (RangeDirection)99));
        }

        /// <summary>
        /// Checks that calling Reduce with invalid direction throws an exception.
        /// </summary>
        [TestMethod]
        public void CannotCall_ReduceWithTimeSpanLessThanDistance()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => this._testClass.Reduce(TimeSpan.FromDays(400), RangeDirection.Both));
            Should.Throw<ArgumentOutOfRangeException>(() => this._testClass.Reduce(this._testClass.Distance(), RangeDirection.Both));
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
            var result = new DateTimeRange(_endDate, _startDate).IsOrdered();

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
            var range = new DateTimeRange(_endDate, _startDate);

            // Act
            var orderedFromOrdered = this._testClass.Order();
            var orderedFromUnordered = range.Order();

            // Assert
            orderedFromOrdered.ShouldBeEquivalentTo(new DateTimeRange(_startDate, _endDate));
            orderedFromUnordered.ShouldBeEquivalentTo(new DateTimeRange(_startDate, _endDate));
        }

        /// <summary>
        /// Checks that the Contains method functions correctly.
        /// </summary>
        [TestMethod]
        public void CanCall_ContainsWithDateTime()
        {
            // Arrange
            var value = _endDate;

            // Act
            var result1 = this._testClass.Contains(value);
            var result2 = this._testClass.Contains(value.AddDays(2));
            var result3 = this._testClass.Contains(_startDate.AddDays(-1));

            // Assert
            result1.ShouldBeTrue();
            result2.ShouldBeFalse();
            result3.ShouldBeFalse();
        }

        /// <summary>
        /// Checks that the Contains method functions correctly.
        /// </summary>
        [TestMethod]
        public void CanCall_ContainsWithDateOnly()
        {
            // Arrange
            var value = _endDate.ToDateOnly();

            // Act
            var result1 = this._testClass.Contains(value);
            var result2 = this._testClass.Contains(value.AddDays(2));
            var result3 = this._testClass.Contains(_startDate.ToDateOnly().AddDays(-1));

            // Assert
            result1.ShouldBeTrue();
            result2.ShouldBeFalse();
            result3.ShouldBeFalse();
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
            result.Start.ShouldBe(DateTime.MinValue);
            result.End.ShouldBe(DateTime.MinValue);
            result.IsEmpty.ShouldBeTrue();
            this._testClass.IsEmpty.ShouldBeFalse();
            new DateTimeRange(default, _endDate).IsEmpty.ShouldBeFalse();
            new DateTimeRange(_startDate, default).IsEmpty.ShouldBeFalse();
        }

        /// <summary>
        /// Checks that the IsWithin method functions correctly.
        /// </summary>
        [TestMethod]
        public void CanCall_IsWithin()
        {
            // Arrange
            var inner = new DateTimeRange(_startDate.AddDays(1), _endDate.AddDays(-1));
            var outer = new DateTimeRange(_startDate.AddDays(-1), _endDate.AddDays(1));
            var partialEnd = new DateTimeRange(_startDate.AddDays(5), _endDate.AddDays(1));

            // Act & Assert
            inner.IsWithin(this._testClass).ShouldBeTrue();
            this._testClass.IsWithin(this._testClass).ShouldBeTrue();
            outer.IsWithin(this._testClass).ShouldBeFalse();
            partialEnd.IsWithin(this._testClass).ShouldBeFalse();
        }
	}
}
