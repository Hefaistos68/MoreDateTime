using System;

namespace MoreDateTime.Interfaces
{
	/// <summary>
	/// A Date/Time range provider, with an start and an end date
	/// </summary>
	public interface IDateTimeRange
	{
		/// <summary>
		/// Gets or sets the start DateTime
		/// </summary>
		DateTime StartTime { get; set; }
		
		/// <summary>
		/// Gets or sets the end DateTime
		/// </summary>
		DateTime EndTime { get; set; }
	}

}
