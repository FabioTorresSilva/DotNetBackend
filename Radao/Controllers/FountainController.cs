using Microsoft.AspNetCore.Mvc;
using Radao.Dtos;
using Radao.Exceptions;
using Radao.Models;
using Radao.Services.ServicesInterfaces;
using Radao.Mapper;
using Radao.Enums;

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
        private readonly WaterAnalysisMapper _waterAnalysisMapper;


        /// <summary>
        /// Constructor Service Injection.
        /// </summary>
        /// <param name="fountainService"></param>
        public FountainController(IFountainService fountainService, FountainMapper fountainMapper, WaterAnalysisMapper waterAnalysisMapper)
        {
            _fountainService = fountainService;
            _fountainMapper = fountainMapper;
            _waterAnalysisMapper = waterAnalysisMapper;
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

        /// <summary>
        /// Updates the susceptibility index of a fountain.
        /// </summary>
        /// <param name="fountainId">The ID of the fountain.</param>
        /// <param name="newIndex">The new susceptibility index value.</param>
        /// <returns></returns>
        [HttpPut("{fountainId}/susceptibility")]
        public async Task<IActionResult> UpdateFountainSusceptibility(int fountainId, [FromBody] SusceptibilityIndex newIndex)
        {
            try
            {
                // Call the service to update the susceptibility index
                var updatedFountain = await _fountainService.UpdateFountainSusceptibilityAsync(fountainId, newIndex);

                // Map the updated fountain entity to a DTO
                var updatedFountainDto = _fountainMapper.FountainToFullDto(updatedFountain);

                // Return the updated fountain dto in the response
                return Ok(updatedFountainDto);
            }
            catch (ParamIsNull e)
            {
                return BadRequest(e.Message);
            }
            catch (ObjIsNull e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidEnumValueException e)
            {
                return BadRequest($"Invalid Susceptibility Index: {e.Message}");
            }
            catch (DbSetNotInitialize e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Updates the susceptibility index of a fountain.
        /// </summary>
        /// <param name="fountainId">The ID of the fountain.</param>
        /// <param name="newIndex">The new susceptibility index value.</param>
        /// <returns></returns>
        [HttpPut("{fountainId}/susceptibility")]
        public async Task<IActionResult> UpdateFountainSusceptibility(int fountainId, [FromBody] SusceptibilityIndex newIndex)
        {
            try
            {
                // Call the service to update the susceptibility index
                var updatedFountain = await _fountainService.UpdateFountainSusceptibilityAsync(fountainId, newIndex);

                // Map the updated fountain entity to a DTO
                var updatedFountainDto = _fountainMapper.FountainToFullDto(updatedFountain);

                // Return the updated fountain dto in the response
                return Ok(updatedFountainDto);
            }
            catch (ParamIsNull e)
            {
                return BadRequest(e.Message);
            }
            catch (ObjIsNull e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidEnumValueException e)
            {
                return BadRequest($"Invalid Susceptibility Index: {e.Message}");
            }
            catch (DbSetNotInitialize e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Switches A continuous use device of a fountain to another
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="newDeviceId"></param>
        /// <returns></returns>
        [HttpPut("{fountainId}/device/{newDeviceId}")]
        public async Task <IActionResult> UpdateFountainContinuousDevice(int fountainId, int newDeviceId)
        {
            try
            {
                // Call the service to update the ContinuousUseDevice of the fountain
                var updatedFountain = await _fountainService.UpdateFountainContinuousUseDeviceAsync(fountainId, newDeviceId);

                // Map the updated fountain entity to a full DTO
                var updatedFountainDto = _fountainMapper.FountainToFullDto(updatedFountain);

                // Return the updated fountain DTO
                return Ok(updatedFountainDto);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fountainId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("{fountainId}/water-analysis")]
        public async Task<IActionResult> GetWaterAnalysis(int fountainId, [FromQuery] int? count = null)
        {
            try
            {
                /*var waterAnalyses = await _fountainService.GetWaterAnalysisAsync(fountainId, count);*/
                /*  var waterAnalysesDto = waterAnalyses.Select(w => _waterAnalysisMapper.WaterAnalysisToFullDto(w));

                  return Ok(waterAnalysesDto);*/
                return Ok();
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

                // Return the updated fountain DTO
                return Ok(updatedFountainDto);
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
