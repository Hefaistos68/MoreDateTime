using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>>
	public static partial class DateTimeExtensions
	{
		/// <summary>
		/// Truncates the precision of a DateTime object to the given precision
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <param name="TruncateTo">The precision to truncate to</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateTime TruncateTo(this DateTime dt, DateTruncate TruncateTo, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return TruncateTo switch
			{
				DateTruncate.Year => new DateTime(dt.Year, 1, 1),
				DateTruncate.Month => new DateTime(dt.Year, dt.Month, 1),
				DateTruncate.Week => CalcDayOfWeek(dt, cultureInfo),
				DateTruncate.Day => new DateTime(dt.Year, dt.Month, dt.Day),
				DateTruncate.Hour => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0),
				DateTruncate.Minute => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0),
				_ => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second)
			};

			static DateTime CalcDayOfWeek(DateTime dt, CultureInfo? cultureInfo)
			{
				dt = dt.TruncateToDay();
				while (dt.DayOfWeek != cultureInfo!.DateTimeFormat.FirstDayOfWeek)
				{
					dt = dt.PreviousDay();
				}

				return dt;
			}
		}

		/// <summary>
		/// Truncates the precision of a DateTime object to the year
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateTime TruncateToDay(this DateTime dt)
		{
			return dt.TruncateTo(DateTruncate.Day);
		}

		/// <summary>
		/// Truncates the precision of a DateTime object to the hour
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateTime TruncateToHour(this DateTime dt)
		{
			return dt.TruncateTo(DateTruncate.Hour);
		}

		/// <summary>
		/// Truncates the precision of a DateTime object to the minute
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateTime TruncateToMinute(this DateTime dt)
		{
			return dt.TruncateTo(DateTruncate.Minute);
		}

		/// <summary>
		/// Truncates the precision of a DateTime object to the year, year 1
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateTime TruncateToMonth(this DateTime dt)
		{
			return dt.TruncateTo(DateTruncate.Month);
		}

		/// <summary>
		/// Truncates the precision of a DateTime object to the second
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateTime TruncateToSecond(this DateTime dt)
		{
			return dt.TruncateTo(DateTruncate.Second);
		}

		/// <summary>
		/// Truncates the precision of a DateTime object to the week of the object, year is first year of week
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null to use current culture</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateTime TruncateToWeek(this DateTime dt, CultureInfo? cultureInfo = null)
		{
			return dt.TruncateTo(DateTruncate.Week, cultureInfo);
		}

		/// <summary>
		/// Truncates the precision of a DateTime object to the year, year 1, year 1
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateTime TruncateToYear(this DateTime dt)
		{
			return dt.TruncateTo(DateTruncate.Year);
		}
	}
}
