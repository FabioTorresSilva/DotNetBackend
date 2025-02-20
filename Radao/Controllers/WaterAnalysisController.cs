using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Radao.Dtos;
using Radao.Exceptions;
using Radao.Mapper;
using Radao.Models;
using Radao.Services.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Radao.Controllers
{
    /// <summary>
    /// Controller for WaterAnalysis operations.
    /// </summary>
    [ApiController]
    [Route("api/wateranalysis")]
    public class WaterAnalysisController : Controller
    {
        // Service Injection
        private readonly IWaterAnalysisService _waterAnalysisService;
        private readonly WaterAnalysisMapper _waterAnalysisMapper;

        /// <summary>
        /// Constructor Service Injection.
        /// </summary>
        /// <param name="waterAnalysisService"></param>
        public WaterAnalysisController(IWaterAnalysisService waterAnalysisService, WaterAnalysisMapper waterAnalysisMapper)
        {
            _waterAnalysisService = waterAnalysisService;
            _waterAnalysisMapper = waterAnalysisMapper;
        }

        /// <summary>
        /// Adds a new water analysis.
        /// </summary>
        /// <param name="waterAnalysisDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddWaterAnalysis([FromBody] WaterAnalysisFullDto waterAnalysisDto)
        {
            try
            {
                // Map the DTO to the domain model using the mapper
                var waterAnalysis = _waterAnalysisMapper.FullDtoToWaterAnalysis(waterAnalysisDto);

                // Add the water analysis
                var addedWaterAnalysis = await _waterAnalysisService.AddWaterAnalysisAsync(waterAnalysis);

                // Map the domain model to the DTO and return it
                var resultDto = _waterAnalysisMapper.WaterAnalysisToFullDto(addedWaterAnalysis);
                return Ok(addedWaterAnalysis);
            }
            catch (DbSetNotInitialize e)
            {
                return StatusCode(500, e.Message);
            }
            catch (ParamIsNull e)
            {
                return BadRequest(e.Message);
            }
            catch (ObjIsNull e)
            {
                               return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Gets a water analysis by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWaterAnalysisById(int id)
        {
            try
            {
                // Get the water analysis
                var waterAnalysis = await _waterAnalysisService.GetWaterAnalysisByIdAsync(id);

                // Map the domain model to the DTO and return it
                var resultDto = _waterAnalysisMapper.WaterAnalysisToFullDto(waterAnalysis);
                return Ok(resultDto);
            }
            catch (DbSetNotInitialize e)
            {
                return StatusCode(500, e.Message);
            }
            catch (ObjIsNull e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Gets all water analysis.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllWaterAnalysis()
        {
            try
            {
                // Get all water analysis
                var analysis = await _waterAnalysisService.GetWaterAnalysisAsync();

                // Map the domain model to the DTO and return it
                var analysisDto = analysis.Select(a => _waterAnalysisMapper.WaterAnalysisToFullDto(a));
                return Ok(analysisDto);
            }
            catch (DbSetNotInitialize e)
            {
                return StatusCode(500, e.Message);
            }
            catch (EmptyList e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Updates a water analysis.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="waterAnalysisDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWaterAnalysis(int id, [FromBody] WaterAnalysisIdDto waterAnalysisIdDto)
        {
            try
            {
                // Ensure the DTO has the correct Id from the route
                waterAnalysisIdDto.Id = id;

                // Map the DTO to the domain model using the mapper
                var updatedWaterAnalysis = _waterAnalysisMapper.IdDtoToWaterAnalysis(waterAnalysisIdDto);

                // Update the water analysis and return it
                var waterAnalysis = await _waterAnalysisService.UpdateWaterAnalysisAsync(updatedWaterAnalysis);
                var resultDto = _waterAnalysisMapper.WaterAnalysisToIdDto(waterAnalysis);

                return Ok(resultDto);
            }
            catch (DbSetNotInitialize e)
            {
                return StatusCode(500, e.Message);
            }
            catch (ObjIsNull e)
            {
                return NotFound(e.Message);
            }
            catch (ParamIsNull e)
            {
                return BadRequest(e.Message);
            }
        }

        

    }
}