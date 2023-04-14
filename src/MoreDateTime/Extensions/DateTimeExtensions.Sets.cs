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
		public static DateTimeRange Union(this DateTimeRange a, DateTimeRange b)
		{
			DateTimeRange ab = new(a);

			// calculate the union of the two ranges
			ab.Start = a.Start < b.Start ? a.Start : b.Start;
			ab.End = a.End > b.End ? a.End : b.End;

			return ab;
		}
	}
}
