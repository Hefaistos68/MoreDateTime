using System;

namespace MoreDateTime.Interfaces
{
	/// <summary>
	/// The interface for providing DateTime information
	/// </summary>
	public interface IDateTimeProvider
	{
		/// <summary>
		/// Gets the current UTC DateTime (or the mock value if set)
		/// </summary>
		DateTime UtcNow { get; }

		/// <summary>
		/// Gets the current DateTime (or the mock value if set)
		/// </summary>
		DateTime Now { get; }

		/// <summary>
		/// Gets the date part of the current DateTime with the time set to 00:00:00 (or the mock value if set)
		/// </summary>
		DateTime Today { get;  }

		/// <summary>
		/// Gets the date part of the current UTC DateTime with the time set to 00:00:00 (or the mock value if set)
		/// </summary>
		DateTime UtcToday { get;  }
	}
}
