namespace Radao.Exceptions
{
    public class DeviceNotFoundException : Exception
    {
        public DeviceNotFoundException() : base("The specified device does not exist.") { }

        public DeviceNotFoundException(string message) : base(message) { }
    }
}
