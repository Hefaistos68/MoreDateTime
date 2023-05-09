namespace ExtendedDateTimeFormat.Tests
{
    using MoreDateTime;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using MoreDateTime;

    [TestClass]
	public class ISO8601_2_EDTF_Tests
	{
		// Date
		[DataRow("1985-04-12")]                                    // A.4.2.1 efers to the calendar date 1985 April 12th
		[DataRow("1985-04")]                                       // A.4.2.2 refers to the calendar date 1985 April with monthprecision.
		[DataRow("1985")]                                          // A.4.2.3 refers to the calendar date ofyear 1985 with year precision.

		[DataRow("-0999")]
		[DataRow("0000")]

		// Date and Time
		[DataRow("1985-04-12T23:20:30")]                           // A.4.3.1 refers to the date 1985 April 12th 23:20:30 in local timeofday.
		[DataRow("1985-04-12T23:20:30Z")]                          // A.4.3.2 refers to the date 1985 April 12th 23:20:30 in UTC of day.
		[DataRow("1985-04-12T23:20:30+04:30")]                     // A.4.3.3 refers to the date 1985 April 12th, time of day 23:20:30 with time shift of 4 hours and 30 minutes ahead of UTC.
		[DataRow("1985-04-12T23:20:30+04")]                        // A.4.3.4 refers to the date 1985 April 12th time of day 23:20:30 in a time of day with time shift of 4 hours ahead of UTC.

		// Time interval
		[DataRow("1964/2008")]                                     // A.4.4.1 is a time interval with calendar year precision, beginning sometime in 1964 ending sometimein 2008
		[DataRow("2004-06/2006-08")]								// A.4.4.2 is a time interval with calendar monthprecision, beginning sometimein June 2004 and ending sometime in August 2006.
		[DataRow("2004-02-01/2005-02-08")]                         // A.4.4.3 is a time interval with calendar day precision, beginning sometime on February1, 2004 and ending sometime on February8,2005.
		[DataRow("2004-02-01/2005-02")]                            // A.4.4.4 is a time interval beginning sometime on February 1, 2004 and ending sometime in February 2005. Note that the start endpoint precision(year) is different than the end endpoint (month)andtherefore the precision ofthe time intervalat large is undefined.
		[DataRow("2004-02-01/2005")]                               // A.4.4.5 is a time interval beginning sometime on February 1, 2004 and ending sometime in 2005. The start endpoint has calendar day precision and the end endpoint has calendar year precision. Similar to the previous example,the precision of the time interval at large is undefined.
		[DataRow("2005/2006-02")]                                  // A.4.4.6 is a time interval beginning sometime in 2005 and ending sometime February 2006

		[DataTestMethod]
		public void TestLevel0Compliance(string edtfString)
		{
			var result = ExtendedDateTimeFormatParser.Parse(edtfString);
			var sResult = result.ToString();
			sResult.ShouldBe(edtfString);
		}

		// Years Exceeding Four Digits
		[DataRow("Y170000002")]                                    // A.5.2.1 
		[DataRow("Y-1985")]                                        // A.5.2.2 

		// Season														
		[DataRow("2001-21")]									    // A.5.3.1 
		[DataRow("2001-22")]
		[DataRow("2001-23")]
		[DataRow("2001-30")]
		
		// qualification		
		[DataRow("1985-04-12?")]									// A.5.4.1
		[DataRow("1985-04?")]										// A.5.4.2
		[DataRow("1985~")]											// A.5.4.3

		// unspecified digits
		[DataRow("1985-04-XX")]									// A.5.5.1
		[DataRow("1985-XX-XX")]									// A.5.5.2
		[DataRow("2004-XX")]									// A.5.5.3
		[DataRow("201X")]										// A.5.5.4
		[DataRow("20XX")]                                      // A.5.5.4

		[DataRow("1985-04-12/..")]								// A.5.6.1
		[DataRow("../1985-04-12")]								// A.5.6.2
		[DataRow("1986-04/")]									// A.5.6.3
		[DataRow("/1985")]										// A.5.6.4
		[DataRow("1984?/2004%")]								// A.5.6.5
		[DataRow("1984-01-02~/2004-06-04")]						// A.5.6.6
		[DataRow("1984~/2004-06")]								// A.5.6.7
		[DataRow("../1985-04-12?")]								// A.5.6.8
		[DataRow("1985-04-12~/")]								// A.5.6.9

		[DataTestMethod]
		public void TestLevel1Compliance(string edtfString)
		{
			var result = ExtendedDateTimeFormatParser.Parse(edtfString);
			var sResult = result.ToString();
			sResult.ShouldBe(edtfString);
		}


		// Exponential Form of Years Exceeding Four Digits
		[DataRow("Y17E7")]										// A.6.2.1 the calendar year 17 x 107 = 170000000
		[DataRow("Y-17E7")]										// A.6.2.1 the calendar -17 x 107 = -170000000
		[DataRow("Y17101E4S3")]                                 // A.6.2.3 some year between 171010000 and 171010999, estimated to be 171010000

		// Significant digits
		[DataRow("1950S2")]                                    // A.6.3.1 a value between 1900 and 1999, estimated to be 1950
		[DataRow("Y171010000S3")]                              // A.6.3.2 some year between 171010000 and 171010999,estimated to be 171010000
		[DataRow("Y3388E2S3")]                                 // A.6.3.3 some year between 338000 and 338999, estimated to be 338800

		// Season														
		[DataRow("2001-34")]                                   // A.6.4.1 

		// Sets
		[DataRow("{1960,1961-12}")]										   // A.6.5.1 
		[DataRow("[1667,1760-12]")]										   // A.6.5.2 
		[DataRow("..1984")]												   // A.6.5.3 
		[DataRow("1984..")]												   // A.6.5.4 
		[DataRow("1670..1673")]											   // A.6.5.5 
		[DataRow("..1983-12-31,1984-10-10..1984-11-01,1984-11-05..")]     // A.6.5.6 

		// Qualification
		[DataRow("2004-06~-11")]										  // A.6.6.1
		[DataRow("2004?-06-11")]										  // A.6.6.2
		[DataRow("?2004-06-~11")]										  // A.6.6.3
		[DataRow("2004-%06-11")]										  // A.6.6.4
		[DataRow("2004-06-~01/2004-06-~20")]							  // A.6.6.5
		[DataRow("..2004-06-01/~2004-06-20")]							  // A.6.6.6
		[DataRow("2004-06-01~/2004-06-20..")]                            // A.6.6.7

		// Unspecified digits
		[DataRow("156X-12-25")]												// A.6.7.1
		[DataRow("XXXX-12-XX")]												// A.6.7.2
		[DataRow("1XXX-XX")]												// A.6.7.3
		[DataRow("1XXX-12")]												// A.6.7.4

		[DataTestMethod]
		public void TestLevel2Compliance(string edtfString)
		{
			var result = ExtendedDateTimeFormatParser.Parse(edtfString);
			var sResult = result.ToString();
			sResult.ShouldBe(edtfString);
		}

		// Uncertain and Approximate
		[DataRow("2004-06?")]
		[DataRow("2004-06-11?")]
		[DataRow("2004-06-11%")]
		[DataRow("1984?")]
		[DataRow("1984~")]
		[DataRow("1984%")]
		
		// quarter
		[DataRow("2001-33")]
		[DataRow("2001-36")]

		// semester
		[DataRow("2001-40")]

		// unspecified implicit
		[DataRow("156X-12-25")]
		[DataRow("15XX-12-25")]
		[DataRow("1XXX-12-25")]
		[DataRow("XXXX-12-XX")]
		
		[DataRow("1XXX-XX")]
		[DataRow("1XXX-12")]
		[DataRow("1XXX-1X")]

		[DataRow("")]

		////////////////////////////////////////////////////////////////////////////////////////////
		// ranges
		[DataRow("1985-04-12/..")]
		[DataRow("1985-04/..")]
		[DataRow("1985/..")]

		[DataRow("../1985-04-12")]
		[DataRow("../1985-04")]
		[DataRow("../1985")]

		[DataRow("1985-04-12/")]
		[DataRow("1985-04/")]
		[DataRow("1985/")]

		[DataRow("/1985-04-12")]
		[DataRow("/1985-04")]
		[DataRow("/1985")]

		// sets
		[DataRow("[1667,1668,1670..1672]")]
		[DataRow("[1760-12..]")]
		[DataRow("[1760-01,1760-02,1760-12..]")]
		[DataRow("[1667,1760-12]")]
		[DataRow("[..1984]")]

		[DataRow("{1667,1668,1670..1672}")]
		[DataRow("{1960,1961-12}")]
		[DataRow("{..1984}")]

		// intervals
		[DataRow("2004-06-~01/2004-06-~20")]
		[DataRow("2004-06-XX/2004-07-03")]

		// some additional tests
		[DataRow("1964/2008")]
		[DataRow("2004-06/2006-08")]
		[DataRow("2004-02-01/2005-02-08")]
		[DataRow("2004-02-01/2005-02")]
		[DataRow("2004-02-01/2005")]
		[DataRow("2005/2006-02")]
		// Level One Interval
		[DataRow("1984~/2004-06")]
		[DataRow("1984/2004-06~")]
		[DataRow("1984~/2004~")]
		[DataRow("1984?/2004%")]
		[DataRow("1984-06?/2004-08?")]
		[DataRow("1984-06-02?/2004-08-08~")]

		[DataRow("[1667,1668]")]
		[DataRow("[..1760-12-03]")]
		[DataRow("[1760-12..]")]
		[DataRow("[1760-01,1760-02]")]
		[DataRow("[1667]")]

		// Unspecified
		[DataRow("199X")]
		[DataRow("19XX")]
		[DataRow("1999-XX")]
		[DataRow("1999-01-XX")]
		[DataRow("1999-XX-XX")]

		// Partial Xnspecified
		[DataRow("156X-12-25")]
		[DataRow("15XX-12-25")]
		[DataRow("15XX-12-XX")]
		[DataRow("1560-XX-25")]

		[DataRow("")]

		[DataTestMethod]
		public void VariedOtherDateFormats(string edtfString)
		{
			var result = ExtendedDateTimeFormatParser.Parse(edtfString);
			var sResult = result.ToString();
			sResult.ShouldBe(edtfString);
		}
	}
}