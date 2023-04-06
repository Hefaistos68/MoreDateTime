using System.Globalization;

using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// The null holiday provider, provides no holidays at all
	/// </summary>
	public class NullHolidayProvider : IHolidayProvider
	{
		/// <inheritdoc/>
		public bool IsPublicHoliday(DateTime date, CultureInfo cultureInfo)
		{
			return false;
		}

		/// <inheritdoc/>
		public bool IsPublicHoliday(DateOnly date, CultureInfo cultureInfo)
		{
			return false;
		}

		/// <inheritdoc/>
		int IHolidayProvider.NumberOfKnownHolidays(int year, CultureInfo? cultureInfo)
		{
			return 0;
		}
	}
}