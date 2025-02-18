namespace Radao.Exceptions
{
    public class NoDeviceAssignedToFountain : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NoDeviceAssignedToFountain() : base("Theres no device Associated to this fountain") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public NoDeviceAssignedToFountain(string message) : base(message) { }
    }
}
