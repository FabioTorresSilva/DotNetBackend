using Radao.Data;
using Radao.Dtos;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Services
{
    /// <summary>
    /// Service that defines ContinuousUseDevice-related business operations.
    /// </summary>
    public class ContinuousUseDeviceService : IContinuousUseDeviceService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly RadaoContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinuousUseDeviceService"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ContinuousUseDeviceService(RadaoContext context)
        {
            _context = context;
        }

        public Task<ContinuousUseDevice> AddContinuousUseDeviceAsync(ContinuousUseDeviceFullDto continuousUseDeviceFullDto)
        {
            throw new NotImplementedException();
            //!!!! USE SERVICE THAT CHECKS IF THERES A FOUNTAIN USING THIS DEVICE
            //
            //
            //
            //
            //
            //
            //
            //
            //
            //
            //
            //
        }

        /// <summary>
        /// Gets ContinuousUseDevice by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ContinuousUseDevice> GetContinuousUseDeviceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Gets list of all ContinuousUseDevice
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<ContinuousUseDevice>> GetContinuousUseDevicesdAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a ContinuousUseDevice
        /// </summary>
        /// <param name="continuousUseDeviceFullDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ContinuousUseDevice> UpdateContinuousUseDeviceAsync(ContinuousUseDeviceFullDto continuousUseDeviceFullDto)
        {
            throw new NotImplementedException();
        }
    }
}
