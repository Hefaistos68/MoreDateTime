using MoreDateTime.Extensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class TimeOnlyExtensions
	{
		/// <summary>
		/// Tests if the time in the given TimeOnly object is midnight (0h)
		/// </summary>
		/// <param name="to">The first TimeOnly argument</param>
		/// <param name="ignoreMilliseconds">Determines if the milliseconds will be taken into account or not</param>
		/// <returns>True if time is 0h ignoring minures and seconds</returns>
		public static bool IsFullHour(this TimeOnly to, bool ignoreMilliseconds = true)
		{
			return (to.Minute == 0) && (to.Second == 0) && (ignoreMilliseconds ? true : (to.Millisecond == 0));
		}

		/// <summary>
		/// Tests if the time in the given TimeOnly object is midnight (0h)
		/// </summary>
		/// <param name="to">The first TimeOnly argument</param>
		/// <param name="ignoreMilliseconds">Determines if the milliseconds will be taken into account or not</param>
		/// <returns>True if time is 0h ignoring minures and seconds</returns>
		public static bool IsFullMinute(this TimeOnly to, bool ignoreMilliseconds = true)
		{
			return (to.Second == 0) && (ignoreMilliseconds ? true : (to.Millisecond == 0));
		}

		/// <summary>
		/// Tests if the time in the given TimeOnly object is midnight (0h)
		/// </summary>
		/// <param name="to">The first TimeOnly argument</param>
		/// <returns>True if time is 0h ignoring minures and seconds</returns>
		public static bool IsMidnight(this TimeOnly to)
		{
			return (to.Hour == 0);
		}

		/// <summary>
		/// Tests if the time in the given TimeOnly object is midday (12h)
		/// </summary>
		/// <param name="to">The TimeOnly object</param>
		/// <returns>True if time is 12h ignoring minutes and seconds</returns>
		public static bool IsMidday(this TimeOnly to)
		{
			return (to.Hour == 12);
		}

		/// <summary>
		/// Tests if the time in the given TimeOnly object is considered afternoon (12:00:00.000 - 18:00:00.000)
		/// </summary>
		/// <param name="to">The first TimeOnly argument</param>
		/// <returns>True if time is between 12:00:00.000 and 18:00:00.000</returns>
		public static bool IsAfternoon(this TimeOnly to)
		{
			return to.Hour.IsWithin(12, 18);
		}

		/// <summary>
		/// Tests if the time in the given TimeOnly object is between 18:00:00 and 06:00:00
		/// </summary>
		/// <param name="to">The TimeOnly object</param>
		/// <returns>True if time is between 18:00:00.000 and 06:00:00.000</returns>
		public static bool IsNight(this TimeOnly to)
		{
			return to.Hour.IsWithin(18, 23) || to.Hour.IsWithin(0, 6);
		}

		/// <summary>
		/// Tests if the time in the given TimeOnly object is midday (12:00:00.000)
		/// </summary>
		/// <param name="to">The first TimeOnly argument</param>
		/// <returns>True if time is 00:00:00.000</returns>
		public static bool IsMorning(this TimeOnly to)
		{
			return to.Hour.IsWithin(6, 12);
		}

		/* There is an BCL implementation of this, although I do not agree with it.
			
		/// <summary>
		/// Checks if the given value is between the given startTime and endTime values, not including start or end time
		/// </summary>
		/// <param name="me">The TimeOnly to compare</param>
		/// <param name="startTime">The start time</param>
		/// <param name="endTime">The end time</param>
		/// <returns>True if the value is greater or equal startTime and less than or equal endTime</returns>
		public static bool IsBetween(this TimeOnly me, TimeOnly startTime, TimeOnly endTime)
		{
			if (endTime < startTime)
			{
				throw new ArgumentException("endTime must be greater than startTime");
			}

			return (me > startTime) && (me < endTime);
		}
		*/

		/// <summary>
		/// Checks if the given value is between the given startTime and endTime values, not including start or end time
		/// </summary>
		/// <param name="me">The TimeOnly to compare</param>
		/// <param name="startTime">The start time</param>
		/// <param name="endTime">The end time</param>
		/// <returns>True if the value is greater or equal startTime and less than or equal endTime</returns>
		public static bool IsBetween(this DateTime me, TimeOnly startTime, TimeOnly endTime)
		{
			return me.ToTimeOnly().IsBetween(startTime, endTime);
		}

		/// <summary>
		/// Checks if the given value is between the given startTime and endTime values, including start or end time
		/// </summary>
		/// <param name="me">The TimeOnly to compare</param>
		/// <param name="startTime">The start time</param>
		/// <param name="endTime">The end time</param>
		/// <returns>True if the value is greater or equal startTime and less than or equal endTime</returns>
		public static bool IsWithin(this TimeOnly me, TimeOnly startTime, TimeOnly endTime)
		{
			if (endTime < startTime)
				return (me <= startTime) || (me >= endTime);
			else
				return (me >= startTime) && (me <= endTime);
		}

		/// <summary>
		/// Checks if the given value is between the given startTime and endTime values, including start or end time
		/// </summary>
		/// <param name="me">The TimeOnly to compare</param>
		/// <param name="startTime">The start time</param>
		/// <param name="endTime">The end time</param>
		/// <returns>True if the value is greater or equal startTime and less than or equal endTime</returns>
		public static bool IsWithin(this DateTime me, TimeOnly startTime, TimeOnly endTime)
		{
			return IsWithin(me.ToTimeOnly(), startTime, endTime);
		}
		/// <summary>
		/// Checks if the given value is between the given start and end values, including start date and end date
		/// </summary>
		/// <param name="me">The DateTime to compare</param>
		/// <param name="range">The date range to check</param>
		/// <returns>True if the value is greater or equal range.Start and less than or equal range.End</returns>
		public static bool IsWithin(this TimeOnly me, TimeOnlyRange range)
		{
			if (!range.IsOrdered())
			{
				throw new ArgumentException("End must be greater than Start");
			}

			return range.Contains(me);
		}

	}
}