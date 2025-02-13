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
        Task<Device> AddDeviceAsync(DeviceFullDto deviceFullDto);

        /// <summary>
        /// Updates a Device.
        /// </summary>
        Task<Device> UpdateDeviceAsync(DeviceFullDto deviceFullDto);

        /// <summary>
        /// Gets a Device by id.
        /// </summary>
        Task<Device> GetDeviceByIdAsync(int id);

        /// <summary>
        /// Gets the list of Device.
        /// </summary>
        Task<List<Device>> GetDevicesdAsync();
    }
}
