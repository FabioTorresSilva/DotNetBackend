using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    /// <summary>
    /// Represents a water analysis at a specific fountain, including radon concentration, the associated device, and the date of the analysis.
    /// </summary>
    public class WaterAnalysis
    {
        /// <summary>
        /// Gets or sets the unique identifier for the water analysis record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the radon concentration level measured during the water analysis.
        /// This field is required.
        /// </summary>
        [Required]
        public double RadonConcentration { get; set; }

        /// <summary>
        /// Gets or sets the fountain associated with the water analysis.
        /// This field is required and establishes a relationship with the Fountain model.
        /// </summary>
        [Required]
        public Fountain Fountain { get; set; }



        /// <summary>
        /// Gets or sets the fountainId associated with the water analysis.
        /// This field is required.
        /// </summary>
        [Required]
        public int FountainId {  get; set; }

        /// <summary>
        /// Gets or sets the date when the water analysis was conducted.
        /// This field is required.
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the device used for the water analysis.
        /// This field is required and establishes a relationship with the Device model.
        /// </summary>
        [Required]
        public Device Device { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the device used in the water analysis.
        /// This field is required and links to the Device model.
        /// </summary>
        [Required]
        public int DeviceId { get; set; }
    }
}
