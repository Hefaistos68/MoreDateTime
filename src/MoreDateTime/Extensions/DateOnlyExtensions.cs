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
		/// <summary>You'd never guess it, its the number of days in a week</summary>
		public const int DaysInWeek = 7;

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
		/// <param name="me">The DateTime value of which the first day is requested</param>
		/// <returns>A DateTime object with day 1, time members set to 0 (00:00:00)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateOnly StartOfMonth(this DateOnly me)
		{
			return me.TruncateToMonth();
		}

		/// <summary>
		/// Returns a DateTime object representing the first day of the current month
		/// </summary>
		/// <param name="me">The DateTime value of which the first day is requested</param>
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
		/// Returns the <see cref="DateTime"/> as <see cref="TimeOnly"/>, stripping the date component
		/// </summary>
		/// <param name="me">The <see cref="DateTime"/> object</param>
		/// <returns>A <see cref="TimeOnly"/> object representing the Time part of the given DateTime object</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeOnly ToTimeOnly(this DateTime me)
		{
			return new TimeOnly(me.Hour, me.Minute, me.Second, me.Millisecond);
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
			if(Math.Abs(timeSpan.TotalDays) < 1)
			{
				return me;
			}

			return (me.ToDateTime().Add(timeSpan)).ToDateOnly();
		}

		/// <summary>
		/// Adds a number of ticks to a DateOnly object. If the number of ticks is less than a whole day, the same value is returned
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="ticks">The number of ticks to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="ticks"/></returns>
		public static DateOnly AddTicks(this DateOnly me, long ticks)
		{
			if(ticks < TicksPerDay)
			{
				return me;
			}

			return (me.ToDateTime().AddTicks(ticks)).ToDateOnly();
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
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="value"/></returns>
		public static DateOnly AddMinutes(this DateOnly me, double value)
		{
			return me.Add(TimeSpan.FromMinutes(value));
		}

		/// <summary>
		/// Adds minutes to a DateOnly object. If the values is less than a 86_400 seconds (a whole day), the same value is returned
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="value">The time span to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="value"/></returns>
		public static DateOnly AddSeconds(this DateOnly me, double value)
		{
			return me.Add(TimeSpan.FromSeconds(value));
		}

		/// <summary>
		/// Adds minutes to a DateOnly object. If the values is less than a 86_400_000 seconds (a whole day), the same value is returned
		/// </summary>
		/// <param name="me">The DateOnly object</param>
		/// <param name="value">The time span to add</param>
		/// <returns>A DateOnly object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="value"/></returns>
		public static DateOnly AddMilliseconds(this DateOnly me, double value)
		{
			return me.Add(TimeSpan.FromMilliseconds(value));
		}

		/// <summary>
		/// Returns a new DateOnly that subtracts the value of the specified TimeSpan from the value of this instance
		/// </summary>
		/// <param name="me">The DateOnly object to subtract the value from</param>
		/// <param name="timeSpan">A positive time interval</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance minus the time interval represented by value</returns>
		public static DateOnly Sub(this DateOnly me, TimeSpan timeSpan)
		{
			return me.Add(-timeSpan);
		}

		/// <summary>
		/// Returns the distance as a TimeSpan between two DateOnly objects. The result is always positive
		/// </summary>
		/// <param name="startDate">The startDate object</param>
		/// <param name="endDate">The endDate object</param>
		/// <returns>A TimeSpan which expresses the difference between the two dates</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan Distance(this DateOnly startDate, DateOnly endDate)
		{
			return (startDate > endDate) ? (startDate.ToDateTime() - endDate.ToDateTime()) : (endDate.ToDateTime() - startDate.ToDateTime());
		}

		/// <summary>
		/// Splits the given range of DateOnly into the given number of parts.
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of DateOnlyRanges</returns>
		public static List<DateOnlyRange> Split(this DateOnly startDate, DateOnly endDate, int parts)
		{
			if (parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			var result = new List<DateOnlyRange>();
			var distance = startDate.Distance(endDate);
			var partDistance = distance.Ticks / parts;
			var part = startDate;

			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new DateOnlyRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}

		/// <summary>
		/// Splits the given range of DateOnly into the given number of parts.
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="distance">The timespan to split</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of DateOnlyRanges</returns>
		public static List<DateOnlyRange> Split(this DateOnly startDate, TimeSpan distance, int parts)
		{
			if (parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			if (distance.Ticks < parts)
			{
				throw new ArgumentOutOfRangeException(nameof(distance), "The ticks in distance must be greater than the number of parts");
			}

			var result = new List<DateOnlyRange>();
			var partDistance = distance.Ticks / parts;
			var part = startDate;

			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new DateOnlyRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}

		/// <summary>
		/// Splits the given range of DateOnly into the given number of parts.
		/// </summary>
		/// <param name="dates">The start and end date</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of DateOnlyRanges</returns>
		public static List<DateOnlyRange> Split(this DateOnlyRange dates, int parts)
		{
			if (dates is null)
			{
				throw new ArgumentNullException(nameof(dates));
			}

			if (parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			var result = new List<DateOnlyRange>();
			var distance = dates.Distance();
			var partDistance = distance.Ticks / parts;
			var part = dates.Start;

			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new DateOnlyRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}

	}
}
