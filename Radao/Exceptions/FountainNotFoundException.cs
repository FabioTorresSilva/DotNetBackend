namespace Radao.Exceptions
{
    public class FountainNotFoundException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FountainNotFoundException() : base("The specified fountain does not exist.") { }
        
        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public FountainNotFoundException(string message) : base(message) { }
    }
}
