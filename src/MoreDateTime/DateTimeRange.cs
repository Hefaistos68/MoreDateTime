using System;

using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="IDateTimeRange"/> interface and provides a time range through its <see cref="Start"/> and <see cref="End"/> members
	/// </summary>
	public class DateTimeRange : IDateTimeRange
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeRange"/> class.
		/// </summary>
		/// <param name="startTime">The start datetime</param>
		/// <param name="endTime">The end datetime</param>
		public DateTimeRange(DateTime startTime, DateTime endTime)
		{
			Start = startTime;
			End = endTime;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeRange"/> class.
		/// </summary>
		/// <param name="startTime">The start datetime</param>
		/// <param name="endTime">The end datetime</param>
		public DateTimeRange(DateOnly startTime, DateOnly endTime)
		{
			Start = startTime.ToDateTime();
			End = endTime.ToDateTime();	  // Should we include this day until midnight - 1 millisecond?
		}

		/// <inheritdoc/>
		public TimeSpan Distance()
		{
			return this.Start.Distance(this.End);
		}

		/// <inheritdoc/>
		public DateTime Start { get; set; }

		/// <inheritdoc/>
		public DateTime End { get; set; }
	}
}
