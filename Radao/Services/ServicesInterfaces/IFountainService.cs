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
    }
}
