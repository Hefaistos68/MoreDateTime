namespace MoreDateTime.Interfaces
{
	/// <summary>
	/// A Date/Time range provider, with an start and an end date
	/// </summary>
	public interface IDateOnlyRange
	{
		/// <summary>
		/// Gets or sets the start date
		/// </summary>
		DateOnly Start { get; set; }
		
		/// <summary>
		/// Gets or sets the end date
		/// </summary>
		DateOnly End { get; set; }

		/// <summary>
		/// Gets the distance between the <see cref="Start"/> and the <see cref="End"/>
		/// </summary>
		public TimeSpan Distance();

	}

}
