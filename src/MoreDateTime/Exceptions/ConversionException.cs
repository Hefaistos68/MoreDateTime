namespace MoreDateTime.Exceptions
{
    /// <summary>
    /// The conversion exception.
    /// </summary>
    public class ConversionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/> class.
        /// </summary>
        /// <param name="message">The source.</param>
        public ConversionException(string message)
            : base(message)
        {
        }
    }
}