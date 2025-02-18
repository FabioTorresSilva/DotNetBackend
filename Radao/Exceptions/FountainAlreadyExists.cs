namespace Radao.Exceptions
{
    public class FountainAlreadyExists : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FountainAlreadyExists() : base("Fountain Already Exists.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public FountainAlreadyExists(string message) : base(message) { }
    }
}
