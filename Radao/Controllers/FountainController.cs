using Microsoft.AspNetCore.Mvc;
using Radao.Dtos;
using Radao.Exceptions.Fountains;
using Radao.Exceptions;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Controllers
{
    /// <summary>
    /// Controller for managing fountains.
    /// </summary>
    [ApiController]
    [Route("api/fountains")]
    public class FountainController : Controller
    {
        private readonly IFountainService _fountainService;

        /// <summary>
        /// Constructor Service Injection.
        /// </summary>
        /// <param name="fountainService"></param>
        public FountainController(IFountainService fountainService)
        {
            _fountainService = fountainService;
        }

        /// <summary>
        /// Adds a new fountain.
        /// </summary>
        /// <param name="fountainDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFountain([FromBody] FountainFullDto fountainDto)
        {
            try
            {
                // Map the DTO to the domain model
                var fountain = new Fountain
                (
                    fountainDto.Description,
                    fountainDto.SusceptibilityIndex,
                    fountainDto.DeviceId,
                    fountainDto.IsDrinkable,
                    fountainDto.Latitude,
                    fountainDto.Longitude
                );
                // Add the fountain
                var addedFountain = await _fountainService.AddFountainAsync(fountain);
                // Map the domain model to the DTO and return it
                return CreatedAtAction(nameof(GetFountainById), new { id = addedFountain.Id }, addedFountain);
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
            catch (FountainAlreadyExists e)
            {
                return Conflict(e.Message);
            }
        }

        /// <summary>
        /// Gets a fountain by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFountainById(int id)
        {
            try
            {
                // Check if the id is valid
                var fountain = await _fountainService.GetFountainByIdAsync(id);
                // Map the domain model to the DTO and return it
                return Ok(new FountainFullDto(fountain.Description, fountain.SusceptibilityIndex, fountain.DeviceId, fountain.IsDrinkable, fountain.Latitude, fountain.Longitude));
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
        /// Gets all fountains.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFountains()
        {
            try
            {
                // Get all fountains
                var fountains = await _fountainService.GetFountainsAsync();
                // Map the domain model to the DTO and return it
                var fountainDtos = fountains.ConvertAll(f => new FountainFullDto(
                    f.Description,
                    f.SusceptibilityIndex,
                    f.DeviceId,
                    f.IsDrinkable,
                    f.Latitude,
                    f.Longitude));
                return Ok(fountainDtos);
            }
            catch (ObjIsNull e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Updates a fountain.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fountainDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFountain(int id, [FromBody] FountainFullDto fountainDto)
        {
            try
            {
                // Map the DTO to the domain model
                var updatedFountain = new Fountain
                (
                    id,
                    fountainDto.Description,
                    fountainDto.SusceptibilityIndex,
                    fountainDto.DeviceId,
                    fountainDto.IsDrinkable,
                    fountainDto.Latitude,
                    fountainDto.Longitude
                );
                // Update the fountain
                var fountain = await _fountainService.UpdateFountainAsync(updatedFountain);
                // Map the domain model to the DTO and return it
                return Ok(fountain);
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
    }
}
