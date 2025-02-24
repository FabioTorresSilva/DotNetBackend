using Microsoft.AspNetCore.Mvc;
using Radao.Dtos;
using Radao.Exceptions;
using Radao.Mapper;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Controllers
{
    [ApiController]
    [Route("api/device")]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly DeviceMapper _deviceMapper;

        /// <summary>
        /// Constructor Service Injection
        /// </summary>
        /// <param name="deviceService"></param>
        public DeviceController(IDeviceService deviceService, DeviceMapper deviceMapper )
        {
            _deviceService = deviceService;
            _deviceMapper = deviceMapper;
        }

        /// <summary>
        /// Adds a new device
        /// </summary>
        /// <param name="deviceDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] DeviceFullDto deviceDto)
        {
            try
            {
                // Map the DTO to the domain model
                var device = _deviceMapper.FullDtoToDevice(deviceDto);

                // Add the device
                var addedDevice = await _deviceService.AddDeviceAsync(device);

                // Map the domain model to the DTO and return it
                var resultDto = _deviceMapper.DeviceToIdDto(addedDevice);

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
            catch (DeviceAlreadyExists e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a device by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceById(int id)
        {
            try
            {
                // Check if the id is valid
                var device = await _deviceService.GetDeviceById(id);

                // Map the domain model to the DTO and return it
                var resultDto = _deviceMapper.DeviceToIdDto(device);

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
        /// Gets all devices
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            try
            {
                // Get all devices
                var devices = await _deviceService.GetDevices();

                // Map the domain models to the DTOs and return them
                var devicesDto = devices.Select(d => _deviceMapper.DeviceToIdDto(d));

                return Ok(devicesDto);
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
        /// Updates a device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] DeviceIdDto deviceIdDto)
        {
            try
            {
                //ensure the id in the URL matches the id in the body
                deviceIdDto.Id = id;

                // Map the DTO to the domain model
                var updatedDevice = _deviceMapper.IdDtoToDevice(deviceIdDto);

                // Map the domain model to the DTO and return it
                var device = await _deviceService.UpdateDevice(updatedDevice);
                var resultDto = _deviceMapper.DeviceToIdDto(device);
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