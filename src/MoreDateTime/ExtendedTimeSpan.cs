namespace MoreDateTime
{
    /// <summary>
    /// An timespan class adapted for the ExtendedDateTime class
    /// </summary>
    public readonly struct ExtendedTimeSpan
    {
#pragma warning disable IDE1006 // Naming Styles
        private readonly int _exclusiveDays;
        private readonly int _months;
        private readonly TimeSpan _timeSpan;
        private readonly double _totalMonths;
        private readonly double _totalYears;
        private readonly int _years;
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedTimeSpan"/> class.
        /// </summary>
        /// <param name="years">The years.</param>
        /// <param name="totalYears">The total years.</param>
        /// <param name="months">The months.</param>
        /// <param name="totalMonths">The total months.</param>
        /// <param name="exclusiveDays">The exclusive days.</param>
        /// <param name="timeSpan">The time span.</param>
        internal ExtendedTimeSpan(int years, double totalYears, int months, double totalMonths, int exclusiveDays, TimeSpan timeSpan)
        {
            _years = years;
            _totalYears = totalYears;
            _months = months;
            _totalMonths = totalMonths;
            _exclusiveDays = exclusiveDays;
            _timeSpan = timeSpan;
        }

        /// <summary>
        /// Gets the days.
        /// </summary>
        public int Days
        {
            get
            {
                return _exclusiveDays;
            }
        }

        /// <summary>
        /// Gets the hours.
        /// </summary>
        public int Hours
        {
            get
            {
                return _timeSpan.Hours;
            }
        }

        /// <summary>
        /// Gets the minutes.
        /// </summary>
        public int Minutes
        {
            get
            {
                return _timeSpan.Minutes;
            }
        }

        /// <summary>
        /// Gets the months.
        /// </summary>
        public int Months
        {
            get
            {
                return _months;
            }
        }

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        public int Seconds
        {
            get
            {
                return _timeSpan.Seconds;
            }
        }

        /// <summary>
        /// Gets the time span.
        /// </summary>
        public TimeSpan TimeSpan
        {
            get
            {
                return _timeSpan;
            }
        }

        /// <summary>
        /// Gets the total days.
        /// </summary>
        public double TotalDays
        {
            get
            {
                return _timeSpan.TotalDays;
            }
        }

        /// <summary>
        /// Gets the total hours.
        /// </summary>
        public double TotalHours
        {
            get
            {
                return _timeSpan.TotalHours;
            }
        }

        /// <summary>
        /// Gets the total minutes.
        /// </summary>
        public double TotalMinutes
        {
            get
            {
                return _timeSpan.TotalMinutes;
            }
        }

        /// <summary>
        /// Gets the total months.
        /// </summary>
        public double TotalMonths
        {
            get
            {
                return _totalMonths;
            }
        }

        /// <summary>
        /// Gets the total seconds.
        /// </summary>
        public double TotalSeconds
        {
            get
            {
                return _timeSpan.TotalSeconds;
            }
        }

        /// <summary>
        /// Gets the total years.
        /// </summary>
        public double TotalYears
        {
            get
            {
                return _totalYears;
            }
        }

        /// <summary>
        /// Gets the years.
        /// </summary>
        public int Years
        {
            get
            {
                return _years;
            }
        }
    }
}