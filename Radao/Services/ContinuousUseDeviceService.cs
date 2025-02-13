using Radao.Data;
using Radao.Dtos;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Services
{
    public class ContinuousUseDeviceService : IContinuousUseDeviceService
    {
        private readonly RadaoContext _context;

        public ContinuousUseDeviceService(RadaoContext context)
        {
            _context = context;
        }

        public Task<ContinuousUseDevice> AddContinuousUseDeviceAsync(ContinuousUseDeviceFullDto continuousUseDeviceFullDto)
        {
            throw new NotImplementedException();
        }

        public Task<ContinuousUseDevice> GetContinuousUseDeviceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContinuousUseDevice>> GetContinuousUseDevicesdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ContinuousUseDevice> UpdateContinuousUseDeviceAsync(ContinuousUseDeviceFullDto continuousUseDeviceFullDto)
        {
            throw new NotImplementedException();
        }
    }
}
