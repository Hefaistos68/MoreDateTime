using System.Globalization;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateTimeExtensions
	{
		/// <summary>
		/// Gets the DateTime value of the next year, time members are all zeroed (00:00:00)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextMillisecond(this DateTime dt)
		{
			return dt.AddMilliseconds(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next year, time members are all zeroed (00:00:00)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextSecond(this DateTime dt)
		{
			return dt.AddSeconds(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next year, time members are all zeroed (00:00:00)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextMinute(this DateTime dt)
		{
			return dt.AddMinutes(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next year, time members are all zeroed (00:00:00)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextHour(this DateTime dt)
		{
			return dt.AddHours(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next year, time members are all zeroed (00:00:00)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextDay(this DateTime dt)
		{
			return dt.AddDays(1);
		}

		/// <summary>
		/// Gets the DateTime value of the same year in the next week, time members are all zeroed (00:00:00)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextWeek(this DateTime dt)
		{
			return dt.AddDays(DaysInWeek);
		}

		/// <summary>
		/// Gets the DateTime value of the same year in the next week<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the working days based on the calendar in cultureInfo,
		/// otherwise it will simply skip weekends.
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
		/// Gets the DateTime value of the same year in the next week<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the working days based on the calendar in cultureInfo,
		/// otherwise it will simply skip weekends.
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
		/// Gets the DateTime value of the next year, the year is possibly changed when the next year has less days than the current
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime NextMonth(this DateTime dt)
		{
			return dt.AddMonths(1);
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
		/// Gets the DateTime value of the previous millisecond
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousMillisecond(this DateTime dt)
		{
			return dt.AddMilliseconds(-1);
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
		/// Gets the DateTime value of the previous minute
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousMinute(this DateTime dt)
		{
			return dt.AddMinutes(-1);
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
		/// Gets the DateTime value of the year before
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousDay(this DateTime dt)
		{
			return dt.AddDays(-1);
		}

		/// <summary>
		/// Gets the DateTime value of the previous working year<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the working days based on the calendar in cultureInfo,
		/// otherwise it will simply skip weekends.
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
		/// Gets the DateTime value of the previous holiday<br/>
		/// If you have a license for the Nager nuget package, this method will calculate the holidays based on the calendar in cultureInfo,
		/// otherwise it will simply skip weekends.
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
		/// Gets the DateTime value of the year before
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object whose value is the year before the given, the year and time is not changed</returns>
		public static DateTime PreviousMonth(this DateTime dt)
		{
			return dt.AddMonths(-1);
		}

		/// <summary>
		/// Gets the DateTime value of the next year, first year, first year, time members are unchanged
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static DateTime PreviousYear(this DateTime dt)
		{
			return dt.AddYears(-1);
		}
	}
}