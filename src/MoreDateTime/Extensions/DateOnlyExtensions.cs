using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Nager.Date;

using static MoreDateTime.Extensions.DateTimeExtensions;

namespace MoreDateTime.Extensions
{
	/// <summary>
	/// The extension methods for working with <see cref="DateOnly"/> objects.
	/// </summary>
	public static partial class DateOnlyExtensions
	{
		public static int DaysInWeek = 7;

		/// <summary>
		/// Returns the Week of the Year of this <see cref="DateOnly"/> object
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="cultureInfo">The <see cref="CultureInfo"/> for the calendar, if null (or not provided) the current culture is used</param>
		/// <returns>An integer </returns>
		public static int WeekOfYear(this DateOnly me, CultureInfo? cultureInfo = null)
		{
			cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;
			
			return cultureInfo.Calendar.GetWeekOfYear(me.ToDateTime(), CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
		}

		/// <summary>
		/// Tests if the time in the given DateTime object is midday (12:00:00.000)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>True if time is 00:00:00.000</returns>
		public static int Weeknumber(this DateOnly dt, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return cultureInfo.Calendar.GetWeekOfYear(dt.ToDateTime(), cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek);
		}

		/// <summary>
		/// Returns a <see cref="DateTime"/> object with the time component set to 0 (00:00:00)
		/// </summary>
		/// <param name="me">The DateOnly object to convert</param>
		/// <param name="kind">The kind of DateTime object to create</param>
		/// <returns>A <see cref="DateTime"/> instance composed of the current <see cref="DateOnly"/> instance and time set to 0 (00:00:00)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateTime ToDateTime(this DateOnly me, DateTimeKind kind = DateTimeKind.Unspecified)
		{
			return me.ToDateTime(new TimeOnly(0), DateTimeKind.Unspecified);
		}

		/// <summary>
		/// Returns a DateTime object representing the first day of the current month
		/// </summary>
		/// <param name="dateTime">The DateTime value of which the first day is requested</param>
		/// <returns>A DateTime object with day 1, time members set to 0 (00:00:00)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateOnly StartOfMonth(this DateOnly me)
		{
			return me.TruncateToMonth();
		}

		/// <summary>
		/// Returns a DateTime object representing the first day of the current month
		/// </summary>
		/// <param name="dateTime">The DateTime value of which the first day is requested</param>
		/// <returns>A DateTime object with the last day of the month, time members set to 0 (00:00:00)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateOnly EndOfMonth(this DateOnly me)
		{
			return me.NextMonth().TruncateToMonth().PreviousDay();
		}

		/// <summary>
		/// Returns the <see cref="DateTime"/> as <see cref="DateOnly"/>
		/// </summary>
		/// <param name="me">The <see cref="DateTime"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the .Date part of the given DateTime object</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateOnly ToDateOnly(this DateTime me)
		{
			return new DateOnly(me.Year, me.Month, me.Day);
		}

		/// <summary>
		/// Converts a DateTime value to an SQL appropriate format (yyyy-MM-dd), independent of the current locale
		/// </summary>
		/// <param name="me">The DateTime value to convert</param>
		/// <returns>A DateTime string in SQL format </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ToSqlString(this DateOnly me)
		{
			return me.ToString("o", CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Adds a timespan to a DateOnly object. If the timespan is less than a whole day, the same value is returned
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="timeSpan">The time span to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="timeSpan"/></returns>
		public static DateOnly Add(this DateOnly me, TimeSpan timeSpan)
		{
			if(timeSpan.TotalDays < 1)
			{
				return me;
			}

			return (me.ToDateTime().Add(timeSpan)).ToDateOnly();
		}

		/// <summary>
		/// Adds weeks to a DateOnly object
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="value">The number of weeks to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the number of weeks</returns>
		public static DateOnly AddWeeks(this DateOnly me, double value)
		{
			return me.Add(TimeSpan.FromDays(DaysInWeek * value));
		}

		/// <summary>
		/// Adds hours to a DateOnly object. If the value is less than 24 hours (a whole day), the same Date is returned
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="value">The number of hours to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the number of hours in value</returns>
		public static DateOnly AddHours(this DateOnly me, double value)
		{
			return me.Add(TimeSpan.FromHours(value));
		}

		/// <summary>
		/// Adds minutes to a DateOnly object. If the values is less than a 1440 minutes (a whole day), the same value is returned
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="value">The number of minutes to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="timeSpan"/></returns>
		public static DateOnly AddMinutes(this DateOnly me, double value)
		{
			return me.Add(TimeSpan.FromMinutes(value));
		}

		/// <summary>
		/// Adds minutes to a DateOnly object. If the values is less than a 86_400 seconds (a whole day), the same value is returned
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="timeSpan">The time span to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="timeSpan"/></returns>
		public static DateOnly AddSeconds(this DateOnly me, double value)
		{
			return me.Add(TimeSpan.FromSeconds(value));
		}

		/// <summary>
		/// Adds minutes to a DateOnly object. If the values is less than a 86_400_000 seconds (a whole day), the same value is returned
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="timeSpan">The time span to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="timeSpan"/></returns>
		public static DateOnly AddMilliseconds(this DateOnly me, double value)
		{
			return me.Add(TimeSpan.FromMilliseconds(value));
		}

		/// <summary>
		/// Returns the distance as a TimeSpan between two DateOnly objects.
		/// </summary>
		/// <param name="me">The startDate object</param>
		/// <param name="other">The endDate object</param>
		/// <returns>A TimeSpan which expresses the difference between the two dates</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan Distance(this DateOnly me, DateOnly other)
		{
			return me.ToDateTime() - other.ToDateTime();
		}
	}
}
