using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDateTime.Interfaces
{
	/// <summary>
	/// An interface to provide access to holiday information
	/// </summary>
	public interface IHolidayProvider
	{
		/// <summary>
		/// Checks whether the given date is a public holiday for the given Calendar in CultureInfo
		/// </summary>
		/// <param name="date">The date</param>
		/// <param name="cultureInfo">The culture info.</param>
		/// <returns>A bool.</returns>
		bool IsPublicHoliday(DateTime date, CultureInfo? cultureInfo = null);

		/// <summary>
		/// Checks whether the given date is a public holiday for the given Calendar in CultureInfo
		/// </summary>
		/// <param name="date">The date</param>
		/// <param name="cultureInfo">The culture info.</param>
		/// <returns>A bool.</returns>
		bool IsPublicHoliday(DateOnly date, CultureInfo? cultureInfo = null);

		/// <summary>
		/// Gets the number of known holidays
		/// </summary>
		/// <param name="year">The year for which the number of holidays is requested</param>
		/// <param name="cultureInfo">The </param>
		int NumberOfKnownHolidays(int year, CultureInfo? cultureInfo = null);
	}
}
