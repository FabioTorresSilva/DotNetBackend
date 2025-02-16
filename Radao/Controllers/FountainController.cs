using Microsoft.AspNetCore.Mvc;
using Radao.Dtos;
using Radao.Exceptions.Fountains;
using Radao.Exceptions;
using Radao.Models;
using Radao.Services.ServicesInterfaces;
using Radao.Mapper;

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
        private readonly FountainMapper _fountainMapper;

        /// <summary>
        /// Constructor Service Injection.
        /// </summary>
        /// <param name="fountainService"></param>
        public FountainController(IFountainService fountainService, FountainMapper fountainMapper)
        {
            _fountainService = fountainService;
            _fountainMapper = fountainMapper;
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
                var fountain = _fountainMapper.FullDtoToFountain(fountainDto);

                // Add the fountain
                var addedFountain = await _fountainService.AddFountainAsync(fountain);

                var resultDto = _fountainMapper.FountainToFullDto(addedFountain);

                // Map the domain model to the DTO and return it
                return CreatedAtAction(nameof(GetFountainById), new { id = addedFountain.Id }, resultDto);
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
                var resultDto = _fountainMapper.FountainToFullDto(fountain);
                return Ok(resultDto);
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
                var fountainsDto = fountains.Select(f => _fountainMapper.FountainToFullDto(f));
                return Ok(fountainsDto);
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
        public async Task<IActionResult> UpdateFountain(int id, [FromBody] FountainIdDto fountainIdDto)
        {
            try
            {
                // Ensure the id is valid
                fountainIdDto.Id = id;

                // Map the DTO to the domain model
                var updatedFountain = _fountainMapper.IdDtoToFountain(fountainIdDto);

                // Update the fountain
                var fountain = await _fountainService.UpdateFountainAsync(updatedFountain);
                var resultDto = _fountainMapper.FountainToIdDto(fountain);
                return Ok(resultDto);

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
