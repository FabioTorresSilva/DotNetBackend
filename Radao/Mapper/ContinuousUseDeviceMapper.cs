using Radao.Dtos;
using Radao.Models;

namespace Radao.Mapper
{
    /// <summary>
    /// Maps ContinuousUseDevice
    /// </summary>
    public class ContinuousUseDeviceMapper
    {
        /// <summary>
        /// Maps the ContinuousUseDeviceFullDTO to the ContinuousUseDevice model
        /// </summary>
        /// <param name="continuousUseDeviceFullDto"></param>
        /// <returns></returns>
        public ContinuousUseDevice FullDtoToContinuousUseDevice(ContinuousUseDeviceFullDto continuousUseDeviceFullDto) 
        {
            return new ContinuousUseDevice(continuousUseDeviceFullDto.Model, continuousUseDeviceFullDto.SerialNumber, continuousUseDeviceFullDto.ExpirationDate, continuousUseDeviceFullDto.FountainId, continuousUseDeviceFullDto.LastAnalysisDate);
        }

        /// <summary>
        /// Maps the ContinuousUseDeviceIdDto to the ContinuousUseDevice model
        /// </summary>
        /// <param name="continuousUseDeviceIdDto"></param>
        /// <returns></returns>
        public ContinuousUseDevice IdDtoToContinuousUseDevice(ContinuousUseDeviceIdDto continuousUseDeviceIdDto)
        {
            return new ContinuousUseDevice(continuousUseDeviceIdDto.Id, continuousUseDeviceIdDto.Model, continuousUseDeviceIdDto.SerialNumber, continuousUseDeviceIdDto.ExpirationDate, continuousUseDeviceIdDto.FountainId, continuousUseDeviceIdDto.LastAnalysisDate);
        }

        /// <summary>
        /// Maps the ContinuousUseDevice to the ContinuousUseDeviceFullDto model
        /// </summary>
        /// <param name="continuousUseDevice"></param>
        /// <returns></returns>
        public ContinuousUseDeviceFullDto ContinuousUseDeviceToFullDto(ContinuousUseDevice continuousUseDevice)
        {
            return new ContinuousUseDeviceFullDto(continuousUseDevice.Model, continuousUseDevice.SerialNumber, continuousUseDevice.ExpirationDate, continuousUseDevice.FountainId, continuousUseDevice.AnalysisFrequency, continuousUseDevice.LastAnalysisDate);
        }

        /// <summary>
        /// Maps the ContinuousUseDevice to the ContinuousUseDeviceIdDto model
        /// </summary>
        /// <param name="continuousUseDevice"></param>
        /// <returns></returns>
        public ContinuousUseDeviceIdDto ContinuousUseDeviceToIdDto(ContinuousUseDevice continuousUseDevice)
        {
            return new ContinuousUseDeviceIdDto(continuousUseDevice.Id, continuousUseDevice.Model, continuousUseDevice.SerialNumber, continuousUseDevice.ExpirationDate, continuousUseDevice.FountainId, continuousUseDevice.AnalysisFrequency, continuousUseDevice.LastAnalysisDate);
        }
    }
}
