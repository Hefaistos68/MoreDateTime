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
		/// Count the number of Days between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdays and holidays, can be null to use current</param>
		/// <returns>The number of Days between startDate and endDate as a double with fractions</returns>
		public static double NumberOfDaysUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Days, cultureInfo);
		}

		/// <summary>
		/// Count the number of Decades between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdays and holidays, can be null to use current</param>
		/// <returns>The number of Decades between startDate and endDate as a double with fractions</returns>
		public static double NumberOfDecadesUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Decades, cultureInfo);
		}

		/// <summary>
		/// Count the number of Holidays between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdayss and holidays, can be null to use current</param>
		/// <returns>The number of Holidays between startDate and endDate as a double with fractions</returns>
		public static double NumberOfHolidaysUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Holidays, cultureInfo);
		}

		/// <summary>
		/// Count the number of Hours between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdayss and holidays, can be null to use current</param>
		/// <returns>The number of Hours between startDate and endDate as a double with fractions</returns>
		public static double NumberOfHoursUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Hours, cultureInfo);
		}

		/// <summary>
		/// Count the number of Milliseconds between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdayss and holidays, can be null to use current</param>
		/// <returns>The number of Milliseconds between startDate and endDate as a double with fractions</returns>
		public static double NumberOfMillisecondsUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Milliseconds, cultureInfo);
		}

		/// <summary>
		/// Count the number of Minutes between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdayss and holidays, can be null to use current</param>
		/// <returns>The number of Minutes between startDate and endDate as a double with fractions</returns>
		public static double NumberOfMinutesUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Minutes, cultureInfo);
		}

		/// <summary>
		/// Count the number of Months between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdays and holidays, can be null to use current</param>
		/// <returns>The number of Months between startDate and endDate as a double with fractions</returns>
		public static double NumberOfMonthsUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Months, cultureInfo);
		}

		/// <summary>
		/// Count the number of Seconds between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdayss and holidays, can be null to use current</param>
		/// <returns>The number of Seconds between startDate and endDate as a double with fractions</returns>
		public static double NumberOfSecondsUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Seconds, cultureInfo);
		}

		/// <summary>
		/// Count the number of Semesters between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdays and holidays, can be null to use current</param>
		/// <returns>The number of Semesters between startDate and endDate as a double with fractions</returns>
		public static double NumberOfSemestersUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Semesters, cultureInfo);
		}

		/// <summary>
		/// Count the number of Weekends between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdays and holidays, can be null to use current</param>
		/// <returns>The number of Weekends between startDate and endDate as a double without fractions</returns>
		public static double NumberOfWeekendsUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Weekends, cultureInfo);
		}

		/// <summary>
		/// Count the number of Weeks between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdays and holidays, can be null to use current</param>
		/// <returns>The number of Weeks between startDate and endDate as a double with fractions</returns>
		public static double NumberOfWeeksUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Weeks, cultureInfo);
		}

		/// <summary>
		/// Count the number of Workdays between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workWorkdays and holiWorkdays, can be null to use current</param>
		/// <returns>The number of Workdays between startDate and endDate as a double with fractions</returns>
		public static double NumberOfWorkdaysUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Workdays, cultureInfo);
		}

		/// <summary>
		/// Count the number of years between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="cultureInfo">The culture info for workdays and holidays, can be null to use current</param>
		/// <returns>The number of years between startDate and endDate as a double with fractions</returns>
		public static double NumberOfYearsUntil(this DateOnly startDate, DateOnly endDate, CultureInfo? cultureInfo = null)
		{
			return NumberOf_Until(startDate, endDate, TimeUnit.Years, cultureInfo);
		}

		/// <summary>
		/// Count the number of given time units between startDate and endDate
		/// </summary>
		/// <param name="startDate">The start date.</param>
		/// <param name="endDate">The end date.</param>
		/// <param name="u">The TimeUnit to count</param>
		/// <param name="cultureInfo">The culture info for workdays and holidays, can be null to use current</param>
		/// <returns>An int.</returns>
		private static double NumberOf_Until(this DateOnly startDate, DateOnly endDate, TimeUnit u, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			TimeSpan diff = endDate.ToDateTime() - startDate.ToDateTime();

			long dif = diff.Ticks;
			long nom = NumberOfMonths(startDate, endDate);

			switch (u)
			{
				case TimeUnit.Decades:
					return ((double)(endDate.Year - startDate.Year)) / 10.0d;

				case TimeUnit.Years:
					double fractionOfYear = nom / 12.0;

					return fractionOfYear;

				case TimeUnit.Semesters:
					double fractionOfSemester = nom / 6.0;

					return fractionOfSemester;

				case TimeUnit.Trimesters:
					double fractionOfTrimester = nom / 4.0;

					return fractionOfTrimester;

				case TimeUnit.Months:
					return nom;

				case TimeUnit.Weeks:
					double fractionOfWeeks = ((double)TimeSpan.FromTicks(dif).TotalDays) / (double)DateTimeExtensions.DaysInWeek;

					return Math.Round(fractionOfWeeks, 2);

				case TimeUnit.Weekends:
					return CalculateWeekends();

				case TimeUnit.Workdays:
					return (double)startDate.EnumerateWorkdaysUntil(endDate).Count();

				case TimeUnit.Days:
					return diff.TotalDays;

				case TimeUnit.Hours:
					return diff.TotalHours;

				case TimeUnit.Minutes:
					return diff.TotalMinutes;

				case TimeUnit.Seconds:
					return diff.TotalSeconds;

				case TimeUnit.Milliseconds:
					return diff.TotalMilliseconds;

				case TimeUnit.Holidays:
					return startDate.EnumerateHolidaysUntil(endDate).Count();
			}

			return 0;

			long NumberOfMonths(DateOnly startDate, DateOnly endDate)
			{
				return (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);
			}

			double CalculateWeekends()
			{
				// 2 steps
				// 1) find first weekend
				// 2) loop until endDate in 7 day steps

				double weekends = 0;
				for (DateOnly date = startDate; date <= endDate; date = date.NextDay())
				{
					if (date.IsWeekend())
					{
						weekends++;
						while (date < endDate)
						{
							date = date.NextWeek(); // jump from saturday/sunday to next
							weekends++;
						}

						// bail out
						return Math.Truncate(weekends);
					}
				}

				return weekends;
			}
		}
	}
}
