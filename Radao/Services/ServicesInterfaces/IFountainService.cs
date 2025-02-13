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
        /// Creates a new Device.
        /// </summary>
        Task<Fountain> AddFountainAsync(FountainFullDto fountainFullDto);

        /// <summary>
        /// Updates a Fountain.
        /// </summary>
        Task<Fountain> UpdateFountainAsync(FountainFullDto fountainFullDto);

        /// <summary>
        /// Gets a Fountain by id.
        /// </summary>
        Task<Fountain> GetFountainByIdAsync(int id);

        /// <summary>
        /// Gets the list of Fountain.
        /// </summary>
        Task<List<Fountain>> GetFountainsdAsync();
    }
}
