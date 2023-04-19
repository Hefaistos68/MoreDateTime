using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MoreDateTime.Extensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateOnlyExtensions
	{
		/// <summary>
		/// Checks if the given value is between the given startDate and endDate values, excluding the start and end value
		/// </summary>
		/// <param name="me">The DateOnly to compare</param>
		/// <param name="startDate">The startDate date</param>
		/// <param name="endDate">The endDate date</param>
		/// <returns>True if the value is greater or equal startDate and less than or equal endDate</returns>
		public static bool IsBetween(this DateOnly me, DateOnly startDate, DateOnly endDate)
		{
			if (endDate < startDate)
			{
				throw new ArgumentException("End must be greater than startDate");
			}

			return (me > startDate) && (me < endDate);
		}

		/// <summary>
		/// Checks if the given value is within the given startDate and endDate values, including startDate and endDate
		/// </summary>
		/// <param name="me">The DateTime to compare</param>
		/// <param name="startDate">The startDate date</param>
		/// <param name="endDate">The endDate date</param>
		/// <returns>True if the value is greater or equal startDate and less than or equal endDate</returns>
		public static bool IsWithin(this DateOnly me, DateTime startDate, DateTime endDate)
		{
			if (endDate < startDate)
			{
				throw new ArgumentException("End must be greater than startDate");
			}

			return (me >= startDate.ToDateOnly()) && (me <= endDate.ToDateOnly());
		}

		/// <summary>
		/// Checks if the given value is within the given startDate and endDate values, including startDate and endDate
		/// </summary>
		/// <param name="me">The DateTime to compare</param>
		/// <param name="startDate">The startDate date</param>
		/// <param name="endDate">The endDate date</param>
		/// <returns>True if the value is greater or equal startDate and less than or equal endDate</returns>
		public static bool IsWithin(this DateOnly me, DateOnly startDate, DateOnly endDate)
		{
			if (endDate < startDate)
			{
				throw new ArgumentException("End must be greater than startDate");
			}

			return (me >= startDate) && (me <= endDate);
		}

		/// <summary>
		/// Checks if the given value is between the given start and end values, including start date and end date
		/// </summary>
		/// <param name="me">The DateTime to compare</param>
		/// <param name="range">The date range to check</param>
		/// <returns>True if the value is greater or equal range.Start and less than or equal range.End</returns>
		public static bool IsWithin(this DateOnly me, DateOnlyRange range)
		{
			if (!range.IsOrdered())
			{
				throw new ArgumentException("End must be greater than Start");
			}

			return range.Contains(me);
		}

		/// <summary>
		/// Checks if the given date falls on a Saturday or Sunday
		/// </summary>
		/// <param name="me">The DateOnly object to check</param>
		/// <returns>True if the given date is Saturday or Sunday</returns>
		public static bool IsWeekend(this DateOnly me)
		{
			return (me.DayOfWeek == DayOfWeek.Saturday) || (me.DayOfWeek == DayOfWeek.Sunday);
		}

		/// <summary>
		/// Checks if the given date falls on a Saturday or Sunday
		/// </summary>
		/// <param name="me">The DateOnly object to check</param>
		/// <param name="cultureInfo">The CultureInfo for which the holidays will be considered. If null, the current culture is used.</param>
		/// <returns>True if the given date is Saturday or Sunday</returns>
		public static bool IsPublicHoliday(this DateOnly me, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return DateTimeExtensions.CurrentHolidayProvider.IsPublicHoliday(me, cultureInfo);
		}
	}
}
