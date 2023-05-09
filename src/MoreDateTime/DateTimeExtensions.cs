namespace MoreDateTime.Extensions
{
    /// <summary>
    /// The date time extensions.
    /// </summary>
    public static partial class DateTimeExtensions
	{
		/// <summary>
		/// Converts a DateTime to a ExtendedDateTime
		/// </summary>
		/// <param name="d">The d.</param>
		/// <returns>An ExtendedDateTime.</returns>
		public static ExtendedDateTime ToExtendedDateTime(this DateTime d)
		{
			return new ExtendedDateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, null);
		}

		/// <summary>
		/// Converts a DateTime to a ExtendedDateTime
		/// </summary>
		/// <param name="d">The d.</param>
		/// <returns>An ExtendedDateTime.</returns>
		public static ExtendedDateTime ToExtendedDateTime(this DateOnly d)
		{
			return new ExtendedDateTime(year: d.Year, month: d.Month, day: d.Day, utcOffset: null);
		}
	}
}