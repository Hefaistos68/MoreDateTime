using System.Globalization;

using static MoreDateTime.Extensions.DateTimeExtensions;

namespace MoreDateTime.Extensions
{
	/// <summary>
	/// The extension methods for working with <see cref="TimeSpan"/> objects.
	/// </summary>
	public static partial class TimeSpanExtensions
	{
		/// <summary>
		/// Precision specification for the {DateTime.}TruncateTo method
		/// </summary>
		public enum TimeSpanTruncate
		{
			/// <summary>Precision Week, all below is set to 0</summary>
			Weeks,

			/// <summary>Precision Day, all below is set to 0</summary>
			Days,

			/// <summary>Precision Hour, all below is set to 0</summary>
			Hours,

			/// <summary>Precision Minute, all below is set to 0</summary>
			Minutes,

			/// <summary>Precision Second, all below is set to 0</summary>
			Seconds
		}

		/// <summary>
		/// Truncates the precision of a TimeSpan object to the given precision
		/// </summary>
		/// <param name="dt">The TimeSpan object</param>
		/// <param name="TruncateTo">The precision to truncate to</param>
		/// <returns>The Truncated TimeSpan object</returns>
		public static TimeSpan TruncateTo(this TimeSpan dt, TimeSpanTruncate TruncateTo)
		{
			return TruncateTo switch
			{
				TimeSpanTruncate.Weeks => new TimeSpan(0, 0, 0),
				TimeSpanTruncate.Days => new TimeSpan(dt.Days, 0, 0, 0),
				TimeSpanTruncate.Hours => new TimeSpan(dt.Hours, 0, 0),
				TimeSpanTruncate.Minutes => new TimeSpan(dt.Hours, dt.Minutes, 0),
				_ => new TimeSpan(dt.Hours, dt.Minutes, dt.Seconds)
			};
		}

		/// <summary>
		/// Truncates the precision of a DateTime object to the year
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>The Truncated dateTime object</returns>
		public static TimeSpan TruncateToDay(this TimeSpan dt)
		{
			return dt.TruncateTo(TimeSpanTruncate.Days);
		}

		/// <summary>
		/// Truncates the precision of a TimeSpan object to the hour
		/// </summary>
		/// <param name="dt">The TimeSpan object</param>
		/// <returns>The Truncated TimeSpan object</returns>
		public static TimeSpan TruncateToHour(this TimeSpan dt)
		{
			return dt.TruncateTo(TimeSpanTruncate.Hours);
		}

		/// <summary>
		/// Truncates the precision of a TimeSpan object to the minute
		/// </summary>
		/// <param name="dt">The TimeSpan object</param>
		/// <returns>The Truncated TimeSpan object</returns>
		public static TimeSpan TruncateToMinute(this TimeSpan dt)
		{
			return dt.TruncateTo(TimeSpanTruncate.Minutes);
		}

		/// <summary>
		/// Truncates the precision of a TimeSpan object to the second
		/// </summary>
		/// <param name="dt">The TimeSpan object</param>
		/// <returns>The Truncated TimeSpan object</returns>
		public static TimeSpan TruncateToSecond(this TimeSpan dt)
		{
			return dt.TruncateTo(TimeSpanTruncate.Seconds);
		}

		/// <summary>
		/// Rounds the TimeSpan mathematically to the next unit of the given precision.
		/// </summary>
		/// <param name="ts">The TimeSpan</param>
		/// <param name="TruncateTo">The truncatation enum</param>
		/// <returns>A TimeSpan.</returns>
		public static TimeSpan RoundTo(this TimeSpan ts, TimeSpanTruncate TruncateTo)
		{
			return TruncateTo switch
			{
				TimeSpanTruncate.Weeks => new TimeSpan((int)(ts.Days/DaysInWeek), 0, 0, 0),
				TimeSpanTruncate.Days => new TimeSpan(ts.Days, 0, 0, 0),
				TimeSpanTruncate.Hours => new TimeSpan(ts.Hours, 0, 0),
				TimeSpanTruncate.Minutes => new TimeSpan(ts.Hours, ts.Minutes, 0),
				TimeSpanTruncate.Seconds => new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds),
				_ => new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds)
			};
		}
	}
}