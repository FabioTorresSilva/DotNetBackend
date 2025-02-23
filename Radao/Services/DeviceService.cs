using Radao.Data;
using Radao.Dtos;
using Radao.Exceptions;
using Radao.Models;
using Radao.Services.ServicesInterfaces;
using System.Data.Entity;

namespace Radao.Services
{
    /// <summary>
    /// Service that defines Device-related business operations.
    /// </summary>
    public class DeviceService : IDeviceService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly RadaoContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceService"/> class.
        /// </summary>
        /// <param name="context"></param>
        public DeviceService(RadaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new device to context
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        public async Task<Device> AddDeviceAsync(Device device)
        {
            // Ensure database exists
            if (_context.Devices == null)
                throw new DbSetNotInitialize();

            // Ensure device is not null
            if (device == null)
                throw new ParamIsNull();
            
            // Ensure Serial number doesnot exist
            var deviceExists = _context.Devices.SingleOrDefault(c => c.SerialNumber == device.SerialNumber);

            if (deviceExists != null )
                throw new DeviceAlreadyExists();

            // Adds device to the database
            await _context.Devices.AddAsync(device);

            // Saves database changes
            await _context.SaveChangesAsync();

            return device;
        }

        /// <summary>
        /// Get device from context with Id equal to id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<Device> GetDeviceById(int id)
        {
            // Ensure database exists
            if (_context.Devices == null)
                throw new DbSetNotInitialize();

            // Ensure Id is valid
            if( id <=  0 )
                throw new ParamIsNull();

            // Gets device with Id equal to the updatedDevice
            Device device = _context.Devices.SingleOrDefault(d => d.Id == id);

            // Ensures updatedDevice exists in the context
            if (device == null)
                throw new ObjIsNull();

            return device;
        }

        /// <summary>
        /// Gets all devices
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="EmptyList"></exception>
        public async Task<List<Device>> GetDevices()
        {
            // Ensure database exists
            if (_context.Devices == null) 
                throw new DbSetNotInitialize();

            // Gets List of devices
            List<Device> devices = _context.Devices.ToList();

            // Ensures list is not empty
            if (devices.Count == 0)
                throw new EmptyList();

            return devices;
        }

        /// <summary>
        /// Updates Device in context
        /// </summary>
        /// <param name="updatedDevice"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<Device> UpdateDevice(Device updatedDevice)
        {
            // Ensure database exists
            if (_context.Devices == null)
                throw new DbSetNotInitialize();

            // Ensure device is not null
            if (updatedDevice == null)
                throw new ParamIsNull();

            // Gets device with Id equal to the deviceIdDto
            var device = _context.Devices.SingleOrDefault(c => c.Id == updatedDevice.Id);

            // Ensures deviceIdDto exists in the context
            if (device == null)
                throw new ObjIsNull();

            // Ensure Serial number doesnot exist
            var deviceExists = _context.Devices.SingleOrDefault(c => c.SerialNumber == device.SerialNumber);

            if (deviceExists != null)
                throw new DeviceAlreadyExists();

            // Updates the device on the database
            device.Model = updatedDevice.Model;
            device.ExpirationDate = updatedDevice.ExpirationDate;
            device .SerialNumber = updatedDevice.SerialNumber;

            // Saves context changes
            await _context.SaveChangesAsync();

            return device;
        }
    }
}
