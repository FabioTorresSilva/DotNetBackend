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
            
            // Check if the fountain already exists
            var existingFountain = _context.Fountains.FirstOrDefault(f => f.Latitude == fountainFull.Latitude && f.Longitude == fountainFull.Longitude);

            // Check if the fountain already exists
            if (existingFountain != null)
                throw new FountainAlreadyExists();

            // Explicitly check if newIndex is a valid enum value
            if (!Enum.IsDefined(typeof(SusceptibilityIndex), fountainFull.SusceptibilityIndex))
                throw new InvalidEnumValueException();

            if (fountainFull.ContinuousUseDeviceId != null)
            {
                // Check if the device exists in the database
                var existingDevice = _context.ContinuousUseDevices.Find(fountainFull.ContinuousUseDeviceId);
                if (existingDevice == null)
                    throw new DeviceNotFoundException();

                // Check if the device already has a fountain 
                if (existingDevice.FountainId >= 0 && existingDevice.FountainId != fountainFull.Id)
                    throw new DeviceAlreadyAssigned("device already has a fountain");
                
                // Add the fountain
                await _context.Fountains.AddAsync(fountainFull);

                //Updates Device
                existingDevice.FountainId = fountainFull.Id;
                existingDevice.Fountain = fountainFull;
                await _context.SaveChangesAsync();

                return fountainFull;
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
            var fountain = _context.Fountains.FirstOrDefault(f => f.Id == id);

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
            var fountains =  _context.Fountains.ToList();

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
            var fountain = _context.Fountains.Find(updatedfountain.Id);

            // Ensure the fountain exists
            if (fountain == null)
                throw new ObjIsNull();

            // Explicitly check if newIndex is a valid enum value
            if (!Enum.IsDefined(typeof(SusceptibilityIndex), updatedfountain.SusceptibilityIndex))
                throw new InvalidEnumValueException();

            // Ensure location does not exist
            if(fountain.Latitude != updatedfountain.Latitude || fountain.Longitude != updatedfountain.Longitude)
            {
                Fountain locationExists = _context.Fountains.FirstOrDefault(f => f.Latitude == updatedfountain.Latitude && f.Longitude == updatedfountain.Longitude);
                if (locationExists != null)
                    throw new FountainAlreadyAssigned("Location already exists.");
            }

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
                var continuousUseDevice = _context.ContinuousUseDevices.SingleOrDefault(d => d.Id == updatedfountain.ContinuousUseDeviceId);

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
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FountainNotFoundException"></exception>
        /// <exception cref="DeviceAlreadyAssigned"></exception>
        /// <exception cref="DeviceNotFoundException"></exception>
        /// <exception cref="FountainAlreadyAssigned"></exception>
        public async Task<Fountain> AddContinuousUseDeviceToFountainAsync(int fountainId, int continuousUseDeviceId)
        {
            // Ensure that the DbSets are initialized.
            if (_context.Fountains == null || _context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Validate the input IDs.
            if (fountainId <= 0 || continuousUseDeviceId <= 0)
                throw new ArgumentOutOfRangeException("The IDs must be higher than zero.");

            // Retrieve the fountain asynchronously.
            var fountain = _context.Fountains.SingleOrDefault(f => f.Id == fountainId);
            if (fountain == null)
                throw new FountainNotFoundException($"Fountain with ID {fountainId} not found.");

            // Check if the fountain already has an assigned device.
            if (fountain.ContinuousUseDeviceId != null)
                throw new DeviceAlreadyAssigned();

            // Retrieve the continuous use device.
            var continuousUseDevice = _context.ContinuousUseDevices.SingleOrDefault(d => d.Id == continuousUseDeviceId);
            if (continuousUseDevice == null)
                throw new DeviceNotFoundException($"Device with ID {continuousUseDeviceId} not found.");

            // Check if the device is already associated with another fountain.
            if (continuousUseDevice.FountainId != null)
                throw new FountainAlreadyAssigned();

            // Assign the device to the fountain and vice versa.
            fountain.ContinuousUseDeviceId = continuousUseDeviceId;
            fountain.ContinuousUseDevice = continuousUseDevice;
            continuousUseDevice.FountainId = fountainId;
            continuousUseDevice.Fountain = fountain;

            // Save changes to the database asynchronously.
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
        public async Task<Fountain> RemoveContinuousUseDeviceFromFountainAsync(int fountainId)
        {
            // Ensure the required DbSets are initialized.
            if (_context.Fountains == null || _context.ContinuousUseDevices == null)
                throw new DbSetNotInitialize();

            // Retrieve the fountain by its Id.
            var fountain = _context.Fountains.SingleOrDefault(f => f.Id == fountainId);
            if (fountain == null)
                throw new ObjIsNull();

            // Check if the fountain has a continuousUseDevice assigned.
            if (fountain.ContinuousUseDeviceId == null)
                throw new NoDeviceAssignedToFountain();

            // Retrieve the device associated with the fountain.
            var continuousUseDevice = _context.ContinuousUseDevices.SingleOrDefault(d => d.Id == fountain.ContinuousUseDeviceId);
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
            var fountains = _context.Fountains
                .Where(f => f.Description.Contains(description))
                .ToList();

            // Optionally, throw an exception if no fountains are found, or return an empty list.
            if (fountains == null || fountains.Count == 0)
                throw new NoFountainMatchesDescription();

            return fountains;
        }
        public async Task DeleteFountainAsync(int id)
        {
            // Garantir que o DbSet esteja inicializado
            if (_context.Fountains == null)
                throw new DbSetNotInitialize();

            // Validar o parâmetro
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "The ID must be higher than zero.");

            // Buscar a fonte de forma assíncrona
            var fountain = _context.Fountains.Find(id);

            // Verificar se a fonte existe
            if (fountain == null)
                throw new ObjIsNull();

            // Remover a fonte
            _context.Fountains.Remove(fountain);

            // Salvar as alterações no banco de dados
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
            var fountain = _context.Fountains.Find(fountainId);
            if (fountain == null)
                throw new ObjIsNull();

            // Update the susceptibility index.
            fountain.SusceptibilityIndex = newIndex;

            // Save changes to the database.
            _context.Fountains.Update(fountain);
             _context.SaveChanges();

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
            var fountain = _context.Fountains
                .Include(f => f.ContinuousUseDevice) // Ensure to load the related ContinuousUseDevice
                .FirstOrDefault(f => f.Id == fountainId);

            if (fountain == null)
                throw new ObjIsNull();

            // Retrieve the new ContinuousUseDevice.
            var newContinuousUseDevice = await _context.ContinuousUseDevices.FindAsync(newContinuousUseDeviceId);
            if (newContinuousUseDevice == null)
                throw new ObjIsNull();

            // Ensures Device is not in use already
            if (newContinuousUseDevice.FountainId != null)
                throw new DeviceAlreadyAssigned("Device already assign to another fountain.");

            // Swap the devices.
            fountain.ContinuousUseDevice = newContinuousUseDevice;
            fountain.ContinuousUseDeviceId = newContinuousUseDeviceId;
            newContinuousUseDevice.Fountain = fountain;
            newContinuousUseDevice.FountainId = fountainId;

            // Save changes to the database.
            _context.Fountains.Update(fountain);
            _context.ContinuousUseDevices.Update(newContinuousUseDevice);
             _context.SaveChanges();

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

            if(count != null && count <= 0)
                throw new CountNumberIsInvalidException();

            // Base query to retrieve water analyses for the fountain.
            var query = _context.WaterAnalysis
                .Where(w => w.FountainId == fountainId)
                .OrderByDescending(w => w.Date);

            // Execute query, applying Take if count is provided
            var waterAnalyses = count.HasValue && count.Value > 0
                ? query.Take(count.Value).ToList()
                : query.ToList();

            // checks if theres a water analyse or count 
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
            var fountain =  _context.Fountains.Find(fountainId);
            if (fountain == null)
                throw new ObjIsNull();

            if (waterAnalysis.DeviceId <= 0)
            {
                throw new ParamIsNull();
            }

            // Ensure if it is a ContinuousUseDevice, it is not in use already
            var device = _context.ContinuousUseDevices.Find(waterAnalysis.DeviceId);
            
            if (device != null)
            {
                
                    if (device.FountainId != fountainId)
                    {
                        throw new AssociatedToAnotherFountain();
                    }
                
            }

            // Assign the fountain ID to the new WaterAnalysis.
            waterAnalysis.FountainId = fountainId;

            // Add the new WaterAnalysis to the database.
             _context.WaterAnalysis.Add(waterAnalysis);
             _context.SaveChangesAsync();

            return waterAnalysis;
        }

    }
}