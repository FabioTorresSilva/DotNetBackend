namespace Radao.Exceptions
{
    public class InvalidFrequency : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidFrequency() : base("Error. Invalid Frequency.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidFrequency(string message) : base(message) { }
    }
}
