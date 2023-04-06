using System.Globalization;

using MoreDateTime.Extensions;

using static MoreDateTime.Extensions.DateTimeExtensions;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class TimeOnlyExtensions
	{
		/// <summary>
		/// Compares two dates for equality down to the given precision
		/// </summary>
		/// <param name="dt">The first TimeOnly argument</param>
		/// <param name="other">The TimeOnly argument to compare with</param>
		/// <param name="truncateTo">The precision to truncate to</param>
		/// <returns>A bool.</returns>
		public static bool IsEqual(this TimeOnly dt, TimeOnly other, DateTruncate truncateTo, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return truncateTo switch
			{
				DateTruncate.Year => true,
				DateTruncate.Month => true,
				DateTruncate.Week => true,
				DateTruncate.Day => true,
				DateTruncate.Hour => dt.Hour == other.Hour,
				DateTruncate.Minute => dt.Hour == other.Hour && dt.Minute == other.Minute,
				DateTruncate.Second => dt.Hour == other.Hour && dt.Minute == other.Minute && dt.Second == other.Second,
				_ => false,
			};
		}

		/// <summary>
		/// Compares two dates for equality down to the hour (minutes, seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first TimeOnly argument</param>
		/// <param name="other">The TimeOnly argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToHour(this TimeOnly dt, TimeOnly other)
		{
			return dt.IsEqual(other, DateTruncate.Hour);
		}

		/// <summary>
		/// Compares two dates for equality down to the minute (seconds and milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first TimeOnly argument</param>
		/// <param name="other">The TimeOnly argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToMinute(this TimeOnly dt, TimeOnly other)
		{
			return dt.IsEqual(other, DateTruncate.Minute);
		}

		/// <summary>
		/// Compares two dates for equality down to the second (milliseconds are ignored)
		/// </summary>
		/// <param name="dt">The first TimeOnly argument</param>
		/// <param name="other">The TimeOnly argument to compare with</param>
		/// <returns>True if the dates are equal down to the second</returns>
		public static bool IsEqualDownToSecond(this TimeOnly dt, TimeOnly other)
		{
			return dt.IsEqual(other, DateTruncate.Second);
		}
	}
}