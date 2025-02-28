﻿namespace Radao.Exceptions
{
    public class DeviceAlreadyAssigned : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DeviceAlreadyAssigned() : base("Theres another device already assigned.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public DeviceAlreadyAssigned(string message) : base(message) { }
    }
}
