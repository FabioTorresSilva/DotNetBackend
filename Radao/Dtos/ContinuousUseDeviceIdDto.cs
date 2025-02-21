namespace Radao.Dtos
{
    public class ContinuousUseDeviceIdDto : DeviceIdDto
    {

        /// <summary>
        /// Gets or sets the foreign key for the fountain associated with the continuous use device.
        /// </summary>
        public int? FountainId { get; set; }

        /// <summary>
        /// Gets or sets the frequency of analysis for the continuous use device.
        /// </summary>
        public int AnalysisFrequency { get; set; }

        /// <summary>
        /// Gets or sets the date of the last analysis performed on the continuous use device.
        /// </summary>
        public DateOnly LastAnalysisDate { get; set; }

        /// <summary>
        /// All arguments Constructor
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="analysisFrequency"></param>
        /// <param name="lastAnalysisDate"></param>
        public ContinuousUseDeviceIdDto(int id, string model, string serialNumber, DateOnly expirationDate, int? fountainId, int analysisFrequency, DateOnly lastAnalysisDate)
            : base(id, model, serialNumber, expirationDate)
        {
            FountainId = fountainId;
            AnalysisFrequency = analysisFrequency;
            LastAnalysisDate = lastAnalysisDate;
        }
    }
}
