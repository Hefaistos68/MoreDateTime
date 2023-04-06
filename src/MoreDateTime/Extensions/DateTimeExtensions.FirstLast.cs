namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateTimeExtensions
	{
		/// <summary>
		/// Returns the first Friday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the first Friday in this month</returns>
		public static DateTime FirstFridayOfMonth(this DateTime me) => FirstWeekdayOfMonth(me, DayOfWeek.Friday);

		/// <summary>
		/// Returns the first monday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the first monday in this month</returns>
		public static DateTime FirstMondayOfMonth(this DateTime me) => FirstWeekdayOfMonth(me, DayOfWeek.Monday);

		/// <summary>
		/// Returns the first Saturday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the first Saturday in this month</returns>
		public static DateTime FirstSaturdayOfMonth(this DateTime me) => FirstWeekdayOfMonth(me, DayOfWeek.Saturday);

		/// <summary>
		/// Returns the first Sunday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the first Sunday in this month</returns>
		public static DateTime FirstSundayOfMonth(this DateTime me)	=> FirstWeekdayOfMonth(me, DayOfWeek.Sunday);

		/// <summary>
		/// Returns the first Thursday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the first Thursday in this month</returns>
		public static DateTime FirstThursdayOfMonth(this DateTime me) => FirstWeekdayOfMonth(me, DayOfWeek.Thursday);

		/// <summary>
		/// Returns the first Tuesday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the first Tuesday in this month</returns>
		public static DateTime FirstTuesdayOfMonth(this DateTime me) => FirstWeekdayOfMonth(me, DayOfWeek.Tuesday);

		/// <summary>
		/// Returns the first Wednesday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the first Wednesday in this month</returns>
		public static DateTime FirstWednesdayOfMonth(this DateTime me) => FirstWeekdayOfMonth(me, DayOfWeek.Wednesday);

		/// <summary>
		/// Returns the first given weekday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <param name="dayOfweek">The <see cref="DayOfWeek"/> to find</param>
		/// <returns>A <see cref="DateTime"/> object representing the first given weekday in this month</returns>
		public static DateTime FirstWeekdayOfMonth(this DateTime me, DayOfWeek dayOfweek)
		{
			DateTime startDate = me.StartOfMonth();

			for (int i = 0; i < DaysInWeek; i++)
			{
				if (startDate.DayOfWeek == dayOfweek)
				{
					return startDate;
				}

				startDate = startDate.NextDay();
			}

			throw new InvalidOperationException("day of week was not found");
		}
		/// <summary>
		/// Returns the last Friday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the last Friday in this month</returns>
		public static DateTime LastFridayOfMonth(this DateTime me) => LastWeekdayOfMonth(me, DayOfWeek.Friday);

		/// <summary>
		/// Returns the last Monday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the last monday in this month</returns>
		public static DateTime LastMondayOfMonth(this DateTime me) => LastWeekdayOfMonth(me, DayOfWeek.Monday);

		/// <summary>
		/// Returns the last Saturday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the last Saturday in this month</returns>
		public static DateTime LastSaturdayOfMonth(this DateTime me) => LastWeekdayOfMonth(me, DayOfWeek.Saturday);

		/// <summary>
		/// Returns the last Sunday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the last Sunday in this month</returns>
		public static DateTime LastSundayOfMonth(this DateTime me) => LastWeekdayOfMonth(me, DayOfWeek.Sunday);

		/// <summary>
		/// Returns the last Thursday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the last Thursday in this month</returns>
		public static DateTime LastThursdayOfMonth(this DateTime me) => LastWeekdayOfMonth(me, DayOfWeek.Thursday);

		/// <summary>
		/// Returns the last Tuesday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the last Tuesday in this month</returns>
		public static DateTime LastTuesdayOfMonth(this DateTime me) => LastWeekdayOfMonth(me, DayOfWeek.Tuesday);

		/// <summary>
		/// Returns the last Wednesday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateTime"/> object representing the last Wednesday in this month</returns>
		public static DateTime LastWednesdayOfMonth(this DateTime me) => LastWeekdayOfMonth(me, DayOfWeek.Wednesday);

		/// <summary>
		/// Returns the last given weekday of the year
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <param name="dayOfweek">The <see cref="DayOfWeek"/> to find</param>
		/// <returns>A <see cref="DateTime"/> object representing the last given weekday in this year</returns>
		public static DateTime LastWeekdayOfMonth(this DateTime me, DayOfWeek dayOfweek)
		{
			DateTime endDate = me.EndOfMonth();

			for (int i = 0; i < DaysInWeek; i++)
			{
				if (endDate.DayOfWeek == dayOfweek)
				{
					return endDate;
				}

				endDate = endDate.PreviousDay();
			}

			throw new InvalidOperationException("day of week was not found");
		}
	}
}