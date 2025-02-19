using Radao.Dtos;
using Radao.Models;

namespace Radao.Services.ServicesInterfaces
{
    /// <summary>
    /// Interface that defines ContinuousUseDevice-related business operations.
    /// </summary>
    public interface IContinuousUseDeviceService
    {
        /// <summary>
        /// Creates a new ContinuousUseDevice.
        /// </summary>
        Task<ContinuousUseDevice> AddContinuousUseDeviceAsync(ContinuousUseDevice continuousUseDevice);

        /// <summary>
        /// Updates a ContinuousUseDevice.
        /// </summary>
        Task<ContinuousUseDevice> UpdateContinuousUseDeviceAsync(ContinuousUseDevice updatedContinuousUseDevice , ContinuousUseDevice continuousUseDevice);

        /// <summary>
        /// Gets a ContinuousUseDevice by id.
        /// </summary>
        Task<ContinuousUseDevice> GetContinuousUseDeviceByIdAsync(int id);

        /// <summary>
        /// Gets the list of ContinuousUseDevice.
        /// </summary>
        Task<List<ContinuousUseDevice>> GetContinuousUseDevicesAsync();

        /// <summary>
        /// Updates de Analysis Frequency of a device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="newFrequency"></param>
        /// <returns></returns>
        Task<ContinuousUseDevice> UpdateDeviceAnalysisFrequencyAsync(int deviceId, int newFrequency);
    }
}
