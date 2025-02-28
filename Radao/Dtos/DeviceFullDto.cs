﻿using System.ComponentModel.DataAnnotations;

namespace Radao.Dtos
{
    /// <summary>
    /// Class that represents an Device data transfer object with all of the arguments of the model
    /// </summary>
    public class DeviceFullDto
    {
        public String Model { get; set; }

        /// <summary>
        /// Device Serial Number 
        /// </summary>
        public String SerialNumber { get; set; }

        /// <summary>
        /// Device Expiration Date
        /// </summary>
        public DateOnly ExpirationDate { get; set; }

        /// <summary>
        /// 3 Arguments DeviceFullDto constructor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="serialNumber"></param>
        /// <param name="expirationDate"></param>
        public DeviceFullDto(string model, string serialNumber, DateOnly expirationDate)
        {
            Model = model;
            SerialNumber = serialNumber;
            this.ExpirationDate = expirationDate;
        }
    }
}
