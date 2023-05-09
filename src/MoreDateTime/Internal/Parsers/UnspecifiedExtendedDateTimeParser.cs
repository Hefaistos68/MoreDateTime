using MoreDateTime;
using MoreDateTime.Exceptions;

namespace MoreDateTime.Internal.Parsers
{
    /// <summary>
    /// The unspecified extended date time parser.
    /// </summary>
    internal static class UnspecifiedExtendedDateTimeParser
    {
        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="unspecifiedExtendedDateTimeString">The unspecified extended date time string.</param>
        /// <param name="unspecifiedExtendedDateTime">The unspecified extended date time.</param>
        /// <returns>An UnspecifiedExtendedDateTime.</returns>
        internal static UnspecifiedExtendedDateTime Parse(string unspecifiedExtendedDateTimeString, UnspecifiedExtendedDateTime? unspecifiedExtendedDateTime = null)
        {
            if (unspecifiedExtendedDateTimeString.Length > 10)
            {
                throw new ParseException("An unspecified extended date time must be between 4 and 10 characters long.", unspecifiedExtendedDateTimeString);
            }

            var components = unspecifiedExtendedDateTimeString.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            // 			if (components.Length > 3)
            // 			{
            // 				throw new ParseException("An unspecified extended date time can have at most two components.", unspecifiedExtendedDateTimeString);
            // 			}

            unspecifiedExtendedDateTime ??= new UnspecifiedExtendedDateTime();
            unspecifiedExtendedDateTime.Year = new DateTimeValue(components[0], DateTimeValue.ValueFlags.Exact);

            if (components.Length == 1)
            {
                return unspecifiedExtendedDateTime;
            }

            unspecifiedExtendedDateTime.Month = new DateTimeValue(components[1], DateTimeValue.ValueFlags.Exact);

            if (components.Length == 2)
            {
                return unspecifiedExtendedDateTime;
            }

            unspecifiedExtendedDateTime.Day = new DateTimeValue(components[2], DateTimeValue.ValueFlags.Exact);

            return unspecifiedExtendedDateTime;
        }
    }
}