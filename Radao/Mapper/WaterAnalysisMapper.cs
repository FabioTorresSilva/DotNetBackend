using Radao.Dtos;
using Radao.Models;

namespace Radao.Mapper
{
    /// <summary>
    /// Maps WaterAnalysis
    /// </summary>
    public class WaterAnalysisMapper
    {
        /// <summary>
        /// Maps the WaterAnalysisFullDTO to the WaterAnalysis model
        /// </summary>
        /// <param name="WaterAnalisysDto"></param>
        /// <returns></returns>
        public WaterAnalysis FullDtoToWaterAnalysis(WaterAnalysisFullDto WaterAnalisysDto)
        {
            return new WaterAnalysis(WaterAnalisysDto.RadonConcentration, WaterAnalisysDto.FountainId, WaterAnalisysDto.Date, WaterAnalisysDto.DeviceId);
        }

        /// <summary>
        /// Maps the WaterAnalysisIdDTO to the WaterAnalysis model
        /// </summary>
        /// <param name="WaterAnalisysIdDto"></param>
        /// <returns></returns>
        public WaterAnalysis IdDtoToWaterAnalysis(WaterAnalysisIdDto WaterAnalisysIdDto)
        {
            return new WaterAnalysis(WaterAnalisysIdDto.Id , WaterAnalisysIdDto.RadonConcentration, WaterAnalisysIdDto.FountainId, WaterAnalisysIdDto.Date, WaterAnalisysIdDto.DeviceId);
        }

        /// <summary>
        /// Maps the WaterAnalysis model to the WaterAnalysisFullDTO
        /// </summary>
        /// <param name="WaterAnalisys"></param>
        /// <returns></returns>
        public WaterAnalysisFullDto WaterAnalysisToFullDto(WaterAnalysis WaterAnalisys)
        {
            return new WaterAnalysisFullDto(WaterAnalisys.RadonConcentration, WaterAnalisys.FountainId, WaterAnalisys.Date, WaterAnalisys.DeviceId);
        }

        /// <summary>
        /// Maps the WaterAnalysis model to the WaterAnalysisIdDTO
        /// </summary>
        /// <param name="WaterAnalisys"></param>
        /// <returns></returns>
        public WaterAnalysisIdDto WaterAnalysisToIdDto(WaterAnalysis WaterAnalisys)
        {
            return new WaterAnalysisIdDto(WaterAnalisys.Id, WaterAnalisys.RadonConcentration, WaterAnalisys.FountainId, WaterAnalisys.Date, WaterAnalisys.DeviceId);
        }

    }
}
