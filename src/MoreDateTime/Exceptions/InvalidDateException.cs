namespace MoreDateTime.Exceptions
{
    /// <summary>
    /// The invalid date exception.
    /// </summary>
    public class InvalidDateException : ParseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="invalidString">The invalid string.</param>
        public InvalidDateException(string message, string? invalidString) : base(message, invalidString)
        {
        }
    }

}
