using Microsoft.EntityFrameworkCore;
using Radao.Data;
using Radao.Dtos;
using Radao.Exceptions;
using Radao.Models;
using Radao.Services.ServicesInterfaces;
using System.Data.Entity;

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

        public async Task<ContinuousUseDevice> AddContinuousUseDeviceAsync(ContinuousUseDevice continuousUseDevice)
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Ensure continuousUseDevice is not null
            if (continuousUseDevice == null)
                throw new ParamIsNull();

            // Checks if updatedContinuousUseDeviceIdDto.FountainId exists
            if (continuousUseDevice.FountainId != null && continuousUseDevice.Fountain == null)
            {
                // Gets Fountai with Id equal to the updatedContinuousUseDeviceIdDto.FountainId
                var fountain = await _context.Fountains.SingleOrDefaultAsync(c => c.Id == continuousUseDevice.FountainId);

                // Ensures fountain is not null
                if (fountain == null)
                    throw new ObjIsNull();

                // Updates the continuousUseDevice.Fountain argument
                continuousUseDevice.Fountain = fountain;
            }

            // Adds continuousUseDevice to the database
            await _context.ContinuousUseDevices.AddAsync(continuousUseDevice);

            // Saves database changes
            await _context.SaveChangesAsync();

            return continuousUseDevice;
        }

        /// <summary>
        /// Gets ContinuousUseDevice by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ContinuousUseDevice> GetContinuousUseDeviceByIdAsync(int id)
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Gets device with Id equal to the updatedDevice
            ContinuousUseDevice continuousUseDevice = await _context.ContinuousUseDevices.SingleOrDefaultAsync(d => d.Id == id);

            // Ensures updatedDevice exists in the context
            if (continuousUseDevice == null)
                throw new ObjIsNull();

            return continuousUseDevice;
        }

        /// <summary>
        ///  Gets list of all ContinuousUseDevice
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContinuousUseDevice>> GetContinuousUseDevicesdAsync()
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Gets List of ContinuousUseDevices
            List<ContinuousUseDevice> continuousUseDevices = await _context.ContinuousUseDevices.ToListAsync();

            // Ensures list is not empty
            if (continuousUseDevices.Count == 0)
                throw new EmptyList();

            return continuousUseDevices;
        }

        /// <summary>
        /// Updates a ContinuousUseDevice
        /// </summary>
        /// <param name="continuousUseDeviceFullDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ContinuousUseDevice> UpdateContinuousUseDeviceAsync(ContinuousUseDeviceIdDto updatedContinuousUseDeviceIdDto)
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Ensure updatedContinuousUseDeviceFullDto is not null
            if (updatedContinuousUseDeviceIdDto == null)
                throw new ParamIsNull();

            // Gets ContinuousUseDevice with Id equal to the updatedContinuousUseDeviceFullDto
            var continuousUseDevice = await _context.ContinuousUseDevices.SingleOrDefaultAsync(c => c.Id == updatedContinuousUseDeviceIdDto.Id);

            // Ensures continuousUseDevice is not null
            if (continuousUseDevice == null)
                throw new ObjIsNull();            

            // Updates the continuousUseDevice object
            continuousUseDevice.Model = updatedContinuousUseDeviceIdDto.Model;
            continuousUseDevice.SerialNumber = updatedContinuousUseDeviceIdDto.SerialNumber;
            continuousUseDevice.ExpirationDate = updatedContinuousUseDeviceIdDto.ExpirationDate;
            continuousUseDevice.FountainId = updatedContinuousUseDeviceIdDto.FountainId;
            continuousUseDevice.LastAnalysisDate = updatedContinuousUseDeviceIdDto.LastAnalysisDate;

            // Checks if updatedContinuousUseDeviceIdDto.FountainId exists
            if (updatedContinuousUseDeviceIdDto.FountainId != null)
            {
                // Gets Fountai with Id equal to the updatedContinuousUseDeviceIdDto.FountainId
                var fountain = await _context.Fountains.SingleOrDefaultAsync(c => c.Id == updatedContinuousUseDeviceIdDto.FountainId);

                // Ensures fountain is not null
                if (fountain == null)
                    throw new ObjIsNull();

                // Updates the continuousUseDevice.Fountain argument
                continuousUseDevice.Fountain = fountain;
            }

            // Saves context changes
            await _context.SaveChangesAsync();

            return continuousUseDevice;
        }
    }
}
