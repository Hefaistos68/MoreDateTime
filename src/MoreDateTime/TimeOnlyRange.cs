using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="ITimeOnlyRange"/> interface and provides a time range through its <see cref="Start"/> and <see cref="End"/> members
	/// </summary>
	public class TimeOnlyRange : ITimeOnlyRange
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOnlyRange"/> class.
		/// </summary>
		/// <param name="startTime">The start TimeOnly</param>
		/// <param name="endTime">The end TimeOnly</param>
		public TimeOnlyRange(TimeOnly startTime, TimeOnly endTime)
		{
			Start = startTime;
			End = endTime;
		}

		/// <inheritdoc/>
		public TimeSpan Distance()
		{
			return this.Start.Distance(this.End);
		}

		/// <inheritdoc/>
		public TimeOnly Start { get; set; }

		/// <inheritdoc/>
		public TimeOnly End { get; set; }
	}
}
