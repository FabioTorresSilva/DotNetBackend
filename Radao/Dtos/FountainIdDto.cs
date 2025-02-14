using Radao.Enums;
using Radao.Models;
using System.ComponentModel.DataAnnotations;

namespace Radao.Dtos
{
    /// <summary>
    /// Class that represents an Fountain data transfer object with all of the arguments of the model
    /// </summary>
    public class FountainFullDto
    {

        /// <summary>
        /// Gets or sets the description of the fountain.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the susceptibility index of the fountain.
        /// </summary>
        public SusceptibilityIndex SusceptibilityIndex { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the device associated with the fountain.
        /// This field is required and links to the Device model.
        /// </summary>
        public int? DeviceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fountain's water is drinkable.
        /// </summary>
        public bool IsDrinkable { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the fountain's location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the fountain's location.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The constructor for the FountainFullDto class
        /// </summary>
        /// <param name="description"></param>
        /// <param name="susceptibilityIndex"></param>
        /// <param name="deviceId"></param>
        /// <param name="isDrinkable"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public FountainFullDto(string description, SusceptibilityIndex susceptibilityIndex, int? deviceId, bool isDrinkable, double longitude, double latitude)
        {
            Description = description;
            SusceptibilityIndex = susceptibilityIndex;
            DeviceId = deviceId;
            IsDrinkable = isDrinkable;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
