using System.Runtime.CompilerServices;

namespace MoreDateTime.Extensions
{
	/// <summary>
	/// The extension methods for working with <see cref="TimeSpan"/> objects.
	/// </summary>
	public static partial class TimeSpanExtensions
	{
		/// <summary>
		/// The rounding unit for rounding TimeSpans
		/// </summary>
		public enum RoundingUnit
		{
			Day,
			Hour,
			Minute,
			Second
		}

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
		/// Returns if the value is negative
		/// </summary>
		/// <param name="ts">The TimeSpan to test</param>
		/// <returns>A bool.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegative(this TimeSpan ts)
		{
			return ts.Ticks < 0;
		}

		/// <summary>
		/// Returns if the value is positive
		/// </summary>
		/// <param name="ts">The TimeSpan to test</param>
		/// <returns>A bool.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPositive(this TimeSpan ts)
		{
			return ts.Ticks >= 0;   // should 0 be positive? Ignore?
		}

		/// <summary>
		/// Rounds the TimeSpan mathematically to the next unit of the given precision.
		/// </summary>
		/// <param name="ts">The TimeSpan</param>
		/// <param name="TruncateTo">The truncatation enum</param>
		/// <returns>A TimeSpan.</returns>
		public static TimeSpan RoundTo(this TimeSpan timeSpan, RoundingUnit roundingUnit)
		{
			return roundingUnit switch
			{
				RoundingUnit.Day => TimeSpan.FromDays(Math.Round(timeSpan.TotalDays)),
				RoundingUnit.Hour => TimeSpan.FromHours(Math.Round(timeSpan.TotalHours)),
				RoundingUnit.Minute => TimeSpan.FromMinutes(Math.Round(timeSpan.TotalMinutes)),
				RoundingUnit.Second => TimeSpan.FromSeconds(Math.Round(timeSpan.TotalSeconds)),
				_ => throw new ArgumentException("Invalid rounding unit specified.")
			};
		}

		/// <summary>
		/// Rounds the TimeSpan mathematically to the day
		/// </summary>
		/// <param name="ts">The TimeSpan</param>
		/// <returns>A TimeSpan.</returns>
		public static TimeSpan RoundToDay(this TimeSpan timeSpan)
		{
			return RoundTo(timeSpan, RoundingUnit.Day);
		}

		/// <summary>
		/// Rounds the TimeSpan mathematically to the Hour
		/// </summary>
		/// <param name="ts">The TimeSpan</param>
		/// <returns>A TimeSpan.</returns>
		public static TimeSpan RoundToHour(this TimeSpan timeSpan)
		{
			return RoundTo(timeSpan, RoundingUnit.Hour);
		}

		/// <summary>
		/// Rounds the TimeSpan mathematically to the Minute
		/// </summary>
		/// <param name="ts">The TimeSpan</param>
		/// <returns>A TimeSpan.</returns>
		public static TimeSpan RoundToMinute(this TimeSpan timeSpan)
		{
			return RoundTo(timeSpan, RoundingUnit.Minute);
		}

		/// <summary>
		/// Rounds the TimeSpan mathematically to the Second
		/// </summary>
		/// <param name="ts">The TimeSpan</param>
		/// <returns>A TimeSpan.</returns>
		public static TimeSpan RoundToSecond(this TimeSpan timeSpan)
		{
			return RoundTo(timeSpan, RoundingUnit.Second);
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
				TimeSpanTruncate.Days => new TimeSpan(dt.Days, 0, 0, 0),
				TimeSpanTruncate.Hours => new TimeSpan(dt.Days, dt.Hours, 0, 0),
				TimeSpanTruncate.Minutes => new TimeSpan(dt.Days, dt.Hours, dt.Minutes, 0),
				_ => new TimeSpan(dt.Days, dt.Hours, dt.Minutes, dt.Seconds)
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
	}
}