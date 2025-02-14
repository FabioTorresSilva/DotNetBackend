using Radao.Dtos;
using Radao.Models;

namespace Radao.Mapper
{
    public class DeviceMapper
    {
        /// <summary>
        /// Creates a Device using a DeviceFullDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Device FullDtoToDevice(DeviceFullDto dto)
        {
            return new Device(dto.Model, dto.SerialNumber, dto.ExpirationDate);
        }

        /// <summary>
        /// Creates a DeviceFullDto using a Device
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public DeviceFullDto DeviceToFullDto(Device device)
        {
            return new DeviceFullDto(device.Model, device.SerialNumber, device.ExpirationDate);
        }

        /// <summary>
        /// Creates a Device using a IdDtoToDevice
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Device IdDtoToDevice(DeviceIdDto dto)
        {
            return new Device(dto.Id, dto.Model, dto.SerialNumber, dto.ExpirationDate);
        }


        /// <summary>
        /// Creates a IdDtoToDevice using a Device
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public DeviceIdDto DeviceToIdDto(Device device)
        {
            return new DeviceIdDto(device.Id, device.Model, device.SerialNumber, device.ExpirationDate);
        }
    }
}
