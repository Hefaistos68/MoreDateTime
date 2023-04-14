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

		public string ToString();
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
