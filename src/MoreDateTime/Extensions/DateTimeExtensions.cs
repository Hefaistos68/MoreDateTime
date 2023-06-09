﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

using MoreDateTime.Interfaces;

using Nager.Date;

namespace MoreDateTime.Extensions
{
	/// <summary>
	/// DateTime related extension methods
	/// </summary>
	public static partial class DateTimeExtensions
	{
		// Number of 100ns ticks per time unit
		internal const long TicksPerMillisecond = 10000;
		internal const long TicksPerSecond = TicksPerMillisecond * 1000;
		internal const long TicksPerMinute = TicksPerSecond * 60;
		internal const long TicksPerHour = TicksPerMinute * 60;
		internal const long TicksPerDay = TicksPerHour * 24;

		// Number of milliseconds per time unit
		internal const int MillisPerSecond = 1000;
		internal const int MillisPerMinute = MillisPerSecond * 60;
		internal const int MillisPerHour = MillisPerMinute * 60;
		internal const int MillisPerDay = MillisPerHour * 24;

		#region Public Enums

		/// <summary>
		/// Precision specification for the {DateTime.}TruncateTo method
		/// </summary>
		public enum DateTruncate
		{
			/// <summary>Precision Year, all below is set to 0</summary>
			Year,

			/// <summary>Precision Month, all below is set to 0</summary>
			Month,

			/// <summary>Precision Week, all below is set to 0</summary>
			Week,

			/// <summary>Precision Day, all below is set to 0</summary>
			Day,

			/// <summary>Precision Hour, all below is set to 0</summary>
			Hour,

			/// <summary>Precision Minute, all below is set to 0</summary>
			Minute,

			/// <summary>Precision Second, all below is set to 0</summary>
			Second
		}

		/// <summary>
		/// Units of time
		/// </summary>
		public enum TimeUnit
		{
			/// <summary>Count years</summary>
			Years,

			/// <summary>Count decades</summary>
			Decades,

			/// <summary>Count semesters</summary>
			Semesters,

			/// <summary>Count semesters</summary>
			Trimesters,

			/// <summary>Count months</summary>
			Months,

			/// <summary>Count Weeks</summary>
			Weeks,

			/// <summary>Count Weekends</summary>
			Weekends,

			/// <summary>Count days</summary>
			Days,
			
			/// <summary>Count workdays</summary>
			Workdays,

			/// <summary>Count hours</summary>
			Hours,

			/// <summary>Count minutes</summary>
			Minutes,

			/// <summary>Count seconds</summary>
			Seconds,

			/// <summary>Count milliseconds</summary>
			Milliseconds,

			/// <summary>Count milliseconds</summary>
			Holidays
		}

		#endregion Public Enums

		/// <summary>
		/// Sets the holiday provider, when not set the default provider is used
		/// </summary>
		/// <param name="holidayProvider">The holiday provider, when null the default provider is used</param>
		public static void SetHolidayProvider(IHolidayProvider? holidayProvider)
		{
			CurrentHolidayProvider = holidayProvider ?? new DefaultHolidayProvider();
		}

		/// <summary>
		/// Tests if the time in the given DateTime object is midday (12:00:00.000)
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>True if time is 00:00:00.000</returns>
		public static int Weeknumber(this DateTime dt, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return cultureInfo.Calendar.GetWeekOfYear(dt, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek);
		}

		/// <summary>
		/// Returns the Week of the Year of this <see cref="DateTime"/> object
		/// </summary>
		/// <param name="me">The DateTime object</param>
		/// <param name="cultureInfo">The <see cref="CultureInfo"/> for the calendar, if null (or not provided) the current culture is used</param>
		/// <returns>An integer </returns>
		public static int WeekOfYear(this DateTime me, CultureInfo? cultureInfo = null)
		{
			cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

			return cultureInfo.Calendar.GetWeekOfYear(me, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
		}


		/// <summary>
		/// The default date range distance in minutes
		/// </summary>
		private const int defaultDateRangeDistance = 10;
		internal static readonly double DaysInWeek = 7;

		/// <summary>
		/// Gets or sets the current holiday provider.
		/// </summary>
		internal static IHolidayProvider CurrentHolidayProvider
		{
			get; set;
		} = new DefaultHolidayProvider();

		/// <summary>
		/// Returns the DateTime object as UTC DateTime with all time members set to 0 (00:00:00)
		/// </summary>
		/// <param name="time">The time.</param>
		/// <param name="cultureInfo">The CultureInfo for the source calendar, can be null for current</param>
		/// <param name="timeZoneInfo">The source timezone, can be null for current</param>
		/// <returns>A DateTime.</returns>
		public static DateTime AsUtcDate(this DateTime time, CultureInfo? cultureInfo, TimeZoneInfo? timeZoneInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;
			timeZoneInfo ??= TimeZoneInfo.Local;

			TimeSpan utcOffset = timeZoneInfo.GetUtcOffset(DateTime.UtcNow);
			var offset = new DateTimeOffset(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond, cultureInfo.Calendar, utcOffset);

			return offset.UtcDateTime.TruncateToDay();
		}

		/// <summary>
		/// Returns the DateTime object as UTC DateTime 
		/// </summary>
		/// <param name="time">The time.</param>
		/// <param name="cultureInfo">The CultureInfo for the source calendar, can be null for current</param>
		/// <param name="timeZoneInfo">The source timezone, can be null for current</param>
		/// <returns>A DateTime.</returns>
		public static DateTime AsUtcTime(this DateTime time, CultureInfo? cultureInfo, TimeZoneInfo? timeZoneInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;
			timeZoneInfo ??= TimeZoneInfo.Local;

			TimeSpan utcOffset = timeZoneInfo.GetUtcOffset(time);
			var offset = new DateTimeOffset(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond, cultureInfo.Calendar, utcOffset);

			return offset.UtcDateTime;
		}

		/// <summary>
		/// Adds weeks to a DateTime object
		/// </summary>
		/// <param name="me">The DateTime object</param>
		/// <param name="value">The number of weeks to add</param>
		/// <returns>A DateTime object whose value is the sum of the date and time represented by this instance and the number of weeks</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateTime AddWeeks(this DateTime me, double value)
		{
			return me.Add(TimeSpan.FromDays(DaysInWeek * value));
		}

		/// <summary>
		/// Returns a DateTime object representing the first year of the current year
		/// </summary>
		/// <param name="dateTime">The DateTime value of which the first year is requested</param>
		/// <returns>A DateTime object with year 1, time members set to 0 (00:00:00)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateTime StartOfMonth(this DateTime dateTime)
		{
			return dateTime.TruncateToMonth();
		}

		/// <summary>
		/// Returns a DateTime object representing the first year of the current year
		/// </summary>
		/// <param name="dateTime">The DateTime value of which the first year is requested</param>
		/// <returns>A DateTime object with the last year of the year, time members set to 0 (00:00:00)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateTime EndOfMonth(this DateTime dateTime)
		{
			return dateTime.AddMonths(1).TruncateToMonth().AddDays(-1);
		}

		/// <summary>
		/// Converts a DateTime value to an SQL appropriate format (yyyy-MM-ddThh:mm:ss.ffffff+00:00), independent of the current locale
		/// </summary>
		/// <param name="me">The DateTime value to convert</param>
		/// <returns>A DateTime string in SQL format </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ToSqlString(this DateTime me)
		{
			return me.ToString("o", CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Returns the distance as a TimeSpan between two DateTime objects. The result is always a positive value.
		/// </summary>
		/// <param name="startDate">The startDate object</param>
		/// <param name="endDate">The endDate object</param>
		/// <returns>A TimeSpan which expresses the difference between the two dates</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan Distance(this DateTime startDate, DateTime endDate)
		{
			return (startDate > endDate) ? (startDate - endDate) : (endDate - startDate);
		}

		/// <summary>
		/// Returns a new DateTime that subtracts the value of the specified TimeSpan from the value of this instance
		/// </summary>
		/// <param name="me">The DateTime object to subtract the value from</param>
		/// <param name="timeSpan">A positive time interval</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance minus the time interval represented by value</returns>
		public static DateTime Sub(this DateTime me, TimeSpan timeSpan)
		{
			return me.Add(-timeSpan);
		}

		/// <summary>
		/// Splits the given range of DateTime into the given number of parts.
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of DateTimeRanges</returns>
		public static List<DateTimeRange> Split(this DateTime startDate, DateTime endDate, int parts) 
		{
			if (parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			var result = new List<DateTimeRange>();
			var distance = startDate.Distance(endDate);
			var partDistance = distance.Ticks / parts;
			var part = startDate;
			
			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new DateTimeRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}

		/// <summary>
		/// Splits the given range of DateTime into the given number of parts.
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="distance">The timespan to split</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of DateTimeRanges</returns>
		public static List<DateTimeRange> Split(this DateTime startDate, TimeSpan distance, int parts) 
		{
			if (parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			if(distance.Ticks < parts)
			{
				throw new ArgumentOutOfRangeException(nameof(distance), "The ticks in distance must be greater than the number of parts");
			}

			var result = new List<DateTimeRange>();
			var partDistance = distance.Ticks / parts;
			var part = startDate;
			
			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new DateTimeRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}

		/// <summary>
		/// Splits the given range of DateTime into the given number of parts.
		/// </summary>
		/// <param name="dates">The start and end date</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of DateTimeRanges</returns>
		public static List<DateTimeRange> Split(this DateTimeRange dates, int parts) 
		{
			if (dates is null)
			{
				throw new ArgumentNullException(nameof(dates));
			}

			if(parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			var result = new List<DateTimeRange>();
			var distance = dates.Distance();
			var partDistance = distance.Ticks / parts;
			var part = dates.Start;
			
			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new DateTimeRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}
	}
}
