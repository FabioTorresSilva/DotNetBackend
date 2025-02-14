using System.ComponentModel.DataAnnotations;

namespace Radao.Dtos
{
    /// <summary>
    /// Class that represents an Device data transfer object with all of the arguments of the model
    /// </summary>
    public class DeviceFullDto
    {

        /// <summary>
        /// Gets or sets the description of the device.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expiration date of the device.
        /// </summary>
        public DateOnly ExpirationDate { get; set; }
    }
}
