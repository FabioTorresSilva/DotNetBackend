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

            if (continuousUseDevice.AnalysisFrequency < 1)
            {
                throw new InvalidAnalysisFrequency();
            }

            // Checks if updatedContinuousUseDeviceIdDto.FountainId exists
            if (continuousUseDevice.FountainId != null && continuousUseDevice.Fountain == null)
            {
                // Gets Fountain with Id equal to the updatedContinuousUseDeviceIdDto.FountainId
                var fountain = _context.Fountains.SingleOrDefault(c => c.Id == continuousUseDevice.FountainId);

                // Ensures fountain is not null
                if (fountain == null)
                    throw new PassedFountainDoesntExist();

                if (fountain.ContinuousUseDeviceId != null)
                {
                    throw new FountainAlreadyAssigned();
                }

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

            // Ensure Id is valid
            if (id <= 0)
                throw new ParamIsNull();

            // Gets device with Id equal to the updatedDevice
            ContinuousUseDevice continuousUseDevice = _context.ContinuousUseDevices.SingleOrDefault(d => d.Id == id);

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
        public async Task<List<ContinuousUseDevice>> GetContinuousUseDevicesAsync()
        {
            // Ensure database exists
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Gets List of ContinuousUseDevices
            List<ContinuousUseDevice> continuousUseDevices = _context.ContinuousUseDevices.ToList();

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
        public async Task<ContinuousUseDevice> UpdateContinuousUseDeviceAsync(ContinuousUseDevice newDeviceData, ContinuousUseDevice existingDevice)
        {
            // Ensure database is initialized
            if (_context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Ensure new device data is not null
            if (newDeviceData == null)
                throw new ParamIsNull();

            // Check if the AnalysisFrequency is valid (adjust condition as needed)
            if (newDeviceData.AnalysisFrequency < 0)
            {
                throw new InvalidAnalysisFrequency();
            }

            // Update the existing device's properties
            existingDevice.Model = newDeviceData.Model;
            existingDevice.SerialNumber = newDeviceData.SerialNumber;
            existingDevice.ExpirationDate = newDeviceData.ExpirationDate;
            existingDevice.FountainId = newDeviceData.FountainId;
            existingDevice.AnalysisFrequency = newDeviceData.AnalysisFrequency;
            existingDevice.LastAnalysisDate = newDeviceData.LastAnalysisDate;

            // Update fountain association if needed
            if (newDeviceData.FountainId != null)
            {
                var fountain = _context.Fountains.SingleOrDefault(c => c.Id == newDeviceData.FountainId);
                if (fountain == null)
                    throw new ObjIsNull();

                existingDevice.Fountain = fountain;
            }

            // Save changes to the context
            await _context.SaveChangesAsync();

            return existingDevice;
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
            var device = _context.ContinuousUseDevices.Find(deviceId);
            if (device == null)
                throw new ObjIsNull();

            // Update periodicity
            device.AnalysisFrequency = newFrequency;

            // Save changes
            await _context.SaveChangesAsync();

            return device;
        }
    }
}
