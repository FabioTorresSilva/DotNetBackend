using Radao.Data;
using Radao.Dtos;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly RadaoContext _context;

        public DeviceService(RadaoContext context)
        {
            _context = context;
        }

        public Task<Device> AddDeviceAsync(DeviceFullDto deviceFullDto)
        {
            throw new NotImplementedException();
        }

        public Task<Device> GetDeviceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Device>> GetDevicesdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Device> UpdateDeviceAsync(DeviceFullDto deviceFullDto)
        {
            throw new NotImplementedException();
        }
    }
}
