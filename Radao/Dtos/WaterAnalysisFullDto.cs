﻿using Radao.Models;
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
        /// Gets or sets the foreign key for the fountain associated with the water analysis.
        /// </summary>
        public int FountainId { get; set; }

        /// <summary>
        /// Gets or sets the date when the water analysis was conducted.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the device used in the water analysis.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Constructor for the WaterAnalysisFullDto class
        /// </summary>
        /// <param name="radonConcentration"></param>
        /// <param name="fountainId"></param>
        /// <param name="date"></param>
        /// <param name="deviceId"></param>
        public WaterAnalysisFullDto(double radonConcentration, int fountainId, DateOnly date, int deviceId)
        {
            RadonConcentration = radonConcentration;
            FountainId = fountainId;
            Date = date;
            DeviceId = deviceId;
        }
    }
}
