using System.Diagnostics;

using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="IRange{T, Treturn}"/> interface and provides a time range through its <see cref="Start"/> and <see cref="End"/> members
	/// </summary>
	[DebuggerDisplay("{Start} - {End}")]
	public class DateOnlyRange : IRange<DateOnly, DateOnlyRange>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="DateOnlyRange"/> class.
		/// </summary>
		/// <param name="startTime">The start time.</param>
		/// <param name="endTime">The end time.</param>
		public DateOnlyRange(DateTime startTime, DateTime endTime)
		{
			Start = startTime.ToDateOnly();
			End = endTime.ToDateOnly();
		}

		/// <summary>
		/// Initializes a copied new instance of the <see cref="DateOnlyRange"/> class.
		/// </summary>
		/// <param name="range">The range to copy</param>
		public DateOnlyRange(DateOnlyRange range)
		{
			Start = range.Start;
			End = range.End;
		}

		/// <summary>
		/// Initializes a copied new instance of the <see cref="DateTimeRange"/> class.
		/// </summary>
		/// <param name="range">The range to copy</param>
		public DateOnlyRange(DateTimeRange range)
		{
			Start = range.Start.ToDateOnly();
			End = range.End.ToDateOnly();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateOnlyRange"/> class.
		/// </summary>
		public DateOnlyRange()
		{
			Start = new DateOnly();
			End = new DateOnly();
		}

		/// <inheritdoc/>
		public DateOnly End { get; set; }

		/// <summary>
		/// Verifies if the range is empty, Start == End == default(T)
		/// </summary>
		/// <returns>True, if both start and end are default(T)</returns>
		public bool IsEmpty
		{
			get { return (this.Start == default) && (this.End == default); }
		}

		/// <inheritdoc/>
		public DateOnly Start { get; set; }

		/// <summary>
		/// Verifies if the given value is contained in this range, including start and end
		/// </summary>
		/// <param name="value">The value</param>
		/// <returns>True, if value is within this range</returns>
		public bool Contains(DateOnly value)
		{
			return (this.Start <= value) && (value <= this.End);
		}

		/// <summary>
		/// Verifies if the given value is contained in this range, including start and end
		/// </summary>
		/// <param name="value">The value</param>
		/// <returns>True, if value is within this range</returns>
		public bool Contains(DateTime value)
		{
			return (this.Start <= value.ToDateOnly()) && (value.ToDateOnly() <= this.End);
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
		public DateOnlyRange Empty() => new DateOnlyRange();

		/// <summary>
		/// Extends the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public DateOnlyRange Extend(TimeSpan timeSpan, RangeDirection direction)
		{
			// DateOnly operates only on timeSpans greater than 1 day
			if (timeSpan.Days < 1)
			{
				return new(this);
			}

			return direction switch
			{
				RangeDirection.Both => new DateOnlyRange(this.Start.Sub(timeSpan / 2), this.End.Add(timeSpan / 2)),
				RangeDirection.Start => new DateOnlyRange(this.Start.Sub(timeSpan), this.End),
				RangeDirection.End => new DateOnlyRange(this.Start, this.End.Add(timeSpan)),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		/// <summary>
		/// Verifies that the order of the dates is correct, Start &lt; End
		/// </summary>
		/// <returns>True if Start is less or equal End</returns>
		public bool IsOrdered()
		{
			return this.Start <= this.End;
		}

		/// <summary>
		/// Verifies if the this range is contained entirely in the given range, including start and end
		/// </summary>
		/// <param name="value">The value to compare with</param>
		/// <returns>True if this object is entirely within the target range</returns>
		public bool IsWithin(DateOnlyRange value)
		{
			return value.Contains(this.Start) && value.Contains(this.End);
		}

		/// <summary>
		/// Offsets the <see cref="Start"/> and <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		public DateOnlyRange Offset(TimeSpan timeSpan)
		{
			return new DateOnlyRange(this.Start.Add(timeSpan), this.End.Add(timeSpan));
		}

		/// <summary>
		/// Orders the range, so that Start is guaranteed to be less or equal End
		/// </summary>
		/// <returns>Itself ordered</returns>
		public DateOnlyRange Order()
		{
			return this.Start <= this.End ? this : new DateOnlyRange(this.End, this.Start);
		}

		/// <summary>
		/// Reduces the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public DateOnlyRange Reduce(TimeSpan timeSpan, RangeDirection direction)
		{
			// DateOnly operates only on timeSpans greater than 1 day
			if (timeSpan.Days < 1)
			{
				return new(this);
			}

			var distance = this.Start.Distance(this.End);
			if (distance <= timeSpan)
			{
				throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan, "The timeSpan is too large to reduce the range");
			}

			return direction switch
			{
				RangeDirection.Both => new DateOnlyRange(this.Start.Add(timeSpan / 2), this.End.Sub(timeSpan / 2)),
				RangeDirection.Start => new DateOnlyRange(this.Start.Add(timeSpan), this.End),
				RangeDirection.End => new DateOnlyRange(this.Start, this.End.Sub(timeSpan)),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}
	}
}