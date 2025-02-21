namespace Radao.Exceptions
{
    public class FountainNotFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FountainNotFound() : base("Error. Fountain not found.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public FountainNotFound(string message) : base(message) { }
    }
}
