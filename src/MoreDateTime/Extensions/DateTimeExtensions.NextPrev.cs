using System.Globalization;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateTimeExtensions
	{
		/// <summary>
		/// Returns a DateTime object representing the last day of the current Week
		/// </summary>
		/// <param name="me">The DateTime value of which the first day is requested</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>A DateTime object with the last day of the Week, time members set to 0 (00:00:00)</returns>
		public static DateTime EndOfWeek(this DateTime me, CultureInfo? cultureInfo = null)
		{
			return me.NextWeek().TruncateToWeek(cultureInfo).PreviousDay();
		}

		/// <summary>
		/// Gets the DateTime value of the next day
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextDay(this DateTime dt)
		{
			return dt.AddDays(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next full day (01/01/2010 10:15 to 02/01/2010 00:00, 01/01/2010 10:45 to 02/01/2010 00:00, etc)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextFullDay(this DateTime dt)
		{
			return dt.AddDays(1).TruncateToDay();
		}

		/// <summary>
		/// Gets the DateTime value of the next full hour (10:15 to 11:00, 10:45 to 11:00, etc)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextFullHour(this DateTime dt)
		{
			return dt.AddHours(1).TruncateToHour();
		}

		/// <summary>
		/// Gets the DateTime value of the next full minute (10:15:20 to 10:16:00)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextFullMinute(this DateTime dt)
		{
			return dt.AddMinutes(1).TruncateToMinute();
		}

		/// <summary>
		/// Gets the DateTime value of the next full second (10:15:20.350 to 10:15:21.000)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextFullSecond(this DateTime dt)
		{
			return dt.AddSeconds(1).TruncateToSecond();
		}

		/// <summary>
		/// Gets the DateTime value of the next public holiday according to the given CultureInfo.Calendar<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the working days based on the calendar in cultureInfo,
		///  or if you set a custom holiday provider, it will use that. Otherwise the default implementation will return DateTime.MaxValue
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextHoliday(this DateTime dt, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			DateTime nextWorkingDay = dt;

			// Loop until we find a weekday that is not a holiday or weekend
			nextWorkingDay = nextWorkingDay.NextDay();

			// Note: lets assume that after 2050 this library will no longer be in use
			//       and that no dates beyond 2050 will be calculated using holidays
			while (!CurrentHolidayProvider.IsPublicHoliday(nextWorkingDay, cultureInfo))
			{
				if (nextWorkingDay.Year >= 2050)
				{
					return DateTime.MaxValue;
				}

				nextWorkingDay = nextWorkingDay.NextDay();
			}

			return nextWorkingDay;
		}

		/// <summary>
		/// Gets the DateTime value of the next hour
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextHour(this DateTime dt)
		{
			return dt.AddHours(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next millisecond
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextMillisecond(this DateTime dt)
		{
			return dt.AddMilliseconds(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next minute
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextMinute(this DateTime dt)
		{
			return dt.AddMinutes(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next month
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object whose value is the same day the next month of the given. If the month has less days then the day may change (e.g. 31 Jan to 28 Feb)</returns>
		public static DateTime NextMonth(this DateTime dt)
		{
			return dt.AddMonths(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next second
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextSecond(this DateTime dt)
		{
			return dt.AddSeconds(1);
		}
		/// <summary>
		/// Gets the DateTime value of the same (week)day next week
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextWeek(this DateTime dt)
		{
			return dt.AddDays(DaysInWeek);
		}

		/// <summary>
		/// Gets the DateTime value of the next weekend following this date. If the given date is already a weekend, the next weekend is returned. 
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object representing the next weekend (always Saturday)</returns>
		public static DateTime NextWeekend(this DateTime dt)
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
		/// Gets the DateTime value of the next working day according to the given CultureInfo.Calendar<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the working days based on the calendar in cultureInfo,
		/// otherwise it will simply skip weekends, or if you set a custom holiday provider, it will use that.
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextWorkday(this DateTime dt, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			DateTime nextWorkingDay = dt;

			// Loop until we find a weekday that is not a holiday or weekend
			nextWorkingDay = nextWorkingDay.NextDay();

			while (CurrentHolidayProvider.IsPublicHoliday(nextWorkingDay, cultureInfo) || nextWorkingDay.IsWeekend())
			{
				nextWorkingDay = nextWorkingDay.NextDay();
			}

			return nextWorkingDay;
		}
		/// <summary>
		/// Gets the DateTime value of the next year
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextYear(this DateTime dt)
		{
			return dt.AddYears(1);
		}

		/// <summary>
		/// Gets the DateTime value of the year before
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousDay(this DateTime dt)
		{
			return dt.AddDays(-1);
		}

		/// <summary>
		/// Gets the DateTime value of the previous holiday<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the holidays based on the calendar in cultureInfo,
		/// otherwise it will simply skip weekends, or if you set a custom holiday provider, it will use that.
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousHoliday(this DateTime dt, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			DateTime nextWorkingDay = dt;

			// Loop until we find a weekday that is not a holiday or weekend
			nextWorkingDay = nextWorkingDay.PreviousDay();

			// Note: lets assume that after 2050 this library will no longer be in use
			//       and that no dates beyond 2050 will be calculated using holidays
			while (!CurrentHolidayProvider.IsPublicHoliday(nextWorkingDay, cultureInfo))
			{
				if (nextWorkingDay.Year <= DateTime.MinValue.Year)
				{
					return DateTime.MaxValue;
				}

				nextWorkingDay = nextWorkingDay.PreviousDay();
			}

			return nextWorkingDay;
		}

		/// <summary>
		/// Gets the DateTime value of the previous hour
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousHour(this DateTime dt)
		{
			return dt.AddHours(-1);
		}

		/// <summary>
		/// Gets the DateTime value of the previous millisecond
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousMillisecond(this DateTime dt)
		{
			return dt.AddMilliseconds(-1);
		}

		/// <summary>
		/// Gets the DateTime value of the previous minute
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousMinute(this DateTime dt)
		{
			return dt.AddMinutes(-1);
		}

		/// <summary>
		/// Gets the DateTime value of the month before
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object whose value is the same day the month before the given. If the month has less days then the day may change (e.g. 31 March to 28 Feb)</returns>
		public static DateTime PreviousMonth(this DateTime dt)
		{
			return dt.AddMonths(-1);
		}

		/// <summary>
		/// Gets the DateTime value of the previous second
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousSecond(this DateTime dt)
		{
			return dt.AddSeconds(-1);
		}
		/// <summary>
		/// Gets the DateOnly value of the same day the week before
		/// </summary>
		/// <param name="dt">The DateOnly object</param>
		/// <returns>A DateOnly object whose value is the week before the given, the weekday and time is not changed</returns>
		public static DateTime PreviousWeek(this DateTime dt)
		{
			return dt.AddDays(-DaysInWeek);
		}

		/// <summary>
		/// Gets the DateTime value of the weekend before this date. If the given date is already a weekend, the previous weekend is returned.
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object representing the previous weekend (always Sunday)</returns>
		public static DateTime PreviousWeekend(this DateTime dt)
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
		/// Gets the DateTime value of the previous working day according to the given CultureInfo.Calendar<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the working days based on the calendar in cultureInfo,
		/// otherwise it will simply skip weekends, or if you set a custom holiday provider, it will use that.
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousWorkday(this DateTime dt, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			DateTime nextWorkingDay = dt;

			nextWorkingDay = nextWorkingDay.PreviousDay();

			// Loop until we find a weekday that is not a holiday or weekend
			while (CurrentHolidayProvider.IsPublicHoliday(nextWorkingDay, cultureInfo) || nextWorkingDay.IsWeekend())
			{
				nextWorkingDay = nextWorkingDay.PreviousDay();
			}

			return nextWorkingDay;
		}
		/// <summary>
		/// Gets the DateTime value of the previous year
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousYear(this DateTime dt)
		{
			return dt.AddYears(-1);
		}

		/// <summary>
		/// Returns a DateTime object representing the first weekday of the given Week
		/// </summary>
		/// <param name="me">The DateTime value of which the first day is requested</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>A DateTime object with first day of the week</returns>
		public static DateTime StartOfWeek(this DateTime me, CultureInfo? cultureInfo = null)
		{
			return me.TruncateToWeek(cultureInfo);
		}
	}
}