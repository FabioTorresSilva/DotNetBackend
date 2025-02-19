namespace Radao.Exceptions
{
    public class PassedFountainDoesntExist : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PassedFountainDoesntExist() : base("Passed Fountain doesnt exist.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public PassedFountainDoesntExist(string message) : base(message) { }
    }
}
