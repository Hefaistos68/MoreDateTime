namespace MoreDateTime.Interfaces
{
    /// <summary>
    /// The extended date time extremity provider, provides methods to get the earliest and latest possible date times
    /// </summary>
    public interface IExtendedDateTimeExtremityProvider
    {
        /// <summary>
        /// Gets the earliest possible date time.
        /// </summary>
        /// <returns>An ExtendedDateTime.</returns>
        ExtendedDateTime Earliest();

        /// <summary>
        /// Gets the latest possible date time.
        /// </summary>
        /// <returns>An ExtendedDateTime.</returns>
        ExtendedDateTime Latest();
    }
}
