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
		#region Truncation methods
		/// <summary>
		/// Truncates the precision of a DateTime object to the given precision
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <param name="TruncateTo">The precision to truncate to</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null to use current culture</param>
		/// <returns>The Truncated dateTime object</returns>
		public static DateOnly TruncateTo(this DateOnly dt, DateTruncate TruncateTo, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return TruncateTo switch
			{
				DateTruncate.Year => new DateOnly(dt.Year, 1, 1),
				DateTruncate.Month => new DateOnly(dt.Year, dt.Month, 1),
				DateTruncate.Week => CalcDayOfWeek(dt),
				_ => new DateOnly(dt.Year, dt.Month, dt.Day)
			};

			DateOnly CalcDayOfWeek(DateOnly dt)
			{
				while (dt.DayOfWeek != cultureInfo.DateTimeFormat.FirstDayOfWeek)
				{
					dt = dt.AddDays(-1);
				}

				return dt;
			}
		}

		/// <summary>
		/// Truncates the precision of a DateOnly object to the month, day 1
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>The Truncated DateOnly object</returns>
		public static DateOnly TruncateToMonth(this DateOnly dt)
		{
			return dt.TruncateTo(DateTruncate.Month);
		}

		/// <summary>
		/// Truncates the precision of a DateOnly object to the week of the object, day is first day of week
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null to use current culture</param>
		/// <returns>The Truncated DateOnly object</returns>
		public static DateOnly TruncateToWeek(this DateOnly dt, CultureInfo? cultureInfo = null)
		{
			return dt.TruncateTo(DateTruncate.Week, cultureInfo);
		}

		/// <summary>
		/// Truncates the precision of a DateOnly object to the year, day 1, month 1
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>The Truncated DateOnly object</returns>
		public static DateOnly TruncateToYear(this DateOnly dt)
		{
			return dt.TruncateTo(DateTruncate.Year);
		}
		#endregion
	}
}
