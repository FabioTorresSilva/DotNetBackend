using Radao.Dtos;
using Radao.Models;

namespace Radao.Services.ServicesInterfaces
{
    /// <summary>
    /// Interface that defines Device-related business operations.
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Creates a new Device.
        /// </summary>
        Task<Device> AddDeviceAsync(Device device);

        /// <summary>
        /// Updates a Device.
        /// </summary>
        Task<Device> UpdateDevice(Device updatedDevice);

        /// <summary>
        /// Gets a Device by id.
        /// </summary>
        Task<Device> GetDeviceById(int id);

        /// <summary>
        /// Gets the list of Device.
        /// </summary>
        Task<List<Device>> GetDevices();
    }
}
