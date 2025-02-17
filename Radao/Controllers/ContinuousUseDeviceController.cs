using Microsoft.AspNetCore.Mvc;
using Radao.Dtos;
using Radao.Exceptions;
using Radao.Mapper;
using Radao.Services;
using Radao.Services.ServicesInterfaces;

namespace Radao.Controllers
{
    [ApiController]
    [Route("api/continuoususedevice")]
    public class ContinuousUseDeviceController : Controller
    {
        private readonly IContinuousUseDeviceService _continuousUseDeviceService;
        private readonly ContinuousUseDeviceMapper _continuousUseDeviceMapper;

        public ContinuousUseDeviceController(IContinuousUseDeviceService continuousUseDeviceService, ContinuousUseDeviceMapper continuousUseDeviceMapper)
        {
            _continuousUseDeviceService = continuousUseDeviceService;
            _continuousUseDeviceMapper = continuousUseDeviceMapper;
        }

        /// <summary>
        /// Adds a Continuous Use Device
        /// </summary>
        /// <param name="continuousUseDeviceDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddContinuousUseDevice([FromBody] ContinuousUseDeviceFullDto continuousUseDeviceDto)
        {
            try
            {
                // Map the DTO to the domain model
                var continuousUseDevice = _continuousUseDeviceMapper.FullDtoToContinuousUseDevice(continuousUseDeviceDto);

                //Add the continuous use device
                var addedContinuousUseDevice = await _continuousUseDeviceService.AddContinuousUseDeviceAsync(continuousUseDevice);

                //Map the domain model to the DTO and return it
                var resultDto = _continuousUseDeviceMapper.ContinuousUseDeviceToIdDto(addedContinuousUseDevice);

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
        }

        /// <summary>
        /// Gets a continuous use device by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContinuousUseDeviceById(int id)
        {
            try
            {
                // Check if the id is valid
                var continuousUseDevice = await _continuousUseDeviceService.GetContinuousUseDeviceByIdAsync(id);

                // Map the domain model to the DTO and return it
                var resultDto = _continuousUseDeviceMapper.ContinuousUseDeviceToFullDto(continuousUseDevice);

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
        /// Gets all continuous use devices
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetContinuousUseDevices()
        {
            try
            {
                // Get all continuous use devices
                var continuousUseDevices = await _continuousUseDeviceService.GetContinuousUseDevicesdAsync();

                // Map the domain models to the DTOs and return them
                var continuousUseDeviceDto = continuousUseDevices.Select(d => _continuousUseDeviceMapper.ContinuousUseDeviceToFullDto(d));

                return Ok(continuousUseDeviceDto);
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
        /// Updates a continuous use device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContinuousUseDevice(int id, [FromBody] ContinuousUseDeviceIdDto continuousUseDeviceIdDto)
        {
            try
            {
                //ensure the id in the URL matches the id in the body
                continuousUseDeviceIdDto.Id = id;

                // Map the DTO to the domain model
                var updatedContinuousUseDevice = _continuousUseDeviceMapper.IdDtoToContinuousUseDevice(continuousUseDeviceIdDto);

                // Map the domain model to the DTO and return it
                var continuousUseDevice = await _continuousUseDeviceService.UpdateContinuousUseDeviceAsync(updatedContinuousUseDevice);

                var resultDto = _continuousUseDeviceMapper.ContinuousUseDeviceToIdDto(continuousUseDevice);

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