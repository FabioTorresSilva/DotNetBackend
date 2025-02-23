namespace Radao.Exceptions
{
    public class InvalidRadonValue : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidRadonValue() : base("Invalid Radon value.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidRadonValue(string message) : base(message) { }
    }
}

