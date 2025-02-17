using Radao.Data;
using Radao.Dtos;
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
            if (_context.Devices == null)
                throw new DbSetNotInitialize();

            // Ensure waterAnalysis is not null
            if (waterAnalysis == null)
                throw new ParamIsNull();

            //Use service to add device and fountain
            //
            //
            //
            //
            //
            //
            //
            //
            //

            // Adds waterAnalysis to the database
            await _context.WaterAnalysises.AddAsync(waterAnalysis);

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
        public async Task<WaterAnalysis> GetWaterAnalysisByIdAsync(int id)
        {
            // Ensure database exists
            if (_context.WaterAnalysises == null)
                throw new DbSetNotInitialize();

            // Gets device with Id equal to the updatedDevice
            WaterAnalysis waterAnalysis = await _context.WaterAnalysises.SingleOrDefaultAsync(d => d.Id == id);

            // Ensures updatedDevice exists in the context
            if (waterAnalysis == null)
                throw new ObjIsNull();

            return waterAnalysis;
        }

        /// <summary>
        /// Gets list of all water analysises
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="EmptyList"></exception>
        public async Task<List<WaterAnalysis>> GetWaterAnalysisesdAsync()
        {
            // Ensure database exists
            if (_context.WaterAnalysises == null)
                throw new DbSetNotInitialize();

            // Gets List of water analysises
            List<WaterAnalysis> waterAnalysises = await _context.WaterAnalysises.ToListAsync();

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
        public async Task<WaterAnalysis> UpdateWaterAnalysisAsync(WaterAnalysis updatedWaterAnalysis)
        {
            // Ensure database exists
            if (_context.WaterAnalysises == null)
                throw new DbSetNotInitialize();

            // Ensure updatedWaterAnalysis is not null
            if (updatedWaterAnalysis == null)
                throw new ParamIsNull();

            // Gets waterAnalysis with Id equal to the updatedWaterAnalysis
            var waterAnalysis = await _context.WaterAnalysises.SingleOrDefaultAsync(c => c.Id == updatedWaterAnalysis.Id);

            // Ensures waterAnalysis is not null
            if (waterAnalysis == null)
                throw new ObjIsNull();

            // Updates the WaterAnalysis object on the database
            waterAnalysis.RadonConcentration = updatedWaterAnalysis.RadonConcentration;
            waterAnalysis.Fountain = updatedWaterAnalysis.Fountain;
            waterAnalysis.FountainId = updatedWaterAnalysis.FountainId;
            waterAnalysis.Date = updatedWaterAnalysis.Date;
            waterAnalysis.Device = updatedWaterAnalysis.Device;
            waterAnalysis.DeviceId = updatedWaterAnalysis.DeviceId;

            // Saves context changes
            await _context.SaveChangesAsync();

            return waterAnalysis;
        }
    }
}










