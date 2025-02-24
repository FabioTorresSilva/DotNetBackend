using Radao.Dtos;
using Radao.Models;

namespace Radao.Services.ServicesInterfaces
{
    /// <summary>
    /// Interface that defines WaterAnalysis-related business operations.
    /// </summary>
    public interface IWaterAnalysisService
    {
        /// <summary>
        /// Creates a new WaterAnalysis.
        /// </summary>
        Task<WaterAnalysis> AddWaterAnalysisAsync(WaterAnalysis waterAnalysis);

        /// <summary>
        /// Updates a WaterAnalysis.
        /// </summary>
        Task<WaterAnalysis> UpdateWaterAnalysis(WaterAnalysis updatedWaterAnalysis);

        /// <summary>
        /// Gets a WaterAnalysis by id.
        /// </summary>
        Task<WaterAnalysis> GetWaterAnalysisById(int id);

        /// <summary>
        /// Gets the list of WaterAnalysis.
        /// </summary>
        Task<List<WaterAnalysis>> GetWaterAnalysis();

        /// <summary>
        /// Gets usefull stats using user favorites list
        /// </summary>
        /// <param name="favoriteFountains"></param>
        /// <returns></returns>
        Task<UserFavoritesWaterAnalysisDto> GetFavoriteFountainsAnalysis(List<Fountain> favoriteFountains);

    }
}
