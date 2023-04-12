using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MoreDateTime.Extensions.DateTimeExtensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateOnlyExtensions
	{
		/// <summary>
		/// Verifies if the two dates are the same day of the month
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same day</returns>
		public static bool IsSameDay(this DateOnly dt, DateOnly other)
		{
			return dt.Day == other.Day;
		}

		/// <summary>
		/// Verifies if the two dates are the same month
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same month</returns>
		public static bool IsSameMonth(this DateOnly dt, DateOnly other)
		{
			return dt.Month == other.Month;
		}

		/// <summary>
		/// Verifies if the two dates are the same week
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same week</returns>
		public static bool IsSameWeek(this DateOnly dt, DateOnly other, CultureInfo? cultureInfo = null)
		{
			return dt.WeekOfYear(cultureInfo) == other.WeekOfYear(cultureInfo);
		}

		/// <summary>
		/// Verifies if the two dates are the same weekday
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same weekday</returns>
		public static bool IsSameWeekday(this DateOnly dt, DateOnly other)
		{
			return dt.DayOfWeek == other.DayOfWeek;
		}

		/// <summary>
		/// Verifies if the two dates are the same year
		/// </summary>
		/// <param name="dt">The first <see cref="DateOnly"/> argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same year</returns>
		public static bool IsSameYear(this DateOnly dt, DateOnly other)
		{
			return dt.Year == other.Year;
		}
		/// <summary>
		/// Verifies if the two dates are the same day of the month
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same day</returns>
		public static bool IsSameDay(this DateOnly dt, DateTime other)
		{
			return dt.Day == other.Day;
		}

		/// <summary>
		/// Verifies if the two dates are the same month
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same month</returns>
		public static bool IsSameMonth(this DateOnly dt, DateTime other)
		{
			return dt.Month == other.Month;
		}

		/// <summary>
		/// Verifies if the two dates are the same week
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same week</returns>
		public static bool IsSameWeek(this DateOnly dt, DateTime other, CultureInfo? cultureInfo = null)
		{
			return dt.WeekOfYear(cultureInfo) == other.WeekOfYear(cultureInfo);
		}

		/// <summary>
		/// Verifies if the two dates are the same weekday
		/// </summary>
		/// <param name="dt">The first DateOnly argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same weekday</returns>
		public static bool IsSameWeekday(this DateOnly dt, DateTime other)
		{
			return dt.DayOfWeek == other.DayOfWeek;
		}

		/// <summary>
		/// Verifies if the two dates are the same year
		/// </summary>
		/// <param name="dt">The first <see cref="DateOnly"/> argument</param>
		/// <param name="other">The DateOnly argument to compare with</param>
		/// <returns>True if the dates are on the same year</returns>
		public static bool IsSameYear(this DateOnly dt, DateTime other)
		{
			return dt.Year == other.Year;
		}
	}
}
