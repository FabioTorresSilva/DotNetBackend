using Radao.Data;
using Radao.Dtos;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Services
{
    public class WaterAnalysisService : IWaterAnalysisService
    {

        private readonly RadaoContext _context;

        public WaterAnalysisService(RadaoContext context)
        {
            _context = context;
        }

        public Task<WaterAnalysis> AddWaterAnalysisAsync(WaterAnalysisFullDto waterAnalysisFullDto)
        {
            throw new NotImplementedException();
        }

        public Task<WaterAnalysis> GetWaterAnalysisByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<WaterAnalysis>> GetWaterAnalysisesdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WaterAnalysis> UpdateWaterAnalysisAsync(WaterAnalysisFullDto waterAnalysisFullDto)
        {
            throw new NotImplementedException();
        }
    }
}
