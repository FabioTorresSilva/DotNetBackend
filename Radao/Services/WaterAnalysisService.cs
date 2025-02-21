using Radao.Data;
using Radao.Dtos;
using Radao.Enums;
using Radao.Exceptions;
using Radao.Models;
using Radao.Services.ServicesInterfaces;
using System.Data.Entity;

namespace Radao.Services
{
    /// <summary>
    /// Service that defines WaterAnalysis-related business operations.
    /// </summary>
    public class WaterAnalysisService : IWaterAnalysisService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly RadaoContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterAnalysisService"/> class.
        /// </summary>
        /// <param name="context"></param>
        public WaterAnalysisService(RadaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new WaterAnalysis object to context
        /// </summary>
        /// <param name="waterAnalysis"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        public async Task<WaterAnalysis> AddWaterAnalysisAsync(WaterAnalysis waterAnalysis)
        {
            // Ensure database exists
            if (_context.WaterAnalysis == null)
                throw new DbSetNotInitialize();

            // Ensure waterAnalysis is not null
            if (waterAnalysis == null)
                throw new ParamIsNull();

            // Gets Fountain with Id equal to the updatedContinuousUseDeviceIdDto.FountainId
            var fountain = _context.Fountains.SingleOrDefault(c => c.Id == waterAnalysis.FountainId);

            // Ensures fountain is not null
            if (fountain == null)
                throw new ObjIsNull();

            // Updates the waterAnalysis.Fountain argument
            waterAnalysis.Fountain = fountain;

            // Gets device with Id equal to the updatedWaterAnalysis.DeviceId
            var device = _context.Devices.SingleOrDefault(c => c.Id == waterAnalysis.DeviceId);

            // Ensures device is not null
            if (device == null)
                throw new ObjIsNull();

            // Updates the updatedWaterAnalysis.Device argument
            waterAnalysis.Device = device;

            // Adds waterAnalysis to the database
            await _context.WaterAnalysis.AddAsync(waterAnalysis);

            // Updates SusceptibilityIndex on fountain
            if (waterAnalysis.RadonConcentration <= 50)
                fountain.SusceptibilityIndex = SusceptibilityIndex.Low;
            else if (waterAnalysis.RadonConcentration > 150)
                fountain.SusceptibilityIndex = SusceptibilityIndex.High;
            else
                fountain.SusceptibilityIndex = SusceptibilityIndex.Moderate;

            // Saves database changes
            await _context.SaveChangesAsync();

            return waterAnalysis;
        }

        /// <summary>
        /// Gets WaterAnalysis by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<WaterAnalysis> GetWaterAnalysisById(int id)
        {
            // Ensure database exists
            if (_context.WaterAnalysis == null)
                throw new DbSetNotInitialize();

            // Ensure Id is valid
            if (id <= 0)
                throw new ParamIsNull();

            // Gets device with Id equal to the updatedDevice
            WaterAnalysis waterAnalysis = _context.WaterAnalysis.SingleOrDefault(d => d.Id == id);

            // Ensures updatedDevice exists in the context
            if (waterAnalysis == null)
                throw new ObjIsNull();

            return waterAnalysis;
        }

        /// <summary>
        /// Gets list of all water analysis
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="EmptyList"></exception>
        public async Task<List<WaterAnalysis>> GetWaterAnalysis()
        {
            // Ensure database exists
            if (_context.WaterAnalysis == null)
                throw new DbSetNotInitialize();

            // Gets List of water analysises
            List<WaterAnalysis> waterAnalysises = _context.WaterAnalysis.ToList();

            // Ensures list is not empty
            if (waterAnalysises.Count == 0)
                throw new EmptyList();

            return waterAnalysises;
        }

        /// <summary>
        /// Updates a WaterAnalysis
        /// </summary>
        /// <param name="updatedWaterAnalysis"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="ObjIsNull"></exception>
        public async Task<WaterAnalysis> UpdateWaterAnalysis(WaterAnalysis updatedWaterAnalysis)
        {
            // Ensure database exists
            if (_context.WaterAnalysis == null)
                throw new DbSetNotInitialize();

            // Ensure updatedWaterAnalysis is not null
            if (updatedWaterAnalysis == null)
                throw new ParamIsNull();

            // Gets waterAnalysis with Id equal to the updatedWaterAnalysis
            var waterAnalysis = _context.WaterAnalysis.SingleOrDefault(c => c.Id == updatedWaterAnalysis.Id);

            // Ensures waterAnalysis is not null
            if (waterAnalysis == null)
                throw new ObjIsNull();

            // Updates the WaterAnalysis object on the database
            waterAnalysis.RadonConcentration = updatedWaterAnalysis.RadonConcentration;
            waterAnalysis.FountainId = updatedWaterAnalysis.FountainId;
            waterAnalysis.Date = updatedWaterAnalysis.Date;
            waterAnalysis.DeviceId = updatedWaterAnalysis.DeviceId;
            

            // Gets Fountain with Id equal to the updatedContinuousUseDeviceIdDto.FountainId
            var fountain = _context.Fountains.SingleOrDefault(c => c.Id == updatedWaterAnalysis.FountainId);

            // Ensures fountain is not null
            if (fountain == null)
               throw new ObjIsNull();

            // Updates the waterAnalysis.Fountain argument
            waterAnalysis.Fountain = fountain;

            // Gets device with Id equal to the updatedWaterAnalysis.DeviceId
            var device = _context.Devices.SingleOrDefault(c => c.Id == updatedWaterAnalysis.DeviceId);

            // Ensures device is not null
            if (device == null)
                throw new ObjIsNull();

            // Updates the updatedWaterAnalysis.Device argument
            waterAnalysis.Device = device;


            // Saves context changes
            await _context.SaveChangesAsync();

            return waterAnalysis;
        }
    }
}