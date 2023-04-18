using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDateTime.Interfaces
{
	/// <summary>
	/// An interface for ranges of dates and times
	/// </summary>
	/// <typeparam name="T">The base type to handle</typeparam>
	/// <typeparam name="Treturn">A self reference as a return type</typeparam>
	internal interface IRange<T, Treturn>  where T : IComparable<T>
	{
		/// <summary>
		/// Gets or sets the start <typeparamref name="T"/>
		/// </summary>
		T Start { get; set; }

		/// <summary>
		/// Gets or sets the end <typeparamref name="T"/>
		/// </summary>
		T End { get; set; }

		/// <summary>
		/// Gets the distance between the <see cref="Start"/> and the <see cref="End"/>
		/// </summary>
		public TimeSpan Distance();

		/// <summary>
		/// Offsets the <see cref="Start"/> and <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		public Treturn Offset(TimeSpan timeSpan);

		/// <summary>
		/// Extends the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public Treturn Extend(TimeSpan timeSpan, RangeDirection direction);

		/// <summary>
		/// Reduces the <see cref="Start"/> and/or <see cref="End"/> by the specified <paramref name="timeSpan"/>
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <param name="direction">The direction in which to extend</param>
		public Treturn Reduce(TimeSpan timeSpan, RangeDirection direction);

		/// <summary>
		/// Verifies that the order of the dates is correct, Start &lt; End
		/// </summary>
		/// <returns>True if Start is less or equal End</returns>
		public bool IsOrdered();

		/// <summary>
		/// Orders the range, so that Start is guaranteed to be less or equal End
		/// </summary>
		/// <returns>Itself ordered</returns>
		public Treturn Order();

		/// <summary>
		/// Verifies if the given value is contained in this range, including start and end
		/// </summary>
		/// <param name="value">The value</param>
		/// <returns>True, if value is within this range</returns>
		public bool Contains(T value);

		/// <summary>
		/// Verifies if the this range is contained entirely in the given range, including start and end
		/// </summary>
		/// <param name="value">The value to compare with</param>
		/// <returns>True if this object is entirely within the target range</returns>
		public bool IsWithin(Treturn value);

		/// <summary>
		/// Verifies if the range is empty, Start == End == default(T)
		/// </summary>
		/// <returns>True, if both start and end are default(T)</returns>
		public bool IsEmpty { get; }

		/// <summary>
		/// Get an empty range
		/// </summary>
		public Treturn Empty();
	}

	/// <summary>
	/// The range direction enum, indicates in which direction to move the ranges values
	/// </summary>
	public enum RangeDirection
	{
		/// <summary>
		/// Move start and end
		/// </summary>
		Both,
		
		/// <summary>
		/// Move start only
		/// </summary>
		Start,

		/// <summary>
		/// Move end only
		/// </summary>
		End
	}
}
