namespace Radao.Exceptions
{
    public class FountainDeviceNotFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FountainDeviceNotFound() : base("Error finding Fountain Device.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public FountainDeviceNotFound(string message) : base(message) { }
    }
}
