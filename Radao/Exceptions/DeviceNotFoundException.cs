namespace Radao.Exceptions
{
    public class DeviceNotFoundException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DeviceNotFoundException() : base("The specified device does not exist.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public DeviceNotFoundException(string message) : base(message) { }
    }
}
