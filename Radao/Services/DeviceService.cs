using Radao.Data;
using Radao.Exceptions;
using Radao.Models;
using Radao.Services.ServicesInterfaces;
using System.Data.Entity;

namespace Radao.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly RadaoContext _context;

        public DeviceService(RadaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds device in context
        /// </summary>
        /// <param Device="device"></param>
        /// <returns device></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        public async Task<Device> AddDeviceAsync(Device device)
        {
            // Ensure database exists
            if (_context.Devices == null)
                throw new DbSetNotInitialize();

            // Ensure device is not null
            if (device == null)
                throw new ParamIsNull();

            // Adds device to the database
            await _context.Devices.AddAsync(device);

            // Saves database changes
            await _context.SaveChangesAsync();

            return device;
        }

        /// <summary>
        /// Gets the device with Id equal to id
        /// </summary>
        /// <param name="id"></param>
        /// <returns Device with Id equal to id></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            // Ensure database exists
            if (_context.Devices == null)
                throw new DbSetNotInitialize();

            return await _context.Devices.SingleOrDefaultAsync(d => d.Id == id);
        }

        /// <summary>
        /// Gets all of the devices on the database
        /// </summary>
        /// <returns List of all Devices></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        public async Task<List<Device>> GetDevicesdAsync()
        {
            // Ensure database exists
            if (_context.Devices == null) 
                throw new DbSetNotInitialize();

            
            return await _context.Devices.ToListAsync();
        }

        /// <summary>
        /// Updates Device in context
        /// </summary>
        /// <param name="updatedDevice"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<Device> UpdateDeviceAsync(Device updatedDevice)
        {
            // Ensure database exists
            if (_context.Devices == null)
                throw new DbSetNotInitialize();

            // Ensure device is not null
            if (updatedDevice == null)
                throw new ParamIsNull();

            // Gets device with Id equal to the updatedDevice
            var device = await _context.Devices.SingleOrDefaultAsync(c => c.Id == updatedDevice.Id);

            // Ensures updatedDevice exists in the context
            if (device == null)
                throw new ObjIsNull();

            // Updates the device on the database
            device.Description = updatedDevice.Description;
            device.ExpirationDate = updatedDevice.ExpirationDate;

            // Saves context changes
            await _context.SaveChangesAsync();

            return device;
        }
    }
}
