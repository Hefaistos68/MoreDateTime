namespace MoreDateTime.Tests
{
	/// <summary>
	/// The date time range tests.
	/// </summary>
	[TestClass]
	public class DateTimeRangeTests
	{
		private readonly DateTime _startDate = new DateTime(2000, 05, 15);
		private readonly DateTime _endDate = new DateTime(2001, 02, 20);

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
			// Method intentionally left empty.
		}

		/// <summary>
		/// Tests the method1.
		/// </summary>
		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var dateTimeRange = this.CreateDateTimeRange();

			// Act

			// Assert
			Assert.AreEqual(dateTimeRange.StartTime, _startDate);
			Assert.AreEqual(dateTimeRange.EndTime, _endDate);
		}
	}
}
