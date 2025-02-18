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
        Task<WaterAnalysis> UpdateWaterAnalysisAsync(WaterAnalysis updatedWaterAnalysis);

        /// <summary>
        /// Gets a WaterAnalysis by id.
        /// </summary>
        Task<WaterAnalysis> GetWaterAnalysisByIdAsync(int id);

        /// <summary>
        /// Gets the list of WaterAnalysis.
        /// </summary>
        Task<List<WaterAnalysis>> GetWaterAnalysisAsync();
    }
}
