using Radao.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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
        public SusceptibilityIndex SusceptibilityIndex { get; set; }

        /// <summary>
        /// Gets or sets the list of water analysis records associated with the fountain.
        /// This field is required and establishes a relationship with the WaterAnalysis model.
        /// </summary>
        [Required]
        [JsonIgnore]
        public List<WaterAnalysis> WaterAnalysis { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ContinuousUseDevice associated with the fountain.
        /// This field is required and establishes a relationship with the Device model.
        /// </summary>
        
        [JsonIgnore]
        public ContinuousUseDevice? ContinuousUseDevice { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the ContinuousUseDevice associated with the fountain.
        /// This field is required and links to the ContinuousUseDevice model.
        /// </summary>

        public int? ContinuousUseDeviceId { get; set; }

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

        /// <summary>
        /// Constructor for the Fountain class without id
        /// </summary>
        /// <param name="description"></param>
        /// <param name="susceptibilityIndex"></param>
        /// <param name="continuousUseDeviceId"></param>
        /// <param name="isDrinkable"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public Fountain(string description, SusceptibilityIndex susceptibilityIndex, int? continuousUseDeviceId, bool isDrinkable, double longitude, double latitude)
        {
            Description = description;
            SusceptibilityIndex = susceptibilityIndex;
            ContinuousUseDeviceId = continuousUseDeviceId;
            IsDrinkable = isDrinkable;
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        /// Constructor for the Fountain class with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="susceptibilityIndex"></param>
        /// <param name="continuousUseDeviceId"></param>
        /// <param name="isDrinkable"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public Fountain(int id, string description, SusceptibilityIndex susceptibilityIndex, int? continuousUseDeviceId, bool isDrinkable, double longitude, double latitude)
        {
            this.Id = id;
            Description = description;
            SusceptibilityIndex = susceptibilityIndex;
            ContinuousUseDeviceId = continuousUseDeviceId;
            IsDrinkable = isDrinkable;
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        /// Default constructor with no args for userfav stats
        /// </summary>
        public Fountain()
        {}
    }
}
