using System.Globalization;

using MoreDateTime.Extensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateOnlyExtensions
	{
		/// <summary>
		/// Gets the DateOnly value of the next day
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly NextDay(this DateOnly dt)
		{
			return dt.AddDays(1);
		}

		/// <summary>
		/// Gets the DateOnly value of the next holiday<br/>
		/// Holidays are calculated through the IHolidayProvider interface in DateTimeExtensions.
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly NextHoliday(this DateOnly dt, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			DateOnly nextWorkingDay = dt;

			// Loop until we find a weekday that is not a holiday or weekend
			nextWorkingDay = nextWorkingDay.NextDay();

			// Note: lets assume that after 2050 this library will no longer be in use
			//       and that no dates beyond 2050 will be calculated using holidays
			while (!DateTimeExtensions.CurrentHolidayProvider.IsPublicHoliday(nextWorkingDay, cultureInfo))
			{
				if (nextWorkingDay.Year >= 2050)
				{
					return DateOnly.MaxValue;
				}

				nextWorkingDay = nextWorkingDay.NextDay();
			}

			return nextWorkingDay;
		}

		/// <summary>
		/// Gets the DateOnly value of the same day in the next week
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly NextWeek(this DateOnly dt)
		{
			return dt.AddDays(DaysInWeek);
		}

		/// <summary>
		/// Gets the DateOnly value of the next weekend following this date. If the given date is already a weekend, the next weekend is returned.
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly NextWeekend(this DateOnly dt)
		{
			// if its already a weekend, skip it
			while (dt.IsWeekend())
			{
				dt = dt.NextDay();
			}

			while (!dt.IsWeekend())
			{
				dt = dt.NextDay();
			}

			return dt;
		}

		/// <summary>
		/// Gets the DateOnly value of the same day in the next week
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <param name="cultureInfo">The CulturInfo to use for calendar calculation, can be null for current culture</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly NextWorkday(this DateOnly dt, CultureInfo? cultureInfo = null)
		{
			if (cultureInfo is null)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}

			DateOnly nextWorkingDay = dt;

			// Loop until we find a weekday that is not a holiday or weekend
			nextWorkingDay = nextWorkingDay.NextDay();

			// Loop until we find a weekday that is not a holiday
			while (DateTimeExtensions.CurrentHolidayProvider.IsPublicHoliday(nextWorkingDay, cultureInfo) || nextWorkingDay.IsWeekend())
			{
				nextWorkingDay = nextWorkingDay.NextDay();
			}

			return nextWorkingDay;
		}

		/// <summary>
		/// Gets the DateOnly value of the next month, first day
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly NextMonth(this DateOnly dt)
		{
			return dt.AddMonths(1);
		}

		/// <summary>
		/// Gets the DateOnly value of the next year, first month, first day
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly NextYear(this DateOnly dt)
		{
			return dt.AddYears(1);
		}

		/// <summary>
		/// Gets the DateOnly value of the day before
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly PreviousDay(this DateOnly dt)
		{
			return dt.AddDays(-1);
		}

		/// <summary>
		/// Gets the DateOnly value of the previous working day
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <param name="cultureInfo">The CulturInfo to use for calendar calculation, can be null for current culture</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly PreviousWorkday(this DateOnly dt, CultureInfo? cultureInfo = null)
		{
			if (cultureInfo is null)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}

			DateTime nextWorkingDay = dt.ToDateTime();

			nextWorkingDay = nextWorkingDay.PreviousDay();

			// Loop until we find a weekday that is not a holiday
			while (DateTimeExtensions.CurrentHolidayProvider.IsPublicHoliday(nextWorkingDay, cultureInfo) || nextWorkingDay.IsWeekend())
			{
				nextWorkingDay = nextWorkingDay.PreviousDay();
			}

			return nextWorkingDay.ToDateOnly();
		}

		/// <summary>
		/// Gets the DateOnly value of the month before
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object whose value is the month before the given, the day and time is not changed</returns>
		public static DateOnly PreviousMonth(this DateOnly dt)
		{
			return dt.AddMonths(-1);
		}

		/// <summary>
		/// Gets the DateOnly value of the week before
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object whose value is the week before the given, the weekday and time is not changed</returns>
		public static DateOnly PreviousWeek(this DateOnly dt)
		{
			return dt.AddDays(-DaysInWeek);
		}

		/// <summary>
		/// Gets the DateOnly value of the weekend before this date. If the given date is already a weekend, the previous weekend is returned.
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object whose value is the week before the given, the weekday and time is not changed</returns>
		public static DateOnly PreviousWeekend(this DateOnly dt)
		{
			while (dt.IsWeekend())
			{
				dt = dt.PreviousDay();
			}

			while (!dt.IsWeekend())
			{
				dt = dt.PreviousDay();
			}

			return dt;
		}

		/// <summary>
		/// Gets the DateOnly value of the next year, first month, first day, time members are all zeroed (00:00:00)
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly PreviousYear(this DateOnly dt)
		{
			return dt.AddYears(-1);
		}

		/// <summary>
		/// Gets the DateOnly value of the previous holiday<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the holidays based on the calendar in cultureInfo,
		/// otherwise it will simply skip weekends.
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>A DateOnly object</returns>
		public static DateOnly PreviousHoliday(this DateOnly dt, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			DateOnly nextWorkingDay = dt;

			// Loop until we find a weekday that is not a holiday or weekend
			nextWorkingDay = nextWorkingDay.PreviousDay();

			// Note: lets assume that after 2050 this library will no longer be in use
			//       and that no dates beyond 2050 will be calculated using holidays
			while (!DateTimeExtensions.CurrentHolidayProvider.IsPublicHoliday(nextWorkingDay, cultureInfo))
			{
				if (nextWorkingDay.Year <= DateOnly.MinValue.Year)
				{
					return DateOnly.MaxValue;
				}

				nextWorkingDay = nextWorkingDay.PreviousDay();
			}

			return nextWorkingDay;
		}
	}
}