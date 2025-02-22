namespace Radao.Exceptions
{
    public class DeviceAlreadyExists : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DeviceAlreadyExists() : base("Theres serial number already exists in database.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public DeviceAlreadyExists(string message) : base(message) { }
    }
}

