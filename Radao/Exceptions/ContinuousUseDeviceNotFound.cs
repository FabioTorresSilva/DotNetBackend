namespace Radao.Exceptions
{
    public class ContinuousUseDeviceNotFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ContinuousUseDeviceNotFound() : base("Error. Continuous Use Device not found.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public ContinuousUseDeviceNotFound(string message) : base(message) { }
    }
}