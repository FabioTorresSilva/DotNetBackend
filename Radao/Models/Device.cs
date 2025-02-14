using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    /// <summary>
    /// Represents a device with a description and an expiration date.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the unique identifier for the device.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Model of the device.
        /// This field is required and can have a maximum length of 500 characters.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the SerialNumber of the device.
        /// </summary>
        [Required]
        public string SerialNumber { get; set; }
        /// <summary>
        /// Gets or sets the expiration date of the device.
        /// This field is required.
        /// </summary>
        [Required]
        public DateOnly ExpirationDate { get; set; }
    }
}
