using System;

using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="IRange{T, Treturn}"/> interface and provides a time range through its <see cref="Start"/> and <see cref="End"/> members
	/// </summary>
	public class DateTimeRange : IRange<DateTime, DateTimeRange>
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

		/// <summary>
		/// Initializes a copied new instance of the <see cref="DateTimeRange"/> class.
		/// </summary>
		/// <param name="range">The range to copy</param>
		public DateTimeRange(DateTimeRange range)
		{
			Start = range.Start;
			End = range.End;
		}

		/// <summary>
		/// Gets the distance between the <see cref="Start"/> and the <see cref="End"/>
		/// </summary>
		public TimeSpan Distance()
		{
			return this.Start.Distance(this.End);
		}

		/// <summary>
		/// Offsets the <see cref="Start"/> and <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		public DateTimeRange Offset(TimeSpan timeSpan)
		{
			return new DateTimeRange(this.Start + timeSpan, this.End + timeSpan);
		}

		/// <summary>
		/// Extends the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public DateTimeRange Extend(TimeSpan timeSpan, RangeDirection direction)
		{
			return direction switch
			{

				RangeDirection.Both  => new DateTimeRange(this.Start.Sub(timeSpan / 2), this.End.Add( timeSpan / 2)),
				RangeDirection.Start => new DateTimeRange(this.Start.Sub(timeSpan), this.End),
				RangeDirection.End   => new DateTimeRange(this.Start, this.End.Add(timeSpan)),
				_                    => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		/// <summary>
		/// Reduces the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public DateTimeRange Reduce(TimeSpan timeSpan, RangeDirection direction)
		{
			var distance = this.Start.Distance(this.End);
			if (distance <= timeSpan)
			{
				throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan, "The timeSpan is too large to reduce the range");
			}

			return direction switch
			{
				RangeDirection.Both  => new DateTimeRange(this.Start.Add(timeSpan / 2), this.End.Sub(timeSpan / 2)),
				RangeDirection.Start => new DateTimeRange(this.Start.Add(timeSpan), this.End),
				RangeDirection.End   => new DateTimeRange(this.Start, this.End.Sub(timeSpan)),
				_                    => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		/// <inheritdoc/>
		public DateTime Start { get; set; }

		/// <inheritdoc/>
		public DateTime End { get; set; }
	}
}
