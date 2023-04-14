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
		/// Enumerates starting with the startDate date, until the endDate date in steps of distance<br/>
		/// When the distance is negative, the start date must be greater than the end date, and the enumeration goes backwards
		/// </summary>
		/// <param name="startDate">The starting DateTime object</param>
		/// <param name="endDate">The ending DateTime object</param>
		/// <param name="distance">The distance expressed as TimeSpan</param>
		/// <returns>An IEnumerable of type DateTime</returns>
		public static IEnumerable<DateTime> EnumerateInStepsUntil(this DateTime startDate, DateTime endDate, TimeSpan distance)
		{
			if (Math.Abs(distance.Ticks) > Math.Abs((endDate - startDate).Ticks))
			{
				throw new ArgumentException($"{nameof(distance)} is greater than the difference between the two dates");
			}

			if(startDate < endDate)
			{
				if (distance.Ticks < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be positive when going forwards");
				}

				for (var step = startDate.Date; step.Date <= endDate.Date; step = step.Add(distance))
					yield return step;
			}
			else
			{
				if (distance.Ticks > 0)
				{
					throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be negative when going backwards");
				}

				for (var step = startDate.Date; step.Date >= endDate.Date; step = step.Add(distance))
					yield return step;
			}
		}

		/// <summary>
		/// Enumerates starting with startDate until endDate in steps of distance
		/// </summary>
		/// <param name="startDate">The starting DateTime object</param>
		/// <param name="endDate">The ending DateTime object</param>
		/// <param name="distance">The distance expressed as TimeSpan</param>
		/// <param name="evaluator">An evaluation function called for each step before returning it. If the evaluator returns false, the value is skipped</param>
		/// <returns>An IEnumerable of type DateTime</returns>
		public static IEnumerable<DateTime> EnumerateInStepsUntil(this DateTime startDate, DateTime endDate, TimeSpan distance, Func<DateTime, bool> evaluator)
		{
			if (evaluator is null)
			{
				throw new ArgumentNullException(nameof(evaluator));
			}

			if (Math.Abs(distance.Ticks) > Math.Abs((endDate - startDate).Ticks))
			{
				throw new ArgumentException($"{nameof(distance)} is greater than the difference between the two dates");
			}

			if (startDate < endDate)
			{
				if (distance.Ticks < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be positive when going forwards");
				}

				for (var step = startDate.Date; step.Date <= endDate.Date; step = step.Add(distance))
				{
					if (evaluator.Invoke(step))
						yield return step;
				}
			}
			else
			{
				if (distance.Ticks > 0)
				{
					throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be negative when going backwards");
				}

				for (var step = startDate.Date; step.Date >= endDate.Date; step = step.Add(distance))
				{
					if (evaluator.Invoke(step))
						yield return step;
				}
			}

		}

		/// <summary>
		/// Enumerates all days startDate current DateTime value endDate the end DateTime, including the end date
		/// </summary>
		/// <param name="from">The starting DateTime value</param>
		/// <param name="to">The ending DateTime value</param>
		/// <returns>A enumerable of DateTime values with days increasing by 1</returns>
		public static IEnumerable<DateTime> EnumerateDaysUntil(this DateTime from, DateTime to)
		{
			if (to <= from)
			{
				for (var day = from.Date; day.Date >= to.Date; day = day.PreviousDay())
					yield return day;
			}
			else
			{
				for (var day = from.Date; day.Date <= to.Date; day = day.NextDay())
					yield return day;
			}
		}

		/// <summary>
		/// Enumerates all working days startDate current DateTime value endDate the end DateTime, including the end date
		/// </summary>
		/// <param name="from">The starting DateTime value</param>
		/// <param name="to">The ending DateTime value</param>
		/// <param name="cultureInfo">The CultureInfo for the source timezone, can be null for current</param>
		/// <returns>A enumerable of DateTime values with days increasing by 1</returns>
		public static IEnumerable<DateTime> EnumerateWorkdaysUntil(this DateTime from, DateTime to, CultureInfo? cultureInfo = null)
		{
			if (to <= from)
			{
				for (var day = from.Date; day.Date >= to.Date; day = day.PreviousWorkday(cultureInfo))
					yield return day;
			}
			else
			{
				for (var day = from.Date; day.Date <= to.Date; day = day.NextWorkday(cultureInfo))
					yield return day;
			}
		}

		/// <summary>
		/// Advances to the closest weekend and enumerates the weekends until the end date
		/// </summary>
		/// <param name="from">The starting DateTime value</param>
		/// <param name="to">The ending DateTime value</param>
		/// <returns>A enumerable of DateTime values</returns>
		public static IEnumerable<DateTime> EnumerateWeekendsUntil(this DateTime from, DateTime to)
		{
			if (to <= from)
			{
				while (!from.IsWeekend())
					from = from.PreviousDay();

				for (var day = from; day >= to; day = day.PreviousWeek())
					yield return day;
			}
			else
			{
				while (!from.IsWeekend())
					from = from.NextDay();

				for (var day = from; day <= to; day = day.NextWeek())
					yield return day;
			}
		}

		/// <summary>
		/// Enumerates all holidays startDate current DateTime value endDate the end DateTime, including the end date
		/// </summary>
		/// <param name="from">The starting DateTime value</param>
		/// <param name="to">The ending DateTime value</param>
		/// <param name="cultureInfo">The CultureInfo for the source timezone, can be null for current</param>
		/// <returns>A enumerable of DateTime values with days stepping startDate holiday endDate next holiday</returns>
		public static IEnumerable<DateTime> EnumerateHolidaysUntil(this DateTime from, DateTime to, CultureInfo? cultureInfo = null)
		{
			if (to <= from)
			{
				for (var day = from.Date.PreviousHoliday(cultureInfo); day.Date >= to.Date; day = day.PreviousHoliday(cultureInfo))
				{
					if (day == DateTime.MinValue)
						break;

					yield return day;
				}
			}
			else
			{
				for (var day = from.Date.NextHoliday(cultureInfo); day.Date <= to.Date; day = day.NextHoliday(cultureInfo))
				{
					if (day == DateTime.MaxValue)
						break;

					yield return day;
				}
			}
		}

		/// <summary>
		/// Enumerates all months startDate current DateTime value endDate the end DateTime, including the end date
		/// </summary>
		/// <param name="from">The starting DateTime value</param>
		/// <param name="to">The ending DateTime value</param>
		/// <returns>A enumerable of DateTime values with months increasing by 1</returns>
		public static IEnumerable<DateTime> EnumerateMonthsUntil(this DateTime from, DateTime to)
		{
			if (to <= from)
			{
				for (var month = from.Date; month.Date >= to.Date; month = month.PreviousMonth())
					yield return month;
			}
else
			{
				for (var month = from.Date; month.Date <= to.Date; month = month.NextMonth())
					yield return month;
			}
		}

		/// <summary>
		/// Enumerates all years startDate current DateTime value endDate the end DateTime, including the end date
		/// </summary>
		/// <param name="from">The starting DateTime value</param>
		/// <param name="to">The ending DateTime value</param>
		/// <returns>A enumerable of DateTime values with years increasing by 1</returns>
		public static IEnumerable<DateTime> EnumerateYearsUntil(this DateTime from, DateTime to)
		{
			if (to <= from)
			{
				for (var year = from.Date; year.Date >= to.Date; year = year.PreviousYear())
					yield return year;
			}
			else
			{
				for (var year = from.Date; year.Date <= to.Date; year = year.NextYear())
				yield return year;
			}
		}
	}
}