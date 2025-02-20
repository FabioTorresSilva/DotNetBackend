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
        Task<ContinuousUseDevice> AddContinuousUseDevice(ContinuousUseDevice continuousUseDevice);

        /// <summary>
        /// Updates a ContinuousUseDevice.
        /// </summary>
        Task<ContinuousUseDevice> UpdateContinuousUseDevice(ContinuousUseDevice updatedContinuousUseDevice);

        /// <summary>
        /// Gets a ContinuousUseDevice by id.
        /// </summary>
        Task<ContinuousUseDevice> GetContinuousUseDeviceById(int id);

        /// <summary>
        /// Gets the list of ContinuousUseDevice.
        /// </summary>
        Task<List<ContinuousUseDevice>> GetContinuousUseDevices();

        /// <summary>
        /// Updates de Analysis Frequency of a device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="newFrequency"></param>
        /// <returns></returns>
        Task<ContinuousUseDevice> UpdateDeviceAnalysisFrequencyAsync(int deviceId, int newFrequency);
    }
}
