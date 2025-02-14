using Radao.Models;
using System.ComponentModel.DataAnnotations;

namespace Radao.Dtos
{
    /// <summary>
    /// Class that represents an ContinuousUseDevice data transfer object with all of the arguments of the model
    /// </summary>
    public class ContinuousUseDeviceFullDto
    {

        /// <summary>
        /// Gets or sets the foreign key for the fountain associated with the continuous use device.
        /// </summary>
        public int FountainId { get; set; }

        /// <summary>
        /// Gets or sets the frequency of analysis for the continuous use device.
        /// </summary>
        public int AnalysisFrequency { get; set; }

        /// <summary>
        /// Gets or sets the date of the last analysis performed on the continuous use device.
        /// </summary>
        public DateOnly LastAnalysisDate { get; set; }
    }
}
