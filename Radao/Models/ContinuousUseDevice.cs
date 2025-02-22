using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Radao.Models
{
    /// <summary>
    /// Represents a continuous use device associated with a fountain, including analysis frequency and the last analysis date.
    /// </summary>
    public class ContinuousUseDevice : Device
    {

        /// <summary>
        /// Gets or sets the fountain associated with the continuous use device.
        /// This field is required and establishes a relationship with the Fountain model.
        /// </summary>
        [JsonIgnore]
        public Fountain? Fountain { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the fountain associated with the continuous use device.
        /// This field is optional and links to the Fountain model.
        /// </summary>

        [ForeignKey("Fountain")]
        public int? FountainId { get; set; }

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

        /// <summary>
        /// No Id constructor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="serialNumber"></param>
        /// <param name="expirationDate"></param>
        /// <param name="fountain"></param>
        /// <param name="fountainId"></param>
        /// <param name="lastAnalysisDate"></param>
        public ContinuousUseDevice(string model, string serialNumber, DateOnly expirationDate, int? fountainId, int analysisFrequency, DateOnly lastAnalysisDate)
            : base(model, serialNumber, expirationDate)
        {
            FountainId = fountainId;
            LastAnalysisDate = lastAnalysisDate;
            AnalysisFrequency = analysisFrequency;
        }

        /// <summary>
        /// All arguments constructor 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="serialNumber"></param>
        /// <param name="expirationDate"></param>
        /// <param name="fountainId"></param>
        /// <param name="lastAnalysisDate"></param>
        public ContinuousUseDevice(int id, string model, string serialNumber, DateOnly expirationDate, int? fountainId, int analysisFrequency, DateOnly lastAnalysisDate)
            : base(id, model, serialNumber, expirationDate)
        {
            FountainId = fountainId;
            LastAnalysisDate = lastAnalysisDate;
            AnalysisFrequency = analysisFrequency;
        }
    }
}

