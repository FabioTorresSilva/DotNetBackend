using Microsoft.AspNetCore.Mvc;
using Radao.Dtos;
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
            catch (DeviceAlreadyAssigned e)
            {
                return BadRequest(e.Message);
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

        /// <summary>
        /// Endpoint that links a device to a fountain
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpPost("{fountainId}/device")]
        public async Task<IActionResult> AddContinuousUseDeviceToFountain(int fountainId, [FromBody] int deviceId)
        {
            try
            {
                // Call the service method to assign the device to the fountain.
                var fountain = await _fountainService.AddContinuousUseDeviceToFountainAsync(fountainId, deviceId);

                // Map the updated fountain to the DTO for the response.
                var resultDto = _fountainMapper.FountainToFullDto(fountain);

                return Ok(resultDto);
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
            catch (DeviceAlreadyAssigned e)
            {
                return BadRequest(e.Message);
            }
            catch (FountainAlreadyAssigned e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Removes a Device link to a fountain
        /// </summary>
        /// <param name="fountainId"></param>
        /// <returns></returns>
        [HttpDelete("{fountainId}/device")]
        public async Task<IActionResult> RemoveDeviceFromFountain(int fountainId)
        {
            try
            {
                // Call the service method to remove the device from the specified fountain.
                var updatedFountain = await _fountainService.RemoveDeviceFromFountainAsync(fountainId);

                // Map the updated fountain to the DTO.
                var resultDto = _fountainMapper.FountainToFullDto(updatedFountain);

                return Ok(resultDto);
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Finds fountains with given description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetFountainsByDescription([FromQuery] string description)
        {
            try
            {
                // Call the service to get fountains by description
                var fountains = await _fountainService.GetFountainsByDescriptionAsync(description);

                // Map the result to DTOs
                var fountainsDto = fountains.Select(f => _fountainMapper.FountainToFullDto(f));

                // Return the results
                return Ok(fountainsDto);
            }
            catch (DbSetNotInitialize e)
            {
                return StatusCode(500, e.Message);
            }
            catch (ParamIsNull e)
            {
                return BadRequest(e.Message);
            }
            catch (NoFountainMatchesDescription e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Deletes a fountain
        /// </summary>
        /// <param name="fountainId"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFountain(int fountainId)
        {
            try
            {
                // Finds a fountain by id and deletes it
                await _fountainService.DeleteFountainAsync(fountainId);

                return Ok(new { message = "Fountain deleted successfully." });
            }
            catch (ParamIsNull e)
            {
                return BadRequest(e.Message);
            }
            catch (ObjIsNull e)
            {
                return NotFound(e.Message);
            }
            catch (DbSetNotInitialize e)
            {
                return StatusCode(500, e.Message);
            }
        }



    }
}
