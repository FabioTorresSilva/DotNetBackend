namespace Radao.Exceptions
{
    public class NoFountainMatchesDescription : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NoFountainMatchesDescription() : base("No Fountains Found With this Description.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public NoFountainMatchesDescription(string message) : base(message) { }
    }
}
