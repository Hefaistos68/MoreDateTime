using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

using MoreDateTime.Interfaces;

using Nager.Date;

namespace MoreDateTime.Extensions
{
	/// <summary>
	/// DateOnly related extension methods
	/// </summary>
	public static partial class DateOnlyExtensions
	{
		/// <summary>
		/// Calculates the union of two DateOnlyRanges. If the two ranges do not overlap, the result is 
		/// an empty DateOnlyRange. Otherwise, the result is the DateOnlyRange that contains both ranges.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A DateOnlyRange which is either empty, when there is no overlap, or contains the range spanning both ranges</returns>
		public static DateOnlyRange Union(this DateOnlyRange a, DateOnlyRange b)
		{
			if(a is null || b is null)
			{
				throw new ArgumentNullException();
			}

			DateOnlyRange ab = new();

			if(a.DoesOverlap(b))
			{
				// calculate the union of the two ranges
				ab.Start = a.Start < b.Start ? a.Start : b.Start;
				ab.End = a.End > b.End ? a.End : b.End;
			}

			return ab;
		}

		/// <summary>
		/// Calculates the intersection of two DateOnlyRanges. If the two ranges do not overlap, the result is 
		/// an empty DateOnlyRange. Otherwise, the result is the DateOnlyRange that where both ranges overlap.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A DateOnlyRange which is either empty, when there is no overlap, or contains the range where both ranges overlap</returns>
		public static DateOnlyRange Intersection(this DateOnlyRange a, DateOnlyRange b)
		{
			if (a is null || b is null)
			{
				throw new ArgumentNullException();
			}

			DateOnlyRange ab = new();

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
		/// Calculates the difference of two DateOnlyRanges. If the two ranges do not overlap, the result is 
		/// the first DateOnlyRange. Otherwise, the result is the first DateOnlyRange without where both ranges overlap.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A list of DateOnlyRange</returns>
		public static List<DateOnlyRange> Difference(this DateOnlyRange a, DateOnlyRange b)
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
				return new List<DateOnlyRange>() {};
			}

			if(b.IsWithin(a))
			{
				return new List<DateOnlyRange>() { new DateOnlyRange(a.Start, b.Start), new DateOnlyRange(b.End, a.End) };
			}

			if(a.Start.IsWithin(b))
			{
				return new List<DateOnlyRange>() { new DateOnlyRange(b.End, a.End) };
			}

			if(a.End.IsWithin(b))
			{
				return new List<DateOnlyRange>() { new DateOnlyRange(a.Start, b.Start) };
			}
			
			throw new InvalidOperationException();
		}

		/// <summary>
		/// Verifies if DateOnlyRange a overlapps with DateOnlyRange b.
		/// </summary>
		/// <param name="a">The DateOnlyRange to verify a possible overlap</param>
		/// <param name="b">The DateOnlyrange to verify with</param>
		/// <returns>A bool.</returns>
		public static bool DoesOverlap(this DateOnlyRange a, DateOnlyRange b)
		{
			return a.Start.IsWithin(b) || a.End.IsWithin(b) 
				|| b.Start.IsWithin(a) || b.End.IsWithin(a);
		}

	}
}
