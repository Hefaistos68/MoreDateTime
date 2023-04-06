using System.Globalization;

using MoreDateTime.Extensions;

using static MoreDateTime.Extensions.DateTimeExtensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class TimeOnlyExtensions
	{
		/// <summary>
		/// Count the number of Hours between startTime and endTime
		/// </summary>
		/// <param name="startTime">The start time</param>
		/// <param name="endTime">The end time</param>
		/// <returns>The number of Hours between startTime and endTime as a double with fractions</returns>
		public static double NumberOfHoursUntil(this TimeOnly startTime, TimeOnly endTime)
		{
			return NumberOf_Until(startTime, endTime, TimeUnit.Hours);
		}

		/// <summary>
		/// Count the number of Milliseconds between startTime and endTime
		/// </summary>
		/// <param name="startTime">The start time</param>
		/// <param name="endTime">The end time</param>
		/// <returns>The number of Milliseconds between startTime and endTime as a double with fractions</returns>
		public static double NumberOfMillisecondsUntil(this TimeOnly startTime, TimeOnly endTime)
		{
			return NumberOf_Until(startTime, endTime, TimeUnit.Milliseconds);
		}

		/// <summary>
		/// Count the number of Minutes between startTime and endTime
		/// </summary>
		/// <param name="startTime">The start time</param>
		/// <param name="endTime">The end time</param>
		/// <returns>The number of Minutes between startTime and endTime as a double with fractions</returns>
		public static double NumberOfMinutesUntil(this TimeOnly startTime, TimeOnly endTime)
		{
			return NumberOf_Until(startTime, endTime, TimeUnit.Minutes);
		}

		/// <summary>
		/// Count the number of Seconds between startTime and endTime
		/// </summary>
		/// <param name="startTime">The start time</param>
		/// <param name="endTime">The end time</param>
		/// <returns>The number of Seconds between startTime and endTime as a double with fractions</returns>
		public static double NumberOfSecondsUntil(this TimeOnly startTime, TimeOnly endTime)
		{
			return NumberOf_Until(startTime, endTime, TimeUnit.Seconds);
		}

		/// <summary>
		/// Count the number of given time units between startTime and endTime
		/// </summary>
		/// <param name="startTime">The start time.</param>
		/// <param name="endTime">The end time.</param>
		/// <param name="u">The TimeUnit to count</param>
		/// <returns>An int.</returns>
		private static double NumberOf_Until(this TimeOnly startTime, TimeOnly endTime, TimeUnit u)
		{
			switch (u)
			{
				case TimeUnit.Decades:
				case TimeUnit.Years:
				case TimeUnit.Semesters:
				case TimeUnit.Trimesters:
				case TimeUnit.Months:
				case TimeUnit.Weeks:
				case TimeUnit.Weekends:
				case TimeUnit.Workdays:
				case TimeUnit.Days:
					return 0;

				case TimeUnit.Hours:
					return (endTime - startTime).TotalHours;

				case TimeUnit.Minutes:
					return (endTime - startTime).TotalMinutes;

				case TimeUnit.Seconds:
					return (endTime - startTime).TotalSeconds;

				case TimeUnit.Milliseconds:
					return (endTime - startTime).TotalMilliseconds;

				case TimeUnit.Holidays:
					return 0;
			}

			return 0;
		}
	}
}