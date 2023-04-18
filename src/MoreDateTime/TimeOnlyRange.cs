using System.Diagnostics;

using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="IRange{T, Treturn}"/> interface and provides a time range through its <see cref="Start"/> and <see cref="End"/> members
	/// </summary>
	[DebuggerDisplay("{Start} - {End}")]
	public class TimeOnlyRange : IRange<TimeOnly, TimeOnlyRange>
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

		/// <summary>
		/// Initializes a copied new instance of the <see cref="TimeOnlyRange"/> class.
		/// </summary>
		/// <param name="range">The range to copy</param>
		public TimeOnlyRange(TimeOnlyRange range)
		{
			Start = range.Start;
			End = range.End;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeOnlyRange"/> class.
		/// </summary>
		public TimeOnlyRange()
		{
			Start = new TimeOnly();
			End = new TimeOnly();
		}

		/// <inheritdoc/>
		public TimeOnly End { get; set; }

		/// <summary>
		/// Verifies if the range is empty, Start == End == default(T)
		/// </summary>
		/// <returns>True, if both start and end are default(T)</returns>
		public bool IsEmpty
		{
			get { return (this.Start == default) && (this.End == default); }
		}

		/// <inheritdoc/>
		public TimeOnly Start { get; set; }

		/// <summary>
		/// Verifies if the given value is contained in this range, including start and end
		/// </summary>
		/// <param name="value">The value</param>
		/// <returns>True, if value is within this range</returns>
		public bool Contains(TimeOnly value)
		{
			// if this range spans midnight, we check if the value is between the start and midnight, or between midnight and the end
			// otherwise we check if the value is between the start and end
			if (this.IsOrdered())
				return (this.Start <= value) && (value <= this.End);
			else
				return (this.End <= value) && (value <= this.Start);
		}

		/// <summary>
		/// Gets the distance between the <see cref="Start"/> and the <see cref="End"/>
		/// </summary>
		public TimeSpan Distance()
		{
			return this.Start.Distance(this.End);
		}

		/// <summary>
		/// Get an empty range
		/// </summary>
		public TimeOnlyRange Empty() => new TimeOnlyRange();
		/// <summary>
		/// Extends the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public TimeOnlyRange Extend(TimeSpan timeSpan, RangeDirection direction)
		{
			return direction switch
			{
				RangeDirection.Both => new TimeOnlyRange(this.Start.Sub(timeSpan / 2), this.End.Add(timeSpan / 2)),
				RangeDirection.Start => new TimeOnlyRange(this.Start.Sub(timeSpan), this.End),
				RangeDirection.End => new TimeOnlyRange(this.Start, this.End.Add(timeSpan)),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		/// <summary>
		/// Verifies if start is less or equal end
		/// </summary>
		/// <returns>True if start is less or equal end</returns>
		public bool IsOrdered()
		{
			// for TimeRanges, the start can be after end, like in 22:00 - 04:00 as opposed to 04:00 - 22:00 (which would be ordered)
			return this.Start <= this.End;
		}

		/// <summary>
		/// Verifies if the this range is contained entirely in the given range, including start and end
		/// </summary>
		/// <param name="value">The value to compare with</param>
		/// <returns>True if this object is entirely within the target range</returns>
		public bool IsWithin(TimeOnlyRange value)
		{
			return value.Contains(this.Start) && value.Contains(this.End);
		}

		/// <summary>
		/// Offsets the <see cref="Start"/> and <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		public TimeOnlyRange Offset(TimeSpan timeSpan)
		{
			return new TimeOnlyRange(this.Start.Add(timeSpan), this.End.Add(timeSpan));
		}

		/// <summary>
		/// Orders the range, so that Start is guaranteed to be less or equal End.
		/// <note>Does not make much sense for TimeOnlyRange since the time can be spanning 00:00, as in 22:00 - 04:00</note>
		/// </summary>
		/// <returns>Itself ordered</returns>
		public TimeOnlyRange Order()
		{
			return this.Start <= this.End ? this : new TimeOnlyRange(this.End, this.Start);
		}

		/// <summary>
		/// Reduces the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public TimeOnlyRange Reduce(TimeSpan timeSpan, RangeDirection direction)
		{
			var distance = this.Start.Distance(this.End);
			if (distance <= timeSpan)
			{
				throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan, "The timeSpan is too large to reduce the range");
			}

			return direction switch
			{
				RangeDirection.Both => new TimeOnlyRange(this.Start.Add(timeSpan / 2), this.End.Sub(timeSpan / 2)),
				RangeDirection.Start => new TimeOnlyRange(this.Start.Add(timeSpan), this.End),
				RangeDirection.End => new TimeOnlyRange(this.Start, this.End.Sub(timeSpan)),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}
	}
}