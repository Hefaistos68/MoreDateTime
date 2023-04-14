using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="IRange{T, Treturn}"/> interface and provides a time range through its <see cref="Start"/> and <see cref="End"/> members
	/// </summary>
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
		public DateOnlyRange Offset(TimeSpan timeSpan)
		{
			return new DateOnlyRange(this.Start.Add(timeSpan), this.End.Add(timeSpan));
		}

		/// <summary>
		/// Extends the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public DateOnlyRange Extend(TimeSpan timeSpan, RangeDirection direction)
		{
			// DateOnly operates only on timeSpans greater than 1 day
			if(timeSpan.Days < 1)
			{ 
				return new(this); 
			}

			return direction switch
			{
				RangeDirection.Both  => new DateOnlyRange(this.Start.Sub(timeSpan / 2), this.End.Add(timeSpan / 2)),
				RangeDirection.Start => new DateOnlyRange(this.Start.Sub(timeSpan), this.End),
				RangeDirection.End   => new DateOnlyRange(this.Start, this.End.Add(timeSpan)),
				_                    => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
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
				RangeDirection.Both  => new DateOnlyRange(this.Start.Add(timeSpan / 2), this.End.Sub(timeSpan / 2)),
				RangeDirection.Start => new DateOnlyRange(this.Start.Add(timeSpan), this.End),
				RangeDirection.End   => new DateOnlyRange(this.Start, this.End.Sub(timeSpan)),
				_                    => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		/// <inheritdoc/>
		public DateOnly Start { get; set; }

		/// <inheritdoc/>
		public DateOnly End { get; set; }
	}

}
