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
		/// Tests if the time in the given DateTime object is midnight (0h)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <returns>True if time is 0h ignoring minures and seconds</returns>
		public static bool IsMidnight(this DateTime dt)
		{
			return (dt.Hour == 0);
		}

		/// <summary>
		/// Tests if the time in the given DateTime object is midday (12h)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>True if time is 12h ignoring minutes and seconds</returns>
		public static bool IsMidday(this DateTime dt)
		{
			return (dt.Hour == 12);
		}

		/// <summary>
		/// Tests if the time in the given DateTime object is considered afternoon (12:00:00.000 - 18:00:00.000)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <returns>True if time is between 12:00:00.000 and 18:00:00.000</returns>
		public static bool IsAfternoon(this DateTime dt)
		{
			return dt.Hour.IsBetween(12, 18);
		}

		/// <summary>
		/// Tests if the time in the given DateTime object is between 18:00:00 and 06:00:00
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>True if time is between 18:00:00.000 and 06:00:00.000</returns>
		public static bool IsNight(this DateTime dt)
		{
			return dt.Hour.IsBetween(18, 23) || dt.Hour.IsBetween(0, 6);
		}

		/// <summary>
		/// Tests if the time in the given DateTime object is midday (12:00:00.000)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <returns>True if time is 00:00:00.000</returns>
		public static bool IsMorning(this DateTime dt)
		{
			return dt.Hour.IsBetween(6, 12);
		}

		/// <summary>
		/// Checks if the given value is between the given start and end values
		/// </summary>
		/// <param name="me">The integer to compare</param>
		/// <param name="start">The start</param>
		/// <param name="end">The end</param>
		/// <returns>True if the value is greater or equal start and less than or equal end</returns>
		public static bool IsBetween(this int me, int start, int end)
		{
			if (end < start)
			{
				throw new ArgumentException("End must be greater than startDate");
			}

			return (me >= start) && (me <= end);
		}

		/// <summary>
		/// Checks if the given value is between the given startDate and endDate values
		/// </summary>
		/// <param name="me">The DateTime to compare</param>
		/// <param name="startDate">The startDate date</param>
		/// <param name="endDate">The endDate date</param>
		/// <returns>True if the value is greater or equal startDate and less than or equal endDate</returns>
		public static bool IsBetween(this DateTime me, DateTime startDate, DateTime endDate)
		{
			if (endDate < startDate)
			{
				throw new ArgumentException("End must be greater than startDate");
			}

			return (me >= startDate) && (me <= endDate);
		}

		/// <summary>
		/// Checks if the given date falls on a Saturday or Sunday
		/// </summary>
		/// <param name="me">The DateTime object to check</param>
		/// <returns>True if the given date is Saturday or Sunday</returns>
		public static bool IsWeekend(this DateTime me)
		{
			return (me.DayOfWeek == DayOfWeek.Saturday) || (me.DayOfWeek == DayOfWeek.Sunday);
		}

		/// <summary>
		/// Checks if the given date falls on a Saturday or Sunday
		/// </summary>
		/// <param name="me">The DateTime object to check</param>
		/// <returns>True if the given date is Saturday or Sunday</returns>
		public static bool IsPublicHoliday(this DateTime me, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return CurrentHolidayProvider.IsPublicHoliday(me, cultureInfo);
		}

	}
}
