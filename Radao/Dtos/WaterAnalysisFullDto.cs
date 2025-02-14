using Radao.Models;
using System.ComponentModel.DataAnnotations;

namespace Radao.Dtos
{
    /// <summary>
    /// Class that represents an WaterAnalysis data transfer object with all of the arguments of the model
    /// </summary>
    public class WaterAnalysisFullDto
    {

        /// <summary>
        /// Gets or sets the radon concentration level measured during the water analysis.
        /// </summary>
        public double RadonConcentration { get; set; }


        /// <summary>
        /// Gets or sets the date when the water analysis was conducted.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the device used in the water analysis.
        /// </summary>
        public int DeviceId { get; set; }
    }
}
