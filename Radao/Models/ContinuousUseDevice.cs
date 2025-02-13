using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    /// <summary>
    /// Represents a continuous use device associated with a fountain, including analysis frequency and the last analysis date.
    /// </summary>
    public class ContinuousUseDevice
    {
        /// <summary>
        /// Gets or sets the fountain associated with the continuous use device.
        /// This field is required and establishes a relationship with the Fountain model.
        /// </summary>
        [Required]
        public Fountain Fountain { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the fountain associated with the continuous use device.
        /// This field is optional and links to the Fountain model.
        /// </summary>
        public int FountainId { get; set; }

        /// <summary>
        /// Gets or sets the frequency of analysis for the continuous use device.
        /// This field is required and specifies how often the analysis occurs.
        /// </summary>
        [Required]
        public int AnalysisFrequency { get; set; }

        /// <summary>
        /// Gets or sets the date of the last analysis performed on the continuous use device.
        /// This field is required.
        /// </summary>
        [Required]
        public DateOnly LastAnalysisDate { get; set; }
    }
}
