using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateTimeExtensions
	{
		/// <summary>
		/// Compares two dates for equality down to the given precision
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <param name="truncateTo">The precision to truncate to</param>
		/// <param name="cultureInfo">The CulturInfo to use for calendar calculation, can be null for current culture</param>
		/// <returns>A bool.</returns>
		public static bool IsEqual(this DateTime dt, DateTime other, DateTruncate truncateTo, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return truncateTo switch
			{
				DateTruncate.Year => dt.Year == other.Year,
				DateTruncate.Month => dt.Year == other.Year && dt.Month == other.Month,
				DateTruncate.Week => cultureInfo.Calendar.GetWeekOfYear(dt, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek) == cultureInfo.Calendar.GetWeekOfYear(other, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek),
				DateTruncate.Day => dt.Year == other.Year && dt.Month == other.Month && dt.Day == other.Day,
				DateTruncate.Hour => dt.Year == other.Year && dt.Month == other.Month && dt.Day == other.Day && dt.Hour == other.Hour,
				DateTruncate.Minute => dt.Year == other.Year && dt.Month == other.Month && dt.Day == other.Day && dt.Hour == other.Hour && dt.Minute == other.Minute,
				DateTruncate.Second => dt.Year == other.Year && dt.Month == other.Month && dt.Day == other.Day && dt.Hour == other.Hour && dt.Minute == other.Minute && dt.Second == other.Second,
				_ => false,
			};
		}

		/// <summary>
		/// Compares two dates for equality down to the year (hours, minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToDay(this DateTime dt, DateTime other)
		{
			return dt.IsEqual(other, DateTruncate.Day);
		}

		/// <summary>
		/// Compares two dates for equality down to the hour (minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToHour(this DateTime dt, DateTime other)
		{
			return dt.IsEqual(other, DateTruncate.Hour);
		}

		/// <summary>
		/// Compares two dates for equality down to the minute (seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToMinute(this DateTime dt, DateTime other)
		{
			return dt.IsEqual(other, DateTruncate.Minute);
		}

		/// <summary>
		/// Compares two dates for equality down to the year (days, hours, minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToMonth(this DateTime dt, DateTime other)
		{
			return dt.IsEqual(other, DateTruncate.Month);
		}

		/// <summary>
		/// Compares two dates for equality down to the second (milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToSecond(this DateTime dt, DateTime other)
		{
			return dt.IsEqual(other, DateTruncate.Second);
		}

		/// <summary>
		/// Compares two dates for equality down to the week (hours, minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <param name="cultureInfo">The CulturInfo to use for week calculation, can be null for current culture</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToWeek(this DateTime dt, DateTime other, CultureInfo? cultureInfo = null)
		{
			return dt.IsEqual(other, DateTruncate.Week, cultureInfo);
		}

		/// <summary>
		/// Compares two dates for equality down to the year (months, days, hours, minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are equal down to the year</returns>
		public static bool IsEqualDownToYear(this DateTime dt, DateTime other)
		{
			return dt.IsEqual(other, DateTruncate.Year);
		}
	}
}