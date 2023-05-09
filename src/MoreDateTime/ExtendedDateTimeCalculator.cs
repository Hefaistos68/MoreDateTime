namespace MoreDateTime
{
    /// <summary>
    /// The extended date time calculator.
    /// </summary>
    public static class ExtendedDateTimeCalculator
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable ConstFieldDocumentationHeader // The field must have a documentation header.
        private const int DaysPer100Years = DaysPer4Years * 25 - 1;
        private const int DaysPer400Years = DaysPer100Years * 4 + 1;
        private const int DaysPer4Years = DaysPerYear * 4 + 1;
        private const int DaysPerYear = 365;
        private static readonly int[] DayOfWeekCenturyKeys = { 6, 4, 2, 0 };
        private static readonly int[] DayOfWeekMonthKeys365 = { 0, 0, 3, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
        private static readonly int[] DayOfWeekMonthKeys366 = { 0, 6, 2, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
        private static readonly int[] DaysInMonthArray = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private static readonly int[] DaysToMonth365 = { 0, 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        private static readonly int[] DaysToMonth366 = { 0, 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };
#pragma warning restore ConstFieldDocumentationHeader // The field must have a documentation header.
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Adds the months.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="monthsToAdd">The months to add.</param>
        /// <param name="dayExceedsDaysInMonthStrategy">The day exceeds days in month strategy.</param>
        /// <returns>An ExtendedDateTime.</returns>
        public static ExtendedDateTime AddMonths(ExtendedDateTime e, int monthsToAdd, DayExceedsDaysInMonthStrategy dayExceedsDaysInMonthStrategy)
        {
            var monthTotal = e.Month + monthsToAdd;
            var month = monthTotal % 12;

            if (month == 0)
            {
                month = 12;
            }

            var year = e.Year + (monthTotal - 1) / 12;
            var day = e.Day;

            if (day > DaysInMonth(year, month))
            {
                // TODO: review this logic
                switch (dayExceedsDaysInMonthStrategy)
                {
                    case DayExceedsDaysInMonthStrategy.RoundDown:
                        day = DaysInMonth(year, month);
                        break;

                    case DayExceedsDaysInMonthStrategy.Overflow:
                        day = day - DaysInMonth(year, month);
                        monthTotal = month + 1;
                        month = monthTotal % 12;

                        if (month == 0)
                        {
                            month = 12;
                        }

                        year = e.Year + (monthTotal - 1) / 12;
                        break;
                }
            }

            var extendedDateTime = new ExtendedDateTime(year, month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset);

            extendedDateTime.CopyFlagsAndPrecision(e);

            return extendedDateTime;
        }

        /// <summary>
        /// Number of Centuries of the year
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>An int.</returns>
        public static int CenturyOfYear(int year)
        {
            return year / 100;
        }

        /// <summary>
        /// Number of Days in the month
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>An int.</returns>
        public static int DaysInMonth(int year, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12");
            }

            return month == 2 && IsLeapYear(year) ? 29 : DaysInMonthArray[month];
        }

        /// <summary>
        /// Number of Days in the month
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns>An int.</returns>
        public static int DaysInMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12");
            }

            return DaysInMonthArray[month];
        }

        /// <summary>
        /// Number of days in the year
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>An int.</returns>
        public static int DaysInYear(int year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        /// <summary>
        /// Number of days since day 1 of the year until the given month
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>An int.</returns>
        public static int DaysToMonth(int year, int month)
        {
            return IsLeapYear(year) ? DaysToMonth366[month] : DaysToMonth365[month];
        }

        /// <summary>
        /// Tests if the given year is a leap year
        /// </summary>
        /// <param name="year">The year</param>
        /// <returns>A bool.</returns>
        public static bool IsLeapYear(int year) // http://www.timeanddate.com/date/leapyear.html
        {
            return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        }

        /// <summary>
        /// Number of years since the beginning of the years century
        /// </summary>
        /// <param name="year">The year</param>
        /// <returns>An int.</returns>
        public static int YearOfCentury(int year)
        {
            return year % 100;
        }

        /// <summary>
        /// Adds a TimeSpan to the ExtendedDateTime
        /// </summary>
        /// <param name="e">The date time value</param>
        /// <param name="t">The t.</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime Add(ExtendedDateTime e, TimeSpan t)
        {
            var extendedDateTime = TimeSpanToExtendedDateTime(e - new ExtendedDateTime(1, 1, 1, 0, 0, 0, null) + t, e.UtcOffset);

            extendedDateTime.CopyFlagsAndPrecision(e);

            return extendedDateTime;
        }

        /// <summary>
        /// Adds years to the date time value
        /// </summary>
        /// <param name="e">The date time valye</param>
        /// <param name="count">The number of years to add</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime AddYears(ExtendedDateTime e, int count)
        {
            // TODO: review this logic and implement normal DateTime logic
            if (e.Month == 2 && e.Day == 29 && IsLeapYear(e.Year) && !IsLeapYear(e.Year + count))
            {
                throw new InvalidOperationException("Years cannot be added to a leap day unless the resulting year also has a leap day.");
            }

            var extendedDateTime = new ExtendedDateTime(e.Year + count, e.Month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset);

            extendedDateTime.CopyFlagsAndPrecision(e);

            return extendedDateTime;
        }

        /// <summary>
        /// Returns the day of the week
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns>A DayOfWeek.</returns>
        internal static DayOfWeek DayOfWeek(ExtendedDateTime e) // http://www.stoimen.com/blog/2012/04/24/computer-algorithms-how-to-determine-the-day-of-the-week/
        {
            var yearOfCentury = YearOfCentury(e.Year);
            var centuryOfYear = CenturyOfYear(e.Year);

            var monthKey = IsLeapYear(e.Year) ? DayOfWeekMonthKeys366[e.Month] : DayOfWeekMonthKeys365[e.Month];
            var centuryKey = DayOfWeekCenturyKeys[centuryOfYear % 4 + centuryOfYear < 0 ? 4 : 0];

            var dayOfWeek = (e.Day + monthKey + yearOfCentury + yearOfCentury / 4 + centuryKey) % 7;

            return (DayOfWeek)dayOfWeek;
        }

        /// <summary>
        /// Subtracts a TimeSpan from the ExtendedDateTime
        /// </summary>
        /// <param name="e">The date time value</param>
        /// <param name="t">The TimeSpan to subtract</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime Subtract(ExtendedDateTime e, TimeSpan t)
        {
            var extendedDateTime = TimeSpanToExtendedDateTime(e - new ExtendedDateTime(1, 1, 1, 0, 0, 0, null) - t, e.UtcOffset);

            extendedDateTime.CopyFlagsAndPrecision(e);

            return extendedDateTime;
        }

        /// <summary>
        /// Subtracts the.
        /// </summary>
        /// <param name="later">The later.</param>
        /// <param name="earlier">The earlier.</param>
        /// <returns>A TimeSpan.</returns>
        internal static TimeSpan Subtract(ExtendedDateTime later, ExtendedDateTime earlier)
        {
            TimeSpan laterUtcOffset = later.UtcOffset ?? TimeSpan.Zero;
            TimeSpan earlierUtcOffset = earlier.UtcOffset ?? TimeSpan.Zero;

            return TimeSpan.FromDays(
                         (later.Year - earlier.Year) * 365
                       + (later.Year - 1) / 4 - (earlier.Year - 1) / 4
                       - (later.Year - 1) / 100 + (earlier.Year - 1) / 100
                       + (later.Year - 1) / 400 - (earlier.Year - 1) / 400
                       + DaysToMonth(later.Year, later.Month!)
                       - DaysToMonth(earlier.Year, earlier.Month!)
                       + later.Day
                       - earlier.Day)
                 + TimeSpan.FromHours(later.Hour - earlier.Hour + laterUtcOffset.Hours - earlierUtcOffset.Hours)
                 + TimeSpan.FromMinutes(later.Minute - earlier.Minute + laterUtcOffset.Minutes - earlierUtcOffset.Minutes)
                 + TimeSpan.FromSeconds(later.Second - earlier.Second);
        }

        /// <summary>
        /// Subtracts the months.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="count">The count.</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime SubtractMonths(ExtendedDateTime e, int count)
        {
            var month = e.Month - count % 12;
            var year = e.Year - count / 12;

            if (e.Day > DaysInMonth(year, month))
            {
                throw new InvalidOperationException("The day is greater than the number of days in the resulting month.");
            }

            var extendedDateTime = new ExtendedDateTime(year, month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset);

            extendedDateTime.CopyFlagsAndPrecision(e);

            return extendedDateTime;
        }

        /// <summary>
        /// Subtracts the years.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="count">The count.</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime SubtractYears(ExtendedDateTime e, int count)
        {
            if (e.Month == 2 && e.Day == 29 && IsLeapYear(e.Year) && !IsLeapYear(e.Year - count))
            {
                throw new InvalidOperationException("The years subtracted from a leap day must result in another leap day.");
            }

            var extendedDateTime = new ExtendedDateTime(e.Year - count, e.Month, e.Day, e.Hour, e.Minute, e.Second, e.UtcOffset);

            extendedDateTime.CopyFlagsAndPrecision(e);

            return extendedDateTime;
        }

        /// <summary>
        /// Times the span to extended date time.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="utcOffset">The utc offset.</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime TimeSpanToExtendedDateTime(TimeSpan t, TimeSpan? utcOffset)
        {
            // To determine the precise date and time, we will use the following method:
            //     1. Add the number of days (rounded down) from 1 CE to the starting date to the number of days in the added duration.
            //     2. Determine the number of whole 400 years periods which have elapsed since 1 CE.
            //     3. Subtract the number of days elapsed during the 400 year periods from the day total.
            //     4. Repeat steps 2 and 3 for 100, 4, and 1 year periods. The remaining days will represent the number of days since the first of january.
            //     5. Add one to the day total to get the ordinal day of the year.
            //     6. To determine the month, we make an estimate by dividing the number of days by 32. We use 32 since it is greater
            //        than the number of days in any month and it is an easy number to divide using bitwise division (since 32 = 2^5).
            //        The actual month will be equal to or greater than the estimate because at the end of each 32 day cycle, the month
            //        will have already advanced.
            //     7. To find the actual month, we increment the month until the day total is greater than or equal to the
            //        number of days between the first of january and the start of the month.
            //     8. Subtract the number of days between the first of january and the start of the month from the day total to get
            //        the number of days from the first of the month to the day of the month.
            //     9. Add one to the day total to get the day of the month.
            //    10. The remainders of the total hours, minutes, and seconds when divided by their carry-over values will give
            //        the correct time.

            var totalDays = (int)t.TotalDays;

            int fourHundredYearPeriods = totalDays / DaysPer400Years;
            totalDays -= fourHundredYearPeriods * DaysPer400Years;

            int oneHundredYearPeriods = totalDays / DaysPer100Years;
            totalDays -= oneHundredYearPeriods * DaysPer100Years;

            int fourYearPeriods = totalDays / DaysPer4Years;
            totalDays -= fourYearPeriods * DaysPer4Years;

            int oneYearPeriods = totalDays / DaysPerYear;
            totalDays -= oneYearPeriods * DaysPerYear;

            int year = fourHundredYearPeriods * 400 + oneHundredYearPeriods * 100 + fourYearPeriods * 4 + oneYearPeriods + 1;

            int dayOfYear = totalDays + 1;

            int month = totalDays / 32 + 1;

            while (month < 12 && totalDays >= DaysToMonth(year, month + 1))
            {
                month++;
            }

            totalDays -= DaysToMonth(year, month);

            totalDays++;

            var day = totalDays;
            var hour = (int)(t.TotalHours % 24);
            var minute = (int)(t.TotalMinutes % 60);
            var second = (int)(t.TotalSeconds % 60);

            return new ExtendedDateTime(year, month, totalDays, hour, minute, second, utcOffset);
        }

        /// <summary>
        /// Tos the rounded precision.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="p">The p.</param>
        /// <param name="roundUp">If true, round up.</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime ToRoundedPrecision(ExtendedDateTime e, ExtendedDateTimePrecision p, bool roundUp = false)
        {
            var year = e.Year;
            var month = e.Month;
            var day = e.Day;
            var hour = e.Hour;
            var minute = e.Minute;
            var second = e.Second;

            if (p < ExtendedDateTimePrecision.Second)
            {
                second = 0;
            }

            if (p < ExtendedDateTimePrecision.Minute)
            {
                minute = 0;
            }

            if (p < ExtendedDateTimePrecision.Hour)
            {
                hour = 0;
            }

            if (p < ExtendedDateTimePrecision.Day)
            {
                day = 1;
            }

            if (p < ExtendedDateTimePrecision.Month)
            {
                month = 1;
            }

            if (roundUp)
            {
                switch (p)
                {
                    case ExtendedDateTimePrecision.Year:

                        if (e.Month > 1)
                        {
                            year++;
                        }

                        break;

                    case ExtendedDateTimePrecision.Month:

                        if (e.Day > 1)
                        {
                            month++;
                        }

                        break;

                    case ExtendedDateTimePrecision.Day:

                        if (e.Hour > 0)
                        {
                            day++;
                        }

                        break;

                    case ExtendedDateTimePrecision.Hour:

                        if (e.Minute > 0)
                        {
                            hour++;
                        }

                        break;

                    case ExtendedDateTimePrecision.Minute:

                        if (e.Second > 0)
                        {
                            minute++;
                        }

                        break;

                    case ExtendedDateTimePrecision.Second:

                        break;
                }

                if (second > 59)
                {
                    second = 0;
                    minute++;
                }

                if (minute > 59)
                {
                    minute = 0;
                    hour++;
                }

                if (hour > 23)
                {
                    hour = 0;
                    day++;
                }

                if (day > DaysInMonth(year, month == 13 ? 1 : month))
                {
                    day = 1;
                    month++;
                }

                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }

            var extendedDateTime = new ExtendedDateTime(year, month, day, hour, minute, second, e.UtcOffset);

            extendedDateTime.CopyFlagsAndPrecision(e);
            extendedDateTime.Precision = p;

            return extendedDateTime;
        }
    }
}