using System;

using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="IDateTimeRange"/> interface and provides a time range through its <see cref="StartTime"/> and <see cref="EndTime"/> members
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
			StartTime = startTime;
			EndTime = endTime;
		}

		/// <inheritdoc/>
		public DateTime StartTime { get; set; }

		/// <inheritdoc/>
		public DateTime EndTime { get; set; }
	}

}
