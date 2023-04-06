using MoreDateTime.Extensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class TimeOnlyExtensions
	{
		/// <summary>
		/// Verifies if the two instances are the same hour
		/// </summary>
		/// <param name="dt">The first TimeOnly argument</param>
		/// <param name="other">The TimeOnly argument to compare with</param>
		/// <returns>True if the dates are on the same hour</returns>
		public static bool IsSameHour(this TimeOnly dt, TimeOnly other)
		{
			return dt.Hour == other.Hour;
		}

		/// <summary>
		/// Verifies if the two dates are the same minute
		/// </summary>
		/// <param name="dt">The first TimeOnly argument</param>
		/// <param name="other">The TimeOnly argument to compare with</param>
		/// <returns>True if the dates are on the same minute</returns>
		public static bool IsSameMinute(this TimeOnly dt, TimeOnly other)
		{
			return dt.Minute == other.Minute;
		}

		/// <summary>
		/// Verifies if the two dates are the same millisecond
		/// </summary>
		/// <param name="dt">The first TimeOnly argument</param>
		/// <param name="other">The TimeOnly argument to compare with</param>
		/// <returns>True if the dates are on the same month</returns>
		public static bool IsSameMillisecond(this TimeOnly dt, TimeOnly other)
		{
			return dt.Millisecond == other.Millisecond;
		}

		/// <summary>
		/// Verifies if the two instances are the same second
		/// </summary>
		/// <param name="dt">The first TimeOnly argument</param>
		/// <param name="other">The TimeOnly argument to compare with</param>
		/// <returns>True if the dates are on the same second</returns>
		public static bool IsSameSecond(this TimeOnly dt, TimeOnly other)
		{
			return dt.Second == other.Second;
		}

		/// <summary>
		/// Adds the given number of milliseconds to the given TimeOnly object
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>An <see cref="TimeOnly"/> whose value is the sum of the time represented by this instance and the time interval represented by value</returns>
	}
}