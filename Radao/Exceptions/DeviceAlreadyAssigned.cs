namespace Radao.Exceptions
{
    public class DeviceAlreadyAssigned : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DeviceAlreadyAssigned() : base("Device Already Assigned.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public DeviceAlreadyAssigned(string message) : base(message) { }
    }
}
