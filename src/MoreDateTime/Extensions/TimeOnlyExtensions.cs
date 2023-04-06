using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Nager.Date;

namespace MoreDateTime.Extensions
{
	/// <summary>
	/// The extension methods for working with <see cref="TimeOnly"/> objects.
	/// </summary>
	public static partial class TimeOnlyExtensions
	{
				

		/// <summary>
		/// Adds the given number of milliseconds to the given TimeOnly object
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>An <see cref="TimeOnly"/> whose value is the sum of the time represented by this instance and the time interval represented by value</returns>
		public static TimeOnly AddMilliseconds(this TimeOnly dt, int milliseconds)
		{
			return dt.Add(new TimeSpan(milliseconds * TimeSpan.TicksPerMillisecond));
		}

		/// <summary>
		/// Adds the given number of seconds to the given TimeOnly object
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <returns>An <see cref="TimeOnly"/> whose value is the sum of the time represented by this instance and the time interval represented by value</returns>
		public static TimeOnly AddSeconds(this TimeOnly dt, int seconds)
		{
			return dt.Add(new TimeSpan(seconds * TimeSpan.TicksPerSecond));
		}

		/// <summary>
		/// Returns the distance as a TimeSpan between two TimeOnly objects.
		/// </summary>
		/// <param name="me">The start time object</param>
		/// <param name="other">The end time object</param>
		/// <returns>A TimeSpan which expresses the difference between the two times</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan Distance(this TimeOnly me, TimeOnly other)
		{
			return me - other;
		}
	}
}
