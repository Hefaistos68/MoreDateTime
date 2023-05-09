namespace MoreDateTime.Exceptions
{
    /// <summary>
    /// The parse exception.
    /// </summary>
    public class ParseException : Exception
    {
        private readonly string? source;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="invalidString">The invalid string.</param>
        public ParseException(string message, string? invalidString) : base(message)
        {
            source = invalidString;
        }

        /// <summary>
        /// Gets the invalid string.
        /// </summary>
        public string? InvalidString
        {
            get
            {
                return source;
            }
        }
    }
}
