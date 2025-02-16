namespace Radao.Dtos
{
    public class WaterAnalysisIdDto
    {
        /// <summary>
        /// Gets or sets the id of the water analysis.
        /// </summary>
        public int Id { get; set; }
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
        /// Constructor for the WaterAnalysisIdDto class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="radonConcentration"></param>
        /// <param name="fountainId"></param>
        /// <param name="date"></param>
        /// <param name="deviceId"></param>
        public WaterAnalysisIdDto(int id, double radonConcentration, int fountainId, DateOnly date, int deviceId)
        {
            Id = id;
            RadonConcentration = radonConcentration;
            FountainId = fountainId;
            Date = date;
            DeviceId = deviceId;
        }
    }
}
