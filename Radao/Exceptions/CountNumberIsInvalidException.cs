namespace Radao.Exceptions
{
    public class CountNumberIsInvalidException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CountNumberIsInvalidException() : base("Invalid count number input.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public CountNumberIsInvalidException(string message) : base(message) { }
    }
}
