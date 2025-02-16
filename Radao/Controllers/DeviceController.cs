using Microsoft.AspNetCore.Mvc;
using Radao.Dtos;
using Radao.Exceptions;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Controllers
{
    [ApiController]
    [Route("api/device")]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        /// <summary>
        /// Constructor Service Injection
        /// </summary>
        /// <param name="deviceService"></param>
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
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
                var device = new Device
                (
                    deviceDto.Model,
                    deviceDto.SerialNumber,
                    deviceDto.ExpirationDate
                );
                // Add the device
                var addedDevice = await _deviceService.AddDeviceAsync(device);
                // Map the domain model to the DTO and return it
                return CreatedAtAction(nameof(GetDeviceById), new { id = addedDevice.Id }, addedDevice);
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
                var device = await _deviceService.GetDeviceByIdAsync(id);
                // Map the domain model to the DTO and return it
                return Ok(new DeviceFullDto(device.Model, device.SerialNumber, device.ExpirationDate));
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
                var devices = await _deviceService.GetDevicesdAsync();
                // Map the domain models to the DTOs and return them
                var deviceDtos = devices.ConvertAll(d => new DeviceFullDto(d.Model, d.SerialNumber, d.ExpirationDate));
                return Ok(deviceDtos);
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
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] DeviceFullDto deviceDto)
        {
            try
            {
                // Map the DTO to the domain model
                var updatedDevice = new Device
                    (
                    id,
                    deviceDto.Model,
                    deviceDto.SerialNumber,
                    deviceDto.ExpirationDate
                    );

                // Update the device
                var device = await _deviceService.UpdateDeviceAsync(updatedDevice);
                // Map the domain model to the DTO and return it
                return Ok(new DeviceIdDto(device.Id, device.Model, device.SerialNumber, device.ExpirationDate));
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