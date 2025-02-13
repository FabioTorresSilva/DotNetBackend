using Radao.Data;
using Radao.Dtos;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Services
{
    public class FountainService : IFountainService
    {

        private readonly RadaoContext _context;

        public FountainService(RadaoContext context)
        {
            _context = context;
        }
        public Task<Fountain> AddFountainAsync(FountainFullDto fountainFullDto)
        {
            throw new NotImplementedException();
        }

        public Task<Fountain> GetFountainByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fountain>> GetFountainsdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Fountain> UpdateFountainAsync(FountainFullDto fountainFullDto)
        {
            throw new NotImplementedException();
        }
    }
}
