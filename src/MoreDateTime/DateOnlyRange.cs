using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="IDateOnlyRange"/> interface and provides a time range through its <see cref="Start"/> and <see cref="End"/> members
	/// </summary>
	public class DateOnlyRange : IDateOnlyRange
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateOnlyRange"/> class.
		/// </summary>
		/// <param name="startTime">The start DateOnly</param>
		/// <param name="endTime">The end DateOnly</param>
		public DateOnlyRange(DateOnly startTime, DateOnly endTime)
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
		public DateOnly Start { get; set; }

		/// <inheritdoc/>
		public DateOnly End { get; set; }
	}

}
