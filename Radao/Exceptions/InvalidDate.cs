namespace Radao.Exceptions
{
    public class InvalidDate: Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidDate() : base("Invalid Date.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidDate(string message) : base(message) { }
    }
}
