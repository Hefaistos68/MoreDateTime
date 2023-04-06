using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MoreDateTime.Extensions;
using static MoreDateTime.Extensions.DateTimeExtensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateOnlyExtensions
	{
		/// <summary>
		/// Compares two dates for equality down to the given precision
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <param name="truncateTo">The precision to truncate to</param>
		/// <returns>A bool.</returns>
		public static bool IsEqual(this DateOnly dt, DateOnly other, DateTruncate truncateTo, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return truncateTo switch
			{
				DateTruncate.Year => dt.Year == other.Year,
				DateTruncate.Month => dt.Year == other.Year && dt.Month == other.Month,
				DateTruncate.Week => cultureInfo.Calendar.GetWeekOfYear(dt.ToDateTime(), cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek) == cultureInfo.Calendar.GetWeekOfYear(other.ToDateTime(), cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek),
				DateTruncate.Day => dt.Year == other.Year && dt.Month == other.Month && dt.Day == other.Day,
				_ => false,
			};
		}

		/// <summary>
		/// Compares two dates for equality down to the year (hours, minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToDay(this DateOnly dt, DateOnly other)
		{
			return dt.IsEqual(other, DateTruncate.Day);
		}

		/// <summary>
		/// Compares two dates for equality down to the year (days, hours, minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToMonth(this DateOnly dt, DateOnly other)
		{
			return dt.IsEqual(other, DateTruncate.Month);
		}

		/// <summary>
		/// Compares two dates for equality down to the week (hours, minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <param name="cultureInfo">The CulturInfo to use for week calculation, can be null for current culture</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToWeek(this DateOnly dt, DateOnly other, CultureInfo? cultureInfo = null)
		{
			return dt.IsEqual(other, DateTruncate.Week, cultureInfo);
		}

		/// <summary>
		/// Compares two dates for equality down to the year (months, days, hours, minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are equal down to the year</returns>
		public static bool IsEqualDownToYear(this DateOnly dt, DateOnly other)
		{
			return dt.IsEqual(other, DateTruncate.Year);
		}
	}
}
