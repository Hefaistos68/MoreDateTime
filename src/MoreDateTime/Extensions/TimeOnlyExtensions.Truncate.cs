using System.Globalization;

using MoreDateTime.Extensions;

using static MoreDateTime.Extensions.DateTimeExtensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class TimeOnlyExtensions
	{
		/// <summary>
		/// Truncates the precision of a TimeOnly object to the given precision
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <param name="TruncateTo">The precision to truncate to</param>
		/// <returns>The Truncated TimeOnly object</returns>
		public static TimeOnly TruncateTo(this TimeOnly dt, DateTruncate TruncateTo)
		{
			return TruncateTo switch
			{
				DateTruncate.Hour => new TimeOnly(dt.Hour, 0, 0),
				DateTruncate.Minute => new TimeOnly(dt.Hour, dt.Minute, 0),
				DateTruncate.Second => new TimeOnly(dt.Hour, dt.Minute, dt.Second),
				_ => new TimeOnly(dt.Hour, dt.Minute, dt.Second, dt.Millisecond)
			};
		}

		/// <summary>
		/// Truncates the precision of a TimeOnly object to the hour
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>The Truncated TimeOnly object</returns>
		public static TimeOnly TruncateToHour(this TimeOnly dt)
		{
			return dt.TruncateTo(DateTruncate.Hour);
		}

		/// <summary>
		/// Truncates the precision of a TimeOnly object to the minute
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>The Truncated TimeOnly object</returns>
		public static TimeOnly TruncateToMinute(this TimeOnly dt)
		{
			return dt.TruncateTo(DateTruncate.Minute);
		}

		/// <summary>
		/// Truncates the precision of a TimeOnly object to the second
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>The Truncated TimeOnly object</returns>
		public static TimeOnly TruncateToSecond(this TimeOnly dt)
		{
			return dt.TruncateTo(DateTruncate.Second);
		}
	}
}