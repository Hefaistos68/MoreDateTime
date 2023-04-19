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
	/// DateTime related extension methods
	/// </summary>
	public static partial class DateTimeExtensions
	{
		/// <summary>
		/// Calculates the union of two DateTimeRanges. If the two ranges do not overlap, the result is 
		/// an empty DateTimeRange. Otherwise, the result is the DateTimeRange that contains both ranges.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A DateTimeRange which is either empty, when there is no overlap, or contains the range spanning both ranges</returns>
		public static DateTimeRange Union(this DateTimeRange a, DateTimeRange b)
		{
			if(a is null || b is null)
			{
				throw new ArgumentNullException();
			}

			DateTimeRange ab = new();

			if(a.DoesOverlap(b))
			{
				// calculate the union of the two ranges
				ab.Start = a.Start < b.Start ? a.Start : b.Start;
				ab.End = a.End > b.End ? a.End : b.End;
			}

			return ab;
		}

		/// <summary>
		/// Calculates the intersection of two DateTimeRanges. If the two ranges do not overlap, the result is 
		/// an empty DateTimeRange. Otherwise, the result is the DateTimeRange that where both ranges overlap.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A DateTimeRange which is either empty, when there is no overlap, or contains the range where both ranges overlap</returns>
		public static DateTimeRange Intersection(this DateTimeRange a, DateTimeRange b)
		{
			if (a is null || b is null)
			{
				throw new ArgumentNullException();
			}

			DateTimeRange ab = new();

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
		/// Calculates the difference of two DateTimeRanges. If the two ranges do not overlap, the result is 
		/// the first DateTimeRange. Otherwise, the result is the first DateTimeRange without where both ranges overlap.
		/// </summary>
		/// <param name="a">The first range</param>
		/// <param name="b">The second range</param>
		/// <returns>A list of DateTimeRange</returns>
		public static List<DateTimeRange> Difference(this DateTimeRange a, DateTimeRange b)
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
				return new List<DateTimeRange>() {};
			}

			if(b.IsWithin(a))
			{
				return new List<DateTimeRange>() { new DateTimeRange(a.Start, b.Start), new DateTimeRange(b.End, a.End) };
			}

			if(a.Start.IsWithin(b))
			{
				return new List<DateTimeRange>() { new DateTimeRange(b.End, a.End) };
			}

			if(a.End.IsWithin(b))
			{
				return new List<DateTimeRange>() { new DateTimeRange(a.Start, b.Start) };
			}
			
			throw new InvalidOperationException();
		}

		/// <summary>
		/// Verifies if DateTimeRange a overlapps with DateTimeRange b.
		/// </summary>
		/// <param name="a">The DateTimeRange to verify a possible overlap</param>
		/// <param name="b">The DateTimerange to verify with</param>
		/// <returns>A bool.</returns>
		public static bool DoesOverlap(this DateTimeRange a, DateTimeRange b)
		{
			return a.Start.IsWithin(b) || a.End.IsWithin(b) 
				|| b.Start.IsWithin(a) || b.End.IsWithin(a);
		}

	}
}
