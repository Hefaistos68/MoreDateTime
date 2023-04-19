using System.Globalization;

using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

using Nager.Date;

namespace MoreDateTime
{
	/// <summary> Provider for the Nager.Date library</summary>
	internal class NagerHolidayProvider : IHolidayProvider
	{
		/// <inheritdoc/>
		public bool IsPublicHoliday(DateTime date, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return DateSystem.IsPublicHoliday(date, cultureInfo.TwoLetterISOLanguageName);
		}

		/// <inheritdoc/>
		public bool IsPublicHoliday(DateOnly date, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return DateSystem.IsPublicHoliday(date.ToDateTime(), cultureInfo.TwoLetterISOLanguageName);
		}

		/// <inheritdoc/>
		public int NumberOfKnownHolidays(int year, CultureInfo? cultureInfo = null)
		{
			cultureInfo ??= CultureInfo.CurrentCulture;

			return DateSystem.GetPublicHolidays(DateTime.Today.Year, cultureInfo.TwoLetterISOLanguageName).Count();
		}
	}
}