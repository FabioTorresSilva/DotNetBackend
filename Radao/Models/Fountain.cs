using Radao.Enums;
using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    /// <summary>
    /// Represents a fountain with its description, susceptibility index, associated address, and various related properties.
    /// </summary>
    public class Fountain
    {
        /// <summary>
        /// Gets or sets the unique identifier for the fountain.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the fountain.
        /// This field is required and can have a maximum length of 500 characters.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the susceptibility index of the fountain.
        /// This field is required.
        /// </summary>
        [Required]
        public SusceptibilityIndex SusceptibilityIndex { get; set; }

        /// <summary>
        /// Gets or sets the list of water analysis records associated with the fountain.
        /// This field is required and establishes a relationship with the WaterAnalysis model.
        /// </summary>
        [Required]
        public List<WaterAnalysis> WaterAnalysis { get; set; } = null!;

        /// <summary>
        /// Gets or sets the device associated with the fountain.
        /// This field is required and establishes a relationship with the Device model.
        /// </summary>
        [Required]
        public Device? Device { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the device associated with the fountain.
        /// This field is required and links to the Device model.
        /// </summary>
        [Required]
        public int? DeviceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fountain's water is drinkable.
        /// This field is required.
        /// </summary>
        [Required]
        public bool IsDrinkable { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the fountain's location.
        /// This field is required.
        /// </summary>
        [Required]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the fountain's location.
        /// This field is required.
        /// </summary>
        [Required]
        public double Longitude { get; set; }
    }
}
