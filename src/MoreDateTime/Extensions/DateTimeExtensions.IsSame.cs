using System.Globalization;

namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateTimeExtensions
	{
		/// <summary>
		/// Verifies if the two dates are at the same year
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are on the same year</returns>
		public static bool IsSameDay(this DateTime dt, DateTime other)
		{
			return dt.IsEqualDownToDay(other);
		}

		/// <summary>
		/// Verifies if the two dates are at the same hour
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are on the same hour</returns>
		public static bool IsSameHour(this DateTime dt, DateTime other)
		{
			return dt.IsEqualDownToHour(other);
		}

		/// <summary>
		/// Verifies if the two dates are at the same minute
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are on the same minute</returns>
		public static bool IsSameMinute(this DateTime dt, DateTime other)
		{
			return dt.IsEqualDownToMinute(other);
		}

		/// <summary>
		/// Verifies if the two dates are at the same year
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are on the same year</returns>
		public static bool IsSameMonth(this DateTime dt, DateTime other)
		{
			return dt.IsEqualDownToMonth(other);
		}

		/// <summary>
		/// Verifies if the two dates are at the same second
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are on the same second</returns>
		public static bool IsSameSecond(this DateTime dt, DateTime other)
		{
			return dt.IsEqualDownToSecond(other);
		}

		/// <summary>
		/// Verifies if the two dates are at the same week
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <param name="cultureInfo">The CultureInfo to use for week calculation, can be null for current culture</param>
		/// <returns>True if the dates are on the same week</returns>
		public static bool IsSameWeek(this DateTime dt, DateTime other, CultureInfo? cultureInfo = null)
		{
			return dt.IsEqualDownToWeek(other, cultureInfo);
		}

		/// <summary>
		/// Verifies if the two dates are at the same year
		/// </summary>
		/// <param name="dt">The first DateTime argument</param>
		/// <param name="other">The DateTime argument to compare with</param>
		/// <returns>True if the dates are on the same year</returns>
		public static bool IsSameYear(this DateTime dt, DateTime other)
		{
			return dt.IsEqualDownToYear(other);
		}
	}
}