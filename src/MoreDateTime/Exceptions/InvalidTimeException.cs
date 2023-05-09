namespace MoreDateTime.Exceptions
{
    /// <summary>
    /// The invalid time exception.
    /// </summary>
    public class InvalidTimeException : ParseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTimeException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="invalidString">The invalid string.</param>
        public InvalidTimeException(string message, string? invalidString) : base(message, invalidString)
        {
        }
    }

}
