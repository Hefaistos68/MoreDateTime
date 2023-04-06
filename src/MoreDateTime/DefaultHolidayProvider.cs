using System.Globalization;

using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// The default holiday provider, has only 4 common holidays, 01 Jan, 01 May, 25 Dec and 26 Dec
	/// </summary>
	public class DefaultHolidayProvider : IHolidayProvider
	{
		struct MonthDay 
		{ 
			int Month; 
			int Day;

			/// <summary>
			/// Initializes a new instance of the <see cref="MonthDay"/> class.
			/// </summary>
			/// <param name="month">The month.</param>
			/// <param name="day">The day.</param>
			public MonthDay(int month, int day)
			{
				this.Month = month;
				this.Day = day;
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="MonthDay"/> class.
			/// </summary>
			/// <param name="date">The date.</param>
			public MonthDay(DateTime date)
			{
				this.Month = date.Month;
				this.Day = date.Day;
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="MonthDay"/> class.
			/// </summary>
			/// <param name="date">The date.</param>
			public MonthDay(DateOnly date)
			{
				this.Month = date.Month;
				this.Day = date.Day;
			}
		}
		
		private readonly List<MonthDay> _commonHolidays = new List<MonthDay>
		{
			new MonthDay(1, 1),
			new MonthDay(5, 1),
			new MonthDay(12, 25),
			new MonthDay(12, 26),
		};

		/// <inheritdoc/>
		public bool IsPublicHoliday(DateTime date, CultureInfo cultureInfo)
		{
			return _commonHolidays.Contains(new MonthDay(date));
		}

		/// <inheritdoc/>
		public bool IsPublicHoliday(DateOnly date, CultureInfo cultureInfo)
		{
			return _commonHolidays.Contains(new MonthDay(date));
		}

		/// <inheritdoc/>
		public int NumberOfKnownHolidays(int year, CultureInfo? cultureInfo = null)
		{
			return   _commonHolidays.Count;
		}
	}
}