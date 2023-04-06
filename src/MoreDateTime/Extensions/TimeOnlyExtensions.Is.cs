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
			return to.Hour.IsBetween(12, 18);
		}

		/// <summary>
		/// Tests if the time in the given TimeOnly object is between 18:00:00 and 06:00:00
		/// </summary>
		/// <param name="to">The TimeOnly object</param>
		/// <returns>True if time is between 18:00:00.000 and 06:00:00.000</returns>
		public static bool IsNight(this TimeOnly to)
		{
			return to.Hour.IsBetween(18, 23) || to.Hour.IsBetween(0, 6);
		}

		/// <summary>
		/// Tests if the time in the given TimeOnly object is midday (12:00:00.000)
		/// </summary>
		/// <param name="to">The first TimeOnly argument</param>
		/// <returns>True if time is 00:00:00.000</returns>
		public static bool IsMorning(this TimeOnly to)
		{
			return to.Hour.IsBetween(6, 12);
		}

		/// <summary>
		/// Checks if the given value is between the given startTime and endTime values
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

			return (me >= startTime) && (me <= endTime);
		}
	}
}