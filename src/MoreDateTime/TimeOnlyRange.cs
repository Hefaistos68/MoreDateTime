using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// Implements the <see cref="IRange{T, Treturn}"/> interface and provides a time range through its <see cref="Start"/> and <see cref="End"/> members
	/// </summary>
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
		public TimeOnlyRange Offset(TimeSpan timeSpan)
		{
			return new TimeOnlyRange(this.Start.Add(timeSpan), this.End.Add(timeSpan));
		}

		/// <summary>
		/// Extends the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public TimeOnlyRange Extend(TimeSpan timeSpan, RangeDirection direction)
		{
			return direction switch
			{
				RangeDirection.Both  => new TimeOnlyRange(this.Start.Sub(timeSpan / 2), this.End.Add(timeSpan / 2)),
				RangeDirection.Start => new TimeOnlyRange(this.Start.Sub(timeSpan), this.End),
				RangeDirection.End   => new TimeOnlyRange(this.Start, this.End.Add(timeSpan)),
				_                    => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
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
				RangeDirection.Both  => new TimeOnlyRange(this.Start.Add(timeSpan / 2), this.End.Sub(timeSpan / 2)),
				RangeDirection.Start => new TimeOnlyRange(this.Start.Add(timeSpan), this.End),
				RangeDirection.End   => new TimeOnlyRange(this.Start, this.End.Sub(timeSpan)),
				_                    => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		/// <inheritdoc/>
		public TimeOnly Start { get; set; }

		/// <inheritdoc/>
		public TimeOnly End { get; set; }
	}
}
