namespace Radao.Exceptions
{
    public class DeviceSerialNumberAlreadyExists : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DeviceSerialNumberAlreadyExists() : base("Device SerialNumber already associated") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public DeviceSerialNumberAlreadyExists(string message) : base(message) { }
    }
}
