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
		/// Adds the given number of ticks to the given TimeOnly object
		/// </summary>
		/// <param name="dt">The TimeOnly object</param>
		/// <param name="ticks">The number of ticks to add</param>
		/// <returns>An <see cref="TimeOnly"/> whose value is the sum of the time represented by this instance and the time interval represented by value</returns>
		public static TimeOnly AddTicks(this TimeOnly dt, long ticks)
		{
			return dt.Add(new TimeSpan(ticks));
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
		/// Returns a new TimeOnly that subtracts the value of the specified TimeSpan from the value of this instance
		/// </summary>
		/// <param name="me">The TimeOnly object to subtract the value from</param>
		/// <param name="timeSpan">A positive time interval</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance minus the time interval represented by value</returns>
		public static TimeOnly Sub(this TimeOnly me, TimeSpan timeSpan)
		{
			return me.Add(-timeSpan);
		}

		/// <summary>
		/// Returns the distance as a TimeSpan between two TimeOnly objects. The result is always positive.
		/// </summary>
		/// <param name="startTime">The start time object</param>
		/// <param name="endTime">The end time object</param>
		/// <returns>A TimeSpan which expresses the difference between the two times</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan Distance(this TimeOnly startTime, TimeOnly endTime)
		{
			return (startTime > endTime) ? (startTime - endTime) : (endTime - startTime);
		}

		/// <summary>
		/// Splits the given range of TimeOnly into the given number of parts.
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="endDate">The end date</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of TimeOnlyRanges</returns>
		public static List<TimeOnlyRange> Split(this TimeOnly startDate, TimeOnly endDate, int parts)
		{
			if (parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			var result = new List<TimeOnlyRange>();
			var distance = startDate.Distance(endDate);
			var partDistance = distance.Ticks / parts;
			var part = startDate;

			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new TimeOnlyRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}

		/// <summary>
		/// Splits the given range of TimeOnly into the given number of parts.
		/// </summary>
		/// <param name="startDate">The start date</param>
		/// <param name="distance">The timespan to split</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of TimeOnlyRanges</returns>
		public static List<TimeOnlyRange> Split(this TimeOnly startDate, TimeSpan distance, int parts)
		{
			if (parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			if (distance.Ticks < parts)
			{
				throw new ArgumentOutOfRangeException(nameof(distance), "The ticks in distance must be greater than the number of parts");
			}

			var result = new List<TimeOnlyRange>();
			var partDistance = distance.Ticks / parts;
			var part = startDate;

			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new TimeOnlyRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}

		/// <summary>
		/// Splits the given range of TimeOnly into the given number of parts.
		/// </summary>
		/// <param name="times">The start and end date</param>
		/// <param name="parts">The number of parts to split into</param>
		/// <returns>A list of TimeOnlyRanges</returns>
		public static List<TimeOnlyRange> Split(this TimeOnlyRange times, int parts)
		{
			if (times is null)
			{
				throw new ArgumentNullException(nameof(times));
			}

			if (parts < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "The number of parts must be greater than 0");
			}

			var result = new List<TimeOnlyRange>();
			var distance = times.Distance();
			var partDistance = distance.Ticks / parts;
			var part = times.Start;

			for (int i = 0; i < parts; i++)
			{
				var nextPart = part.AddTicks(partDistance);
				result.Add(new TimeOnlyRange(part, nextPart));
				part = nextPart;
			}
			return result;
		}

	}
}
