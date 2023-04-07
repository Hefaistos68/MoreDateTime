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

			if (startTime < endTime)
			{
				for (var step = startTime; step <= endTime; step = step.Add(distance))
					yield return step;
			}
			else
			{
				for (var step = startTime; step >= endTime; step = step.Add(distance))
					yield return step;
			}
		}

		/// <summary>
		/// Enumerates starting with startTime until endTime in steps of distance
		/// </summary>
		/// <param name="startTime">The starting TimeOnly object</param>
		/// <param name="endTime">The ending TimeOnly object</param>
		/// <param name="distance">The distance expressed as TimeSpan</param>
		/// <param name="evaluator">An evaluation function called for each step before returning it. If the evaluator returns false, the value is skipped</param>
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

			if (startTime < endTime)
			{
				for (var step = startTime; step <= endTime; step = step.Add(distance))
				{
					if (evaluator.Invoke(step))
						yield return step;
				}
			}
			else
			{
				for (var step = startTime; step >= endTime; step = step.Add(distance))
				{
					if (evaluator.Invoke(step))
						yield return step;
				}
			}

		}

		/// <summary>
		/// Enumerates all days startTime current TimeOnly value endTime the end TimeOnly, including the end date
		/// </summary>
		/// <param name="from">The starting TimeOnly value</param>
		/// <param name="to">The ending TimeOnly value</param>
		/// <returns>A enumerable of TimeOnly values with days increasing by 1</returns>
	}
}