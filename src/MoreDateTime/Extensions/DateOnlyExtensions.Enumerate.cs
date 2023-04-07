using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>	
	public static partial class DateOnlyExtensions
	{
		private static readonly TimeSpan _enumerationGap = new TimeSpan(0, 23, 59, 59, 999);

		/// <summary>
		/// Enumerates starting with the startDate date, until the endDate date in steps of distance<br/>
		/// When the distance is negative, the start date must be greater than the end date, and the enumeration goes backwards
		/// </summary>
		/// <param name="startDate">The starting DateOnly object</param>
		/// <param name="endDate">The ending DateOnly object</param>
		/// <param name="distance">The distance expressed as TimeSpan</param>
		/// <returns>An IEnumerable of type DateOnly</returns>
		public static IEnumerable<DateOnly> EnumerateInStepsUntil(this DateOnly startDate, DateOnly endDate, TimeSpan distance)
		{
			DateTime startDate2 = startDate.ToDateTime();
			DateTime endDate2 = endDate.ToDateTime();

			if (Math.Abs(distance.Ticks) > Math.Abs((endDate2 - startDate2).Ticks))
			{
				throw new ArgumentException($"{nameof(distance)} is greater than the difference between the two dates");
			}

			// make sure we hit the same day again if distance is less than a day

			if (startDate < endDate)
			{
				endDate2 = endDate2.Add(_enumerationGap);
				for (var step = startDate2.Date; step < endDate2; step = step.Add(distance))
					yield return step.ToDateOnly();
			}
			else
			{
				endDate2 = endDate2.Sub(_enumerationGap);
				for (var step = startDate2.Date; step >= endDate2; step = step.Add(distance))
					yield return step.ToDateOnly();
			}
		}

		/// <summary>
		/// Enumerates starting with startDate until endDate in steps of distance
		/// </summary>
		/// <param name="startDate">The starting DateOnly object</param>
		/// <param name="endDate">The ending DateOnly object</param>
		/// <param name="distance">The distance expressed as TimeSpan</param>
		/// <param name="evaluator">An evaluation function called for each step before returning it. If the evaluator returns false, the value is skipped</param>
		/// <returns>An IEnumerable of type DateOnly</returns>
		public static IEnumerable<DateOnly> EnumerateInStepsUntil(this DateOnly startDate, DateOnly endDate, TimeSpan distance, Func<DateOnly, bool> evaluator)
		{
			if (evaluator is null)
			{
				throw new ArgumentNullException(nameof(evaluator));
			}

			DateTime startDate2 = startDate.ToDateTime();
			DateTime endDate2 = endDate.ToDateTime();

			if (Math.Abs(distance.Ticks) > Math.Abs((endDate2 - startDate2).Ticks))
			{
				throw new ArgumentException($"{nameof(distance)} is greater than the difference between the two dates");
			}

			if (startDate < endDate)
			{
				for (var step = startDate2.Date; step.Date <= endDate2.Date; step = step.Add(distance))
				{
					var dateOnly = step.ToDateOnly();
					if (evaluator.Invoke(dateOnly))
						yield return dateOnly;
				}
			}
			else
			{
				for (var step = startDate2.Date; step.Date >= endDate2.Date; step = step.Add(distance))
				{
					var dateOnly = step.ToDateOnly();
					if (evaluator.Invoke(dateOnly))
						yield return dateOnly;
				}
			}

		}

		/// <summary>
		/// Enumerates all days startDate current DateOnly value endDate the end DateOnly, including the end date
		/// </summary>
		/// <param name="from">The starting DateOnly value</param>
		/// <param name="to">The ending DateOnly value</param>
		/// <returns>A enumerable of DateOnly values with days increasing by 1</returns>
		public static IEnumerable<DateOnly> EnumerateDaysUntil(this DateOnly from, DateOnly to)
		{
			if (to <= from)
			{
				for (var day = from; day >= to; day = day.NextDay())
					yield return day;
			}
			else
			{
				for (var day = from; day <= to; day = day.NextDay())
					yield return day;
			}
		}

		/// <summary>
		/// Enumerates all working days startDate current DateOnly value endDate the end DateOnly, including the end date
		/// </summary>
		/// <param name="from">The starting DateOnly value</param>
		/// <param name="to">The ending DateOnly value</param>
		/// <param name="cultureInfo">The CultureInfo for the source timezone, can be null for current</param>
		/// <returns>A enumerable of DateOnly values with days increasing by 1</returns>
		public static IEnumerable<DateOnly> EnumerateWorkdaysUntil(this DateOnly from, DateOnly to, CultureInfo? cultureInfo = null)
		{
			if (to <= from)
			{
				for (var day = from; day >= to; day = day.PreviousWorkday(cultureInfo))
					yield return day;
			}
			else
			{
				for (var day = from; day <= to; day = day.NextWorkday(cultureInfo))
					yield return day;
			}
		}

		/// <summary>
		/// Advances to the closest weekend and enumerates the weekends until the end date
		/// </summary>
		/// <param name="from">The starting DateOnly value</param>
		/// <param name="to">The ending DateOnly value</param>
		/// <returns>A enumerable of DateOnly values</returns>
		public static IEnumerable<DateOnly> EnumerateWeekendsUntil(this DateOnly from, DateOnly to)
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
				while(!from.IsWeekend()) 
					from = from.NextDay();

				for (var day = from; day <= to; day = day.NextWeek())
					yield return day;
			}
		}

		/// <summary>
		/// Enumerates all holidays startDate current DateOnly value endDate the end DateOnly, including the end date
		/// </summary>
		/// <param name="from">The starting DateOnly value</param>
		/// <param name="to">The ending DateOnly value</param>
		/// <param name="cultureInfo">The CultureInfo for the source timezone, can be null for current</param>
		/// <returns>A enumerable of DateOnly values with days stepping startDate holiday endDate next holiday</returns>
		public static IEnumerable<DateOnly> EnumerateHolidaysUntil(this DateOnly from, DateOnly to, CultureInfo? cultureInfo = null)
		{
			if (to <= from)
			{
				for (DateOnly day = from.PreviousHoliday(cultureInfo); day >= to; day = day.PreviousHoliday(cultureInfo))
				{
					if (day == DateOnly.MaxValue)
						break;

					yield return day;
				}
			}
			else
			{
				for (DateOnly day = from.NextHoliday(cultureInfo); day <= to; day = day.NextHoliday(cultureInfo))
				{
					if (day == DateOnly.MaxValue)
						break;

					yield return day;
				}
			}
		}

		/// <summary>
		/// Enumerates all months startDate current DateOnly value endDate the end DateOnly, including the end date
		/// </summary>
		/// <param name="from">The starting DateOnly value</param>
		/// <param name="to">The ending DateOnly value</param>
		/// <returns>A enumerable of DateOnly values with months increasing by 1</returns>
		public static IEnumerable<DateOnly> EnumerateMonthsUntil(this DateOnly from, DateOnly to)
		{
			if (to <= from)
			{
				for (var month = from; month >= to; month = month.PreviousMonth())
					yield return month;
			}
			else
			{
				for (var month = from; month <= to; month = month.NextMonth())
					yield return month;
			}
		}

		/// <summary>
		/// Enumerates all years startDate current DateOnly value endDate the end DateOnly, including the end date
		/// </summary>
		/// <param name="from">The starting DateOnly value</param>
		/// <param name="to">The ending DateOnly value</param>
		/// <returns>A enumerable of DateOnly values with years increasing by 1</returns>
		public static IEnumerable<DateOnly> EnumerateYearsUntil(this DateOnly from, DateOnly to)
		{
			if (to <= from)
			{
				for (var year = from; year >= to; year = year.PreviousYear())
					yield return year;
			}
			else
			{
				for (var year = from; year <= to; year = year.NextYear())
					yield return year;
			}
		}

	}
}
