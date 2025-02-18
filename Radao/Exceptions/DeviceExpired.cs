namespace Radao.Exceptions
{
    public class DeviceExpired : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DeviceExpired() : base("Error adding device to fountain, Device expired.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public DeviceExpired(string message) : base(message) { }
    }
}
