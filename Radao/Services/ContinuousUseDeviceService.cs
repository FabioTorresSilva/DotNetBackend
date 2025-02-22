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

        public async Task<ContinuousUseDevice> AddContinuousUseDevice(ContinuousUseDevice continuousUseDevice)
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
                // Gets Fountain with Id equal to the updatedContinuousUseDeviceIdDto.FountainId
                var fountain = _context.Fountains.SingleOrDefault(c => c.Id == continuousUseDevice.FountainId);

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
        public async Task<ContinuousUseDevice> GetContinuousUseDeviceById(int id)
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Ensure Id is valid
            if (id <= 0)
                throw new ParamIsNull();

            // Gets device with Id equal to the updatedDevice
            ContinuousUseDevice continuousUseDevice = _context.ContinuousUseDevices.SingleOrDefault(d => d.Id == id);

            // Ensures updatedDevice exists in the context
            if (continuousUseDevice == null)
                throw new ContinuousUseDeviceNotFound();

            return continuousUseDevice;
        }

        /// <summary>
        ///  Gets list of all ContinuousUseDevice
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContinuousUseDevice>> GetContinuousUseDevices()
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Gets List of ContinuousUseDevices
            List<ContinuousUseDevice> continuousUseDevices = _context.ContinuousUseDevices.ToList();

            // Ensures list is not empty
            //if (continuousUseDevices.Count == 0)
              //  throw new EmptyList();

            return continuousUseDevices;
        }

        /// <summary>
        /// Updates a ContinuousUseDevice
        /// </summary>
        /// <param name="continuousUseDeviceFullDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ContinuousUseDevice> UpdateContinuousUseDevice(ContinuousUseDevice updatedContinuousUseDevice)
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Ensure updatedContinuousUseDeviceFullDto is not null
            if (updatedContinuousUseDevice == null)
                throw new ParamIsNull();

            // Gets ContinuousUseDevice with Id equal to the updatedContinuousUseDeviceFullDto
            var continuousUseDevice = _context.ContinuousUseDevices.SingleOrDefault(c => c.Id == updatedContinuousUseDevice.Id);

            // Ensures continuousUseDevice is not null
            if (continuousUseDevice == null)
                throw new ContinuousUseDeviceNotFound();

            // Updates the continuousUseDevice object
            continuousUseDevice.Model = updatedContinuousUseDevice.Model;
            continuousUseDevice.SerialNumber = updatedContinuousUseDevice.SerialNumber;
            continuousUseDevice.ExpirationDate = updatedContinuousUseDevice.ExpirationDate;
            continuousUseDevice.FountainId = updatedContinuousUseDevice.FountainId;
            continuousUseDevice.LastAnalysisDate = updatedContinuousUseDevice.LastAnalysisDate;

            // Checks if updatedContinuousUseDeviceIdDto.FountainId exists
            if (updatedContinuousUseDevice.FountainId != null)
            {
                // Gets Fountai with Id equal to the updatedContinuousUseDeviceIdDto.FountainId
                var fountain = _context.Fountains.SingleOrDefault(c => c.Id == updatedContinuousUseDevice.FountainId);

                // Ensures fountain is not null
                if (fountain == null)
                    throw new FountainNotFound();

                // Updates the continuousUseDevice.Fountain argument
                continuousUseDevice.Fountain = fountain;
            }

            // Saves context changes
            await _context.SaveChangesAsync();

            return continuousUseDevice;
        }

        /// <summary>
        /// Updates the continuous use device analysis Frequency
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="newFrequency"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<ContinuousUseDevice> UpdateDeviceAnalysisFrequencyAsync(int deviceId, int newFrequency)
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Validate input
            if (deviceId <= 0 || newFrequency <= 0)
                throw new ParamIsNull();

            // Find the device
            var device = await _context.ContinuousUseDevices.FindAsync(deviceId);
            if (device == null)
                throw new ContinuousUseDeviceNotFound();

            // Update periodicity
            device.AnalysisFrequency = newFrequency;

            // Save changes
            await _context.SaveChangesAsync();

            return device;
        }
    }
}
