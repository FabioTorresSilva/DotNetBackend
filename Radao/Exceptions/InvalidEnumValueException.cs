namespace Radao.Exceptions
{
    public class InvalidEnumValueException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidEnumValueException() : base("Passed Susceptibility Index is not valid.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidEnumValueException(string message) : base(message) { }
    }
}
