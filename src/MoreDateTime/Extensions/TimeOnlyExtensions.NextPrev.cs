using MoreDateTime.Extensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class TimeOnlyExtensions
	{
		/// <summary>
		/// Gets the TimeOnly value of the next millisecond
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly NextMillisecond(this TimeOnly dt)
		{
			return dt.AddMilliseconds(1);
		}

		/// <summary>
		/// Gets the TimeOnly value of the next second
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly NextSecond(this TimeOnly dt)
		{
			return dt.AddSeconds(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next full second (10:15:20.350 to 10:15:21.000)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static TimeOnly NextFullSecond(this TimeOnly dt)
		{
			return dt.AddSeconds(1).TruncateToSecond();
		}

		/// <summary>
		/// Gets the TimeOnly value of the next day
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly NextMinute(this TimeOnly dt)
		{
			return dt.AddMinutes(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next full minute (10:15:20 to 10:16:00)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static TimeOnly NextFullMinute(this TimeOnly dt)
		{
			return dt.AddMinutes(1).TruncateToMinute();
		}

		/// <summary>
		/// Gets the TimeOnly value of the next day
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly NextHour(this TimeOnly dt)
		{
			return dt.AddHours(1);
		}

		/// <summary>
		/// Gets the DateTime value of the next full hour (10:15 to 11:00, 10:45 to 11:00, etc)
		/// </summary>
		/// <param name="dt">The DateTime object</param>
		/// <returns>A DateTime object</returns>
		public static TimeOnly NextFullHour(this TimeOnly dt)
		{
			return dt.AddHours(1).TruncateToHour();
		}

		/// <summary>
		/// Gets the TimeOnly value of the Previous day
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly PreviousMillisecond(this TimeOnly dt)
		{
			return dt.AddMilliseconds(-1);
		}

		/// <summary>
		/// Gets the TimeOnly value of the Previous day
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly PreviousSecond(this TimeOnly dt)
		{
			return dt.AddSeconds(-1);
		}

		/// <summary>
		/// Gets the TimeOnly value of the Previous day
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly PreviousMinute(this TimeOnly dt)
		{
			return dt.AddMinutes(-1);
		}

		/// <summary>
		/// Gets the TimeOnly value of the Previous day
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly PreviousHour(this TimeOnly dt)
		{
			return dt.AddHours(-1);
		}
	}
}