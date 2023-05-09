using Microsoft.VisualStudio.TestTools.UnitTesting;

using MoreDateTime;

using Shouldly;

namespace ExtendedDateTimeFormat.Tests
{
	/// <summary>
	/// The extended date time calculator tests.
	/// </summary>
	[TestClass]
	public class ExtendedDateTimeCalculatorTests
	{
		/// <summary>
		/// Verifies that the AddMonth() method calculates the correct values
		/// </summary>
		/// <param name="expected">The expected.</param>
		/// <param name="date">The date.</param>
		/// <param name="months">The months.</param>
		[DataRow("2012-02-02", "2012-02-02", 0)]
		[DataRow("2012-07-02", "2012-02-02", 5)]
		[DataRow("2013-05-02", "2012-02-02", 15)]       // 11 months = 2012-02-02 to 2013-01-02 + 4 months = 2013-01-02 to 2013-05-02
		[DataRow("2014-08-02", "2012-02-02", 30)]       // 12 months = 2012-02-02 to 2013-02-02 + 12 months = 2013-02-02 to 2014-02-02 + 6 months = 2014-02-02 to 2014-08-02

		[TestMethod]
		public void CalculatesCorrectAddMonths(string expected, string date, int months)
		{
			var firstDate = new ExtendedDateTime(date);
			var result = firstDate.AddMonths(months);

			result.ToString().ShouldBe(expected);
		}

		/// <summary>
		/// Verifies that the AddYears() method calculates the correct values
		/// </summary>
		/// <param name="expected">The expected.</param>
		/// <param name="date">The date.</param>
		/// <param name="years">The years.</param>
		[DataRow("2012-02-02", "2012-02-02", 0)]
		[DataRow("2017-02-02", "2012-02-02", 5)]
		[DataRow("2027-02-02", "2012-02-02", 15)]
		[DataRow("2042-02-02", "2012-02-02", 30)]

		[TestMethod]
		public void CalculatesCorrectAddYears(string expected, string date, int years)
		{
			var firstDate = new ExtendedDateTime(date);
			var result = firstDate.AddYears(years);

			result.ToString().ShouldBe(expected);
		}

		/// <summary>
		/// Verifies that timespan substraction calculates the correct values
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="h">The h.</param>
		/// <param name="m">The m.</param>
		/// <param name="s">The s.</param>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		[DataRow(0, 0, 0, 0, "2012", "2012")]
		[DataRow(1096, 0, 0, 0, "2015", "2012")]       // 366 for 2012 (leap year) + 365 for 2013 + 365 for 2014 = 1096 days
		[DataRow(0, 0, 0, 0, "2012-01", "2012-01")]
		[DataRow(31, 0, 0, 0, "2012-02", "2012-01")]
		[DataRow(365, 0, 0, 0, "2013-03", "2012-03")]      // 31 days for 3/2012 + 30 days for 4/2012 + 31 days for 5/2012 + 30 days for 6/2012 + 31 days for 7/2012 + 31 days for 8/2012 + 30 days for 9/2012 + 31 days for 10/2012 + 30 days for 11/2012 + 31 days for 12/2012 + 31 days for 1/2013 + 28 days for 2/2013 = 365 days
		[DataRow(0, 0, 0, 0, "2012-02-02", "2012-02-02")]
		[DataRow(292, 0, 0, 0, "2012-11-20", "2012-02-02")]      // 28 days remaining in of February + 31 days in March + 30 days in April + 31 days in May + 30 days in June + 31 days in July + 31 days in August + 30 days in September + 31 days in October + 19 days passed into November = 292 days
		[DataRow(0, 0, 0, 0, "2012-03-03T03Z", "2012-03-03T03Z")]
		[DataRow(292, 18, 0, 0, "2012-11-20T20Z", "2012-02-02T02Z")]       // 20 additional hours passed after the end day - 2 hours in to the beginning day = 18 additional hours
		[DataRow(0, 0, 0, 0, "2012-03-03T03:03Z", "2012-03-03T03:03Z")]
		[DataRow(292, 18, 18, 0, "2012-11-20T20:20Z", "2012-02-02T02:02Z")]        // 20 additional minutes passed after the end hour - 2 minutes in to the beginning hour = 18 additional minutes
		[DataRow(0, 0, 0, 0, "2012-03-03T03:03:03Z", "2012-03-03T03:03:03Z")]
		[DataRow(292, 18, 18, 18, "2012-11-20T20:20:20Z", "2012-02-02T02:02:02Z")]      // 20 additional seconds passed after the end minute - 2 seconds in to the beginning minute = 18 additional seconds
		[DataRow(291, 16, 0, 0, "2012-11-20T00:00:00-08:00", "2012-02-02T00:00:00Z")]      // 28 days remaining in of February + 31 days in March + 30 days in April + 31 days in May + 30 days in June + 31 days in July + 31 days in August + 30 days in September + 31 days in October + 19 days passed into November - 8 hours behind = 291.16 days

		[TestMethod]
		public void CalculatesCorrectDifference(int d, int h, int m, int s, string first, string second)
		{
			var firstDate = new ExtendedDateTime(first);
			var secondDate = new ExtendedDateTime(second);
			var result = new TimeSpan(d, h, m, s);

			var diff = firstDate - secondDate;

			diff.ShouldBe(result);
		}
	}
}
