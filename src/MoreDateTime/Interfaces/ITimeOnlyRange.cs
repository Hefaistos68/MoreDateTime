namespace MoreDateTime.Interfaces
{
	/// <summary>
	/// A Date/Time range provider, with an start and an end date
	/// </summary>
	public interface ITimeOnlyRange
	{
		/// <summary>
		/// Gets or sets the start time
		/// </summary>
		TimeOnly Start { get; set; }
		
		/// <summary>
		/// Gets or sets the end time
		/// </summary>
		TimeOnly End { get; set; }

		/// <summary>
		/// Gets the distance between the <see cref="Start"/> and the <see cref="End"/>
		/// </summary>
		public TimeSpan Distance();

	}

}
