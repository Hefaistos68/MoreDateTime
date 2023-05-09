namespace MoreDateTime
{
    /// <summary>
    /// The extended date time comparer.
    /// </summary>
    public class ExtendedDateTimeComparer : IComparer<ExtendedDateTime>
    {
        /// <summary>
        /// Compares the two ExtendedDateTimes
        /// </summary>
        /// <param name="x">The first</param>
        /// <param name="y">The second</param>
        /// <returns>-1 when first is less or null, +1 when first is greater or second is null</returns>
        public int Compare(ExtendedDateTime? x, ExtendedDateTime? y)
        {
            if (x is null && y is null)
            {
                return 0;
            }

            if (y is null)
            {
                return 1;
            }

            if (x is null)
            {
                return -1;
            }

            long longXYear = x.Year;
            long longYYear = y.Year;

            if (x.YearExponent.HasValue)
            {
                try
                {
                    longXYear *= Convert.ToInt64(Math.Pow(10, x.YearExponent.Value));
                }
                catch (Exception)
                {
                    longXYear = x.Year < 0 ? long.MinValue : long.MaxValue;
                }
            }

            if (y.YearExponent.HasValue)
            {
                try
                {
                    longYYear *= Convert.ToInt64(Math.Pow(10, y.YearExponent.Value));
                }
                catch (Exception)
                {
                    longYYear = y.Year < 0 ? long.MinValue : long.MaxValue;
                }
            }

            if (longXYear > longYYear)
            {
                return 1;
            }
            else if (longXYear < longYYear)
            {
                return -1;
            }

            if (x.Season.IsUnspecified || y.Season.IsUnspecified)
            {
                if (y.Season.IsUnspecified)
                {
                    return 1;
                }
                else if (x.Season.IsUnspecified)
                {
                    return -1;
                }
                else if (x.Season > y.Season)
                {
                    return 1;
                }
                else if (x.Season < y.Season)
                {
                    return -1;
                }
            }

            if (x.Month > y.Month)
            {
                return 1;
            }
            else if (x.Month < y.Month)
            {
                return -1;
            }

            if (x.Day > y.Day)
            {
                return 1;
            }
            else if (x.Day < y.Day)
            {
                return -1;
            }

            if (x.Hour > y.Hour)
            {
                return 1;
            }
            else if (x.Hour < y.Hour)
            {
                return -1;
            }

            if (x.Minute > y.Minute)
            {
                return 1;
            }
            else if (x.Minute < y.Minute)
            {
                return -1;
            }

            if (x.Second > y.Second)
            {
                return 1;
            }
            else if (x.Second < y.Second)
            {
                return -1;
            }

            return 0;
        }
    }
}