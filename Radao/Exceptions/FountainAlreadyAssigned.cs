namespace Radao.Exceptions
{
    public class FountainAlreadyAssigned : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FountainAlreadyAssigned() : base("Fountain Already Assigned to a device.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public FountainAlreadyAssigned(string message) : base(message) { }
    }
}
