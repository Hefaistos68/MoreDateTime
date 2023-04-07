namespace MoreDateTime.Extensions
{
	/// <inheritdoc/>
	public static partial class DateOnlyExtensions
	{
		/// <summary>
		/// Returns the first Friday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the First Friday ofthis month</returns>
		public static DateOnly FirstFridayOfMonth(this DateOnly me) => me.FirstWeekdayOfMonth(DayOfWeek.Friday);

		/// <summary>
		/// Returns the first Monday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the First Monday ofthis month</returns>
		public static DateOnly FirstMondayOfMonth(this DateOnly me) => me.FirstWeekdayOfMonth(DayOfWeek.Monday);

		/// <summary>
		/// Returns the first Saturday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the First Saturday ofthis month</returns>
		public static DateOnly FirstSaturdayOfMonth(this DateOnly me) => me.FirstWeekdayOfMonth(DayOfWeek.Saturday);

		/// <summary>
		/// Returns the first Sunday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the First Sunday ofthis month</returns>
		public static DateOnly FirstSundayOfMonth(this DateOnly me) => me.FirstWeekdayOfMonth(DayOfWeek.Sunday);

		/// <summary>
		/// Returns the first Thursday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the First Thursday ofthis month</returns>
		public static DateOnly FirstThursdayOfMonth(this DateOnly me) => me.FirstWeekdayOfMonth(DayOfWeek.Thursday);

		/// <summary>
		/// Returns the first MoTuesday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the First Tuesday ofthis month</returns>
		public static DateOnly FirstTuesdayOfMonth(this DateOnly me) => me.FirstWeekdayOfMonth(DayOfWeek.Tuesday);

		/// <summary>
		/// Returns the first Wednesday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the First Wednesday ofthis month</returns>
		public static DateOnly FirstWednesdayOfMonth(this DateOnly me) => me.FirstWeekdayOfMonth(DayOfWeek.Wednesday);

		/// <summary>
		/// Returns the first given weekday in the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <param name="dayOfweek">The <see cref="DayOfWeek"/> to find</param>
		/// <returns>A <see cref="DateOnly"/> object representing the last given weekday in this month</returns>
		public static DateOnly FirstWeekdayOfMonth(this DateOnly me, DayOfWeek dayOfweek)
		{
			DateOnly startDate = me.StartOfMonth();

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
		/// <returns>A <see cref="DateOnly"/> object representing the last Friday ofthis month</returns>
		public static DateOnly LastFridayOfMonth(this DateOnly me) => me.LastWeekdayOfMonth(DayOfWeek.Friday);

		/// <summary>
		/// Returns the last Monday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the last Monday ofthis month</returns>
		public static DateOnly LastMondayOfMonth(this DateOnly me) => me.LastWeekdayOfMonth(DayOfWeek.Monday);

		/// <summary>
		/// Returns the last Saturday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the last Saturday ofthis month</returns>
		public static DateOnly LastSaturdayOfMonth(this DateOnly me) => me.LastWeekdayOfMonth(DayOfWeek.Saturday);

		/// <summary>
		/// Returns the last Sunday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the last Sunday ofthis month</returns>
		public static DateOnly LastSundayOfMonth(this DateOnly me) => me.LastWeekdayOfMonth(DayOfWeek.Sunday);

		/// <summary>
		/// Returns the last Thursday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the last Thursday ofthis month</returns>
		public static DateOnly LastThursdayOfMonth(this DateOnly me) => me.LastWeekdayOfMonth(DayOfWeek.Thursday);

		/// <summary>
		/// Returns the last Tuesday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the last Tuesday ofthis month</returns>
		public static DateOnly LastTuesdayOfMonth(this DateOnly me) => me.LastWeekdayOfMonth(DayOfWeek.Tuesday);

		/// <summary>
		/// Returns the last Wednesday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <returns>A <see cref="DateOnly"/> object representing the last Wednesday ofthis month</returns>
		public static DateOnly LastWednesdayOfMonth(this DateOnly me) => me.LastWeekdayOfMonth(DayOfWeek.Wednesday);

		/// <summary>
		/// Returns the last given weekday of the month
		/// </summary>
		/// <param name="me">The <see cref="DateOnly"/> object</param>
		/// <param name="dayOfweek">The <see cref="DayOfWeek"/> to find</param>
		/// <returns>A <see cref="DateOnly"/> object representing the last given weekday in this month</returns>
		public static DateOnly LastWeekdayOfMonth(this DateOnly me, DayOfWeek dayOfweek)
		{
			DateOnly endDate = me.EndOfMonth();

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