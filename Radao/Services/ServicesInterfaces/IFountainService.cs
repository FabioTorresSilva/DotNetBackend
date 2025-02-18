using Radao.Dtos;
using Radao.Models;

namespace Radao.Services.ServicesInterfaces
{
    /// <summary>
    /// Interface that defines Fountain-related business operations.
    /// </summary>
    public interface IFountainService
    {
        /// <summary>
        /// Creates a new Fountain.
        /// </summary>
        Task<Fountain> AddFountainAsync(Fountain fountainFull);

        /// <summary>
        /// Updates a Fountain.
        /// </summary>
        Task<Fountain> UpdateFountainAsync(Fountain fountain);

        /// <summary>
        /// Gets a Fountain by id.
        /// </summary>
        Task<Fountain> GetFountainByIdAsync(int id);

        /// <summary>
        /// Gets the list of Fountain.
        /// </summary>
        Task<List<Fountain>> GetFountainsAsync();

        /// <summary>
        /// Associates a ContinuousUseDevice To a Fountain
        /// </summary>
        /// <returns></returns>
        Task<Fountain> AddContinuousUseDeviceToFountainAsync(int fountainId, int deviceId);

        /// <summary>
        /// Removes the device associated with a fountain
        /// </summary>
        /// <param name="fountainId"></param>
        /// <returns></returns>
        Task<Fountain> RemoveDeviceFromFountainAsync(int fountainId);

        /// <summary>
        /// Lists fountains that match a description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<List<Fountain>> GetFountainsByDescriptionAsync(string description);
    }
}
