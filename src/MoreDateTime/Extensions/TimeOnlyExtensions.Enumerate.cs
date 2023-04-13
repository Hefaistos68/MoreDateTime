using MoreDateTime.Extensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class TimeOnlyExtensions
	{
		/// <summary>
		/// Enumerates starting with the startTime date, until the endTime date in steps of distance<br/>
		/// When the distance is negative, the start date must be greater than the end date, and the enumeration goes backwards
		/// </summary>
		/// <param name="startTime">The starting TimeOnly object</param>
		/// <param name="endTime">The ending TimeOnly object</param>
		/// <param name="distance">The distance expressed as TimeSpan</param>
		/// <returns>An IEnumerable of type TimeOnly</returns>
		public static IEnumerable<TimeOnly> EnumerateInStepsUntil(this TimeOnly startTime, TimeOnly endTime, TimeSpan distance)
		{
			if (Math.Abs(distance.Ticks) > Math.Abs((endTime - startTime).Ticks))
			{
				throw new ArgumentException($"{nameof(distance)} is greater than the difference between the two times");
			}

			if (distance.Ticks == 0)
			{
				throw new ArgumentException($"{nameof(distance)} must not be zero");
			}

			// unlike the DateTime and DateOnly, the TimeOnly is roundrobin, so there is no real start or end
			// The direction is determined by the distance, negative means backwards (24h to 0), positive means forwards (0 to 24h)
			for (var moment = startTime.Ticks; distance.IsNegative() ? moment >= endTime.Ticks : moment <= endTime.Ticks; moment += distance.Ticks)
				yield return new TimeOnly(moment);
		}

		/// <summary>
		/// Enumerates starting with startTime until endTime in steps of distance
		/// </summary>
		/// <param name="startTime">The starting TimeOnly object</param>
		/// <param name="endTime">The ending TimeOnly object</param>
		/// <param name="distance">The distance expressed as TimeSpan</param>
		/// <param name="evaluator">An evaluation function called for each moment before returning it. If the evaluator returns false, the value is skipped</param>
		/// <returns>An IEnumerable of type TimeOnly</returns>
		public static IEnumerable<TimeOnly> EnumerateInStepsUntil(this TimeOnly startTime, TimeOnly endTime, TimeSpan distance, Func<TimeOnly, bool> evaluator)
		{
			if (evaluator is null)
			{
				throw new ArgumentNullException(nameof(evaluator));
			}

			if (Math.Abs(distance.Ticks) > Math.Abs((endTime - startTime).Ticks))
			{
				throw new ArgumentException($"{nameof(distance)} is greater than the difference between the two times");
			}

			if (distance.Ticks == 0)
			{
				throw new ArgumentException($"{nameof(distance)} must not be zero");
			}

			for (var moment = startTime.Ticks; distance.IsNegative() ? moment >= endTime.Ticks : moment <= endTime.Ticks; moment += distance.Ticks)
			{
				var m = new TimeOnly(moment);
				if (evaluator.Invoke(m))
					yield return m;
			}
		}
	}
}