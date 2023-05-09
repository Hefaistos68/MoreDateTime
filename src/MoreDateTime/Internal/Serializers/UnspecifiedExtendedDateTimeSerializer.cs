namespace MoreDateTime.Internal.Serializers
{
    /// <summary>
    /// The unspecified extended date time serializer.
    /// </summary>
    internal static class UnspecifiedExtendedDateTimeSerializer
    {
        /// <summary>
        /// Serializes the.
        /// </summary>
        /// <param name="unspecifiedExtendedDateTime">The unspecified extended date time.</param>
        /// <returns>A string.</returns>
        internal static string Serialize(UnspecifiedExtendedDateTime unspecifiedExtendedDateTime)
        {
            if (!unspecifiedExtendedDateTime.Day.IsNone)
            {
                return string.Format("{0}-{1}-{2}", unspecifiedExtendedDateTime.Year, unspecifiedExtendedDateTime.Month, unspecifiedExtendedDateTime.Day);
            }

            if (!unspecifiedExtendedDateTime.Month.IsNone)
            {
                return string.Format("{0}-{1}", unspecifiedExtendedDateTime.Year, unspecifiedExtendedDateTime.Month);
            }

            return unspecifiedExtendedDateTime.Year.IsNone ? string.Empty : unspecifiedExtendedDateTime.Year;
        }
    }
}
