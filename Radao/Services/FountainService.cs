using Radao.Data;
using Radao.Exceptions;
using Radao.Exceptions.Fountains;
using Radao.Models;
using Radao.Services.ServicesInterfaces;
using System.Data.Entity;

namespace Radao.Services
{
    /// <summary>
    /// Service that defines Fountain-related business operations.
    /// </summary>
    public class FountainService : IFountainService
    {

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly RadaoContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FountainService"/> class.
        /// </summary>
        /// <param name="context"></param>
        public FountainService(RadaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Fountain.
        /// </summary>
        /// <param name="fountainFull"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        /// <exception cref="FountainAlreadyExists"></exception>
        public async Task<Fountain> AddFountainAsync(Fountain fountainFull)
        {
            if (_context.Fountains == null)
                throw new DbSetNotInitialize();

            // Check if the fountain is null
            if (fountainFull == null)
                throw new ParamIsNull();

            // Check if the device is null
            var device = await _context.Devices.FindAsync(fountainFull.DeviceId);

            // Check if the device is null
            if (device == null)
                throw new ObjIsNull();

            // Check if the fountain already exists
            var existingFountain = await _context.Fountains.FirstOrDefaultAsync(f => f.Latitude == fountainFull.Latitude && f.Longitude == fountainFull.Longitude);

            // Check if the fountain already exists
            if (existingFountain != null)
                throw new FountainAlreadyExists();

            //USE SERVICE THAT CHECKS IF THERES A DEVICE IN THIS FOUNTAIN
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

            // Add the fountain to the database
            await _context.Fountains.AddAsync(fountainFull);
            await _context.SaveChangesAsync();

            return fountainFull;
        }

        /// <summary>
        /// Gets a Fountain by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public Task<Fountain> GetFountainByIdAsync(int id)
        {
            // Check if the id is valid
            if (id <= 0)
                throw new ParamIsNull();

            // Check if the fountain exists
            var fountain = _context.Fountains.FirstOrDefaultAsync(f => f.Id == id);

            // Ensure the fountain exists
            if (fountain == null)
                throw new ObjIsNull();

            return fountain;
        }

        /// <summary>
        /// Gets the list of Fountains.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<List<Fountain>> GetFountainsAsync()
        {
            // Get the list of fountains
            var fountains = await _context.Fountains.ToListAsync();

            //Ensure the fountains exist
            if (fountains == null)
                throw new ObjIsNull();

            return fountains;
        }

        /// <summary>
        /// Updates a Fountain.
        /// </summary>
        /// <param name="fountainFull"></param>
        /// <returns></returns>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<Fountain> UpdateFountainAsync(Fountain fountainFull)
        {
            // Check if the fountain is null
            if (fountainFull == null)
                throw new ParamIsNull();

            // Find the existing fountain
            var fountain = await _context.Fountains.FindAsync(fountainFull.Id);

            // Ensure the fountain exists
            if (fountain == null)
                throw new ObjIsNull();

            // Update the fountain
            fountain.Description = fountainFull.Description;
            fountain.SusceptibilityIndex = fountainFull.SusceptibilityIndex;
            fountain.WaterAnalysis = fountainFull.WaterAnalysis;
            fountain.Device = fountainFull.Device;
            fountain.DeviceId = fountainFull.DeviceId;
            fountain.IsDrinkable = fountainFull.IsDrinkable;
            fountain.Latitude = fountainFull.Latitude;
            fountain.Longitude = fountainFull.Longitude;

            // Save changes
            _context.Fountains.Update(fountain);
            await _context.SaveChangesAsync();

            return fountain;
        }
    }
}