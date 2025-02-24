namespace Radao.Dtos
{
    public class UserFavoritesWaterAnalysisDto
    {

        /// <summary>
        /// Total number of water tests performed on the favorite fountains.
        /// </summary>
        public int TotalTests { get; set; }

        /// <summary>
        /// The lowest radon concentration recorded.
        /// </summary>
        public double LowestRadonValue { get; set; }

        /// <summary>
        /// The name or description of the fountain with the lowest radon value.
        /// </summary>
        public string LowestRadonFountain { get; set; }

        /// <summary>
        /// The highest radon concentration recorded.
        /// </summary>
        public double HighestRadonValue { get; set; }

        /// <summary>
        /// The name of the fountain with the highest radon value.
        /// </summary>
        public string HighestRadonFountain { get; set; }

        /// <summary>
        /// Percentage of tests indicating that the fountain water was drinkable.
        /// </summary>
        public double DrinkablePercentage { get; set; }
    }

}
