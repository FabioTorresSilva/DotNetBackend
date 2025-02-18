using Radao.Data;
using Radao.Enums;
using Radao.Exceptions;
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
            var device = _context.Devices.FirstOrDefault(c => c.Id == fountainFull.ContinuousUseDeviceId);
            if (device == null)
                throw new ObjIsNull();

            // Check if the fountain already exists
            var existingFountain = _context.Fountains.FirstOrDefault(f => f.Latitude == fountainFull.Latitude && f.Longitude == fountainFull.Longitude);

            // Check if the fountain already exists
            if (existingFountain != null)
                throw new FountainAlreadyExists();

            // If a Fountain already has an associated device, prevent adding a new one
            if (fountainFull.ContinuousUseDeviceId != null)
            {
                // Checks if the device exists in db 
                var existingDevice = _context.Devices.FirstOrDefault(d => d.Id == fountainFull.ContinuousUseDeviceId);
                if (existingDevice != null)
                    throw new DeviceAlreadyAssigned();
            }

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
        public async Task<Fountain> GetFountainByIdAsync(int id)
        {
            // Check if the id is valid
            if (id <= 0)
                throw new ParamIsNull();

            // Check if the fountain exists
            var fountain = await _context.Fountains.FirstOrDefaultAsync(f => f.Id == id);

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
        public async Task<Fountain> UpdateFountainAsync(Fountain updatedfountain)
        {
            // Ensure database exists
            if (_context.Fountains == null)
                throw new DbSetNotInitialize();

            // Check if the fountain is null
            if (updatedfountain == null)
                throw new ParamIsNull();

            // Find the existing fountain
            var fountain = await _context.Fountains.FindAsync(updatedfountain.Id);

            // Ensure the fountain exists
            if (fountain == null)
                throw new ObjIsNull();

            // Update the fountain
            fountain.Description = updatedfountain.Description;
            fountain.SusceptibilityIndex = updatedfountain.SusceptibilityIndex;
            fountain.ContinuousUseDeviceId = updatedfountain.ContinuousUseDeviceId;
            fountain.IsDrinkable = updatedfountain.IsDrinkable;
            fountain.Latitude = updatedfountain.Latitude;
            fountain.Longitude = updatedfountain.Longitude;

            // Check if DeviceId exists before updating
            if (updatedfountain.ContinuousUseDeviceId != null)
            {
                var continuousUseDevice = await _context.ContinuousUseDevices.SingleOrDefaultAsync(d => d.Id == updatedfountain.ContinuousUseDeviceId);

                if (continuousUseDevice == null)
                    throw new ObjIsNull(); // Device does not exist

                // If Fountain has a navigation property to Device, update it
                fountain.ContinuousUseDevice = continuousUseDevice;
            }

            // Save changes
            await _context.SaveChangesAsync();

            return fountain;
        }

        /// <summary>
        /// Links ContinuousUseDevice to a fountain
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="continuousUseDeviceId"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ObjIsNull"></exception>
        /// <exception cref="DeviceAlreadyAssigned"></exception>
        /// <exception cref="FountainAlreadyAssigned"></exception>
        public async Task<Fountain> AddContinuousUseDeviceToFountainAsync(int fountainId, int continuousUseDeviceId)
        {
            // Ensure required DbSets are initialized.
            if (_context.Fountains == null || _context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Retrieve the fountain by its Id.
            var fountain = await _context.Fountains.SingleOrDefaultAsync(f => f.Id == fountainId);
            if (fountain == null)
                throw new ObjIsNull();

            // Check if the fountain already has a device assigned.
            if (fountain.ContinuousUseDeviceId != null)
                throw new DeviceAlreadyAssigned();

            // Retrieve the device from the ContinuousUseDevices DbSet.
            var continuousUseDevice = await _context.ContinuousUseDevices.SingleOrDefaultAsync(d => d.Id == continuousUseDeviceId);
            if (continuousUseDevice == null)
                throw new ObjIsNull();

            // Check if the device is already associated with another fountain.
            if (continuousUseDevice.FountainId != null)
                throw new FountainAlreadyAssigned();

            // Assign the device to the fountain.
            fountain.ContinuousUseDeviceId = continuousUseDeviceId;
            fountain.ContinuousUseDevice = continuousUseDevice;

            // Assign the fountain to the device
            continuousUseDevice.FountainId = fountainId;
            continuousUseDevice.Fountain = fountain;

            // Persist the changes to the database.
            await _context.SaveChangesAsync();

            return fountain;
        }

        /// <summary>
        /// Removes the device associated with a fountain
        /// </summary>
        /// <param name="fountainId"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ObjIsNull"></exception>
        /// <exception cref="NoDeviceAssignedToFountain"></exception>
        /// <exception cref="FountainDeviceNotFound"></exception>
        public async Task<Fountain> RemoveDeviceFromFountainAsync(int fountainId)
        {
            // Ensure the required DbSets are initialized.
            if (_context.Fountains == null || _context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Retrieve the fountain by its Id.
            var fountain = await _context.Fountains.SingleOrDefaultAsync(f => f.Id == fountainId);
            if (fountain == null)
                throw new ObjIsNull();

            // Check if the fountain has a continuousUseDevice assigned.
            if (fountain.ContinuousUseDeviceId == null)
                throw new NoDeviceAssignedToFountain();

            // Retrieve the device associated with the fountain.
            var continuousUseDevice = await _context.ContinuousUseDevices.SingleOrDefaultAsync(d => d.Id == fountain.ContinuousUseDeviceId);
            if (continuousUseDevice == null)
                throw new FountainDeviceNotFound();

            // Clear the continuousUseDevice association from the fountain.
            fountain.ContinuousUseDeviceId = null;
            fountain.ContinuousUseDevice = null;

            // Clear the fountain association from the continuousUseDevice.
            continuousUseDevice.FountainId = null;
            continuousUseDevice.Fountain = null;

            // Persist changes to the database.
            await _context.SaveChangesAsync();

            return fountain;
        }

        /// <summary>
        /// Gets a List of Fountains with by description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<List<Fountain>> GetFountainsByDescriptionAsync(string description)
        {
            // Ensure the DbSet is initialized.
            if (_context.Fountains == null)
                throw new DbSetNotInitialize();

            // Validate the input.
            if (string.IsNullOrWhiteSpace(description))
                throw new ParamIsNull();

            // Retrieve all fountains where the description contains the provided text.
            var fountains = await _context.Fountains
                .Where(f => f.Description.Contains(description))
                .ToListAsync();

            // Optionally, throw an exception if no fountains are found, or return an empty list.
            if (fountains == null || fountains.Count == 0)
                throw new NoFountainMatchesDescription();

            return fountains;
        }
        public async Task DeleteFountainAsync(int id)
        {
            // Ensure the DbSet is initialized
            if (_context.Fountains == null)
                throw new DbSetNotInitialize();

            // Validate the input
            if (id <= 0)
                throw new ParamIsNull();

            // Find the fountain by ID
            var fountain = await _context.Fountains.FindAsync(id);

            // Check if the fountain exists
            if (fountain == null)
                throw new ObjIsNull();

            // Remove the fountain
            _context.Fountains.Remove(fountain);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates SuscepibilityIndex Of a fountain
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="newIndex"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<Fountain> UpdateFountainSusceptibilityAsync(int fountainId, SusceptibilityIndex newIndex)
        {
            // Ensure the DbSet is initialized.
            if (_context.Fountains == null)
                throw new DbSetNotInitialize();

            // Validate the input.
            if (fountainId <= 0)
                throw new ParamIsNull();

            // Explicitly check if newIndex is a valid enum value
            if (!Enum.IsDefined(typeof(SusceptibilityIndex), newIndex))
                throw new InvalidEnumValueException();

            // Retrieve the fountain.
            var fountain = await _context.Fountains.FindAsync(fountainId);
            if (fountain == null)
                throw new ObjIsNull();

            // Update the susceptibility index.
            fountain.SusceptibilityIndex = newIndex;

            // Save changes to the database.
            _context.Fountains.Update(fountain);
            await _context.SaveChangesAsync();

            return fountain;
        }

        /// <summary>
        /// Switchs the current Continuous use device to a different one
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="newDeviceId"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<Fountain> UpdateFountainContinuousUseDeviceAsync(int fountainId, int newContinuousUseDeviceId)
        {
            // Ensure the DbSet is initialized.
            if (_context.Fountains == null || _context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Validate the input.
            if (fountainId <= 0 || newContinuousUseDeviceId <= 0)
                throw new ParamIsNull();

            // Retrieve the fountain with its related ContinuousUseDevice.
            var fountain = await _context.Fountains
                .Include(f => f.ContinuousUseDevice) // Ensure to load the related ContinuousUseDevice
                .FirstOrDefaultAsync(f => f.Id == fountainId);

            if (fountain == null)
                throw new ObjIsNull();

            // Retrieve the new ContinuousUseDevice.
            var newContinuousUseDevice = await _context.ContinuousUseDevices.FindAsync(newContinuousUseDeviceId);
            if (newContinuousUseDevice == null)
                throw new ObjIsNull();

            // Swap the devices.
            fountain.ContinuousUseDevice = newContinuousUseDevice;
            fountain.ContinuousUseDeviceId = newContinuousUseDeviceId;
            newContinuousUseDevice.Fountain = fountain;
            newContinuousUseDevice.FountainId = fountainId;

            // Save changes to the database.
            _context.Fountains.Update(fountain);
            _context.ContinuousUseDevices.Update(newContinuousUseDevice);
            await _context.SaveChangesAsync();

            return fountain;
        }

        /// <summary>
        /// Gets the last x/all water analysis from a fountain
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<List<WaterAnalysis>> GetWaterAnalysisAsync(int fountainId, int? count = null)
        {
            // Ensure the DbSet is initialized.
            if (_context.WaterAnalysis == null || _context.Fountains == null)
                throw new DbSetNotInitialize();

            // Validate the input.
            if (fountainId <= 0)
                throw new ParamIsNull();

            // Base query to retrieve water analyses for the fountain.
            var query = _context.WaterAnalysis
                .Where(w => w.FountainId == fountainId)
                .OrderByDescending(w => w.Date);

            // Execute query, applying Take if count is provided
            var waterAnalyses = count.HasValue && count.Value > 0
                ? await query.Take(count.Value).ToListAsync()
                : await query.ToListAsync();

            // checks if theres a water analyse ou count 
            if (waterAnalyses == null || waterAnalyses.Count == 0)
                throw new ObjIsNull();

            return waterAnalyses;
        }

        /// <summary>
        /// Adds a water analysis to a fountain.
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="waterAnalysis"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<WaterAnalysis> AddWaterAnalysisAsync(int fountainId, WaterAnalysis waterAnalysis)
        {
            // Ensure the DbSet is initialized.
            if (_context.WaterAnalysis == null || _context.Fountains == null)
                throw new DbSetNotInitialize();

            // Validate the input.
            if (fountainId <= 0 || waterAnalysis == null)
                throw new ParamIsNull();

            // Retrieve the fountain.
            var fountain = await _context.Fountains.FindAsync(fountainId);
            if (fountain == null)
                throw new ObjIsNull();

            if (waterAnalysis.DeviceId != null)
            {
                var device = await _context.ContinuousUseDevices.FindAsync(waterAnalysis.DeviceId);
                if (device.FountainId != fountainId)
                {
                    throw new AssociatedToAnotherFountain();
                }
            }
            
            // Assign the fountain ID to the new WaterAnalysis.
            waterAnalysis.FountainId = fountainId;

            // Add the new WaterAnalysis to the database.
            await _context.WaterAnalysis.AddAsync(waterAnalysis);
            await _context.SaveChangesAsync();

            return waterAnalysis;
        }

    }
}