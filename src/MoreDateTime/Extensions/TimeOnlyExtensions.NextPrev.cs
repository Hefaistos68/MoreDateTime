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
		/// Gets the TimeOnly value of the next day
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>A TimeOnly object</returns>
		public static TimeOnly NextMinute(this TimeOnly dt)
		{
			return dt.AddMinutes(1);
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