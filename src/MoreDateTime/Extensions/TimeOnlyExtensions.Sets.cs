using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

using MoreDateTime.Interfaces;

namespace MoreDateTime.Extensions
{
	/// <summary>
	/// TimeOnly related extension methods
	/// </summary>
	public static partial class TimeOnlyExtensions
	{
		/// <summary>
		/// Calculates the union of two TimeOnlyRanges. If the two ranges do not overlap, the result is 
		/// an empty TimeOnlyRange. Otherwise, the result is the TimeOnlyRange that contains both ranges.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A TimeOnlyRange which is either empty, when there is no overlap, or contains the range spanning both ranges</returns>
		public static TimeOnlyRange Union(this TimeOnlyRange a, TimeOnlyRange b)
		{
			if(a is null || b is null)
			{
				throw new ArgumentNullException();
			}

			TimeOnlyRange ab = new();

			if(a.DoesOverlap(b))
			{
				// calculate the union of the two ranges
				ab.Start = a.Start < b.Start ? a.Start : b.Start;
				ab.End = a.End > b.End ? a.End : b.End;
			}

			return ab;
		}

		/// <summary>
		/// Calculates the intersection of two TimeOnlyRanges. If the two ranges do not overlap, the result is 
		/// an empty TimeOnlyRange. Otherwise, the result is the TimeOnlyRange that where both ranges overlap.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A TimeOnlyRange which is either empty, when there is no overlap, or contains the range where both ranges overlap</returns>
		public static TimeOnlyRange Intersection(this TimeOnlyRange a, TimeOnlyRange b)
		{
			if (a is null || b is null)
			{
				throw new ArgumentNullException();
			}

			TimeOnlyRange ab = new();

			if(a.DoesOverlap(b))
			{
				// calculate the intersection of the two ranges
				ab.Start = a.Start > b.Start ? a.Start : b.Start;
				ab.End = a.End < b.End ? a.End : b.End;
				
				if(!ab.IsOrdered())
				{
					ab = ab.Order();
				}
			}

			return ab;
		}

		/// <summary>
		/// Calculates the difference of two TimeOnlyRanges. If the two ranges do not overlap, the result is 
		/// the first TimeOnlyRange. Otherwise, the result is the first TimeOnlyRange without where both ranges overlap.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A list of TimeOnlyRange</returns>
		public static List<TimeOnlyRange> Difference(this TimeOnlyRange a, TimeOnlyRange b)
		{
			if (a is null || b is null)
			{
				throw new ArgumentNullException();
			}

			// cases:
			// a is within b, => empty, all dates of a are contained in b
			// b is within a, => a.start to b.start and b.end to a.end, creates two separate ranges with the overlap as a hole
			// a overlaps with b on a.start => b.end to a.end, the overlap with b is cut out from the start of a
			// a overlaps b on b.end => a.start to b.start, the overlap with b is cut out from the end of a

			if (a.IsWithin(b) || !a.DoesOverlap(b))
			{
				return new List<TimeOnlyRange>() {};
			}

			if(b.IsWithin(a))
			{
				return new List<TimeOnlyRange>() { new TimeOnlyRange(a.Start, b.Start), new TimeOnlyRange(b.End, a.End) };
			}

			if(a.Start.IsWithin(b))
			{
				return new List<TimeOnlyRange>() { new TimeOnlyRange(b.End, a.End) };
			}

			if(a.End.IsWithin(b))
			{
				return new List<TimeOnlyRange>() { new TimeOnlyRange(a.Start, b.Start) };
			}
			
			throw new InvalidOperationException();
		}

		/// <summary>
		/// Verifies if TimeOnlyRange a overlapps with TimeOnlyRange b.
		/// </summary>
		/// <param name="a">The TimeOnlyRange to verify a possible overlap</param>
		/// <param name="b">The TimeOnlyrange to verify with</param>
		/// <returns>A bool.</returns>
		public static bool DoesOverlap(this TimeOnlyRange a, TimeOnlyRange b)
		{
			return a.Start.IsWithin(b) || a.End.IsWithin(b) 
				|| b.Start.IsWithin(a) || b.End.IsWithin(a);
		}

	}
}
