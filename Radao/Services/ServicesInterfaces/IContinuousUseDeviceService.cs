﻿using Radao.Dtos;
using Radao.Models;

namespace Radao.Services.ServicesInterfaces
{
    /// <summary>
    /// Interface that defines ContinuousUseDevice-related business operations.
    /// </summary>
    public interface IContinuousUseDeviceService
    {
        /// <summary>
        /// Creates a new ContinuousUseDevice.
        /// </summary>
        Task<ContinuousUseDevice> AddContinuousUseDeviceAsync(ContinuousUseDevice continuousUseDevice);

        /// <summary>
        /// Updates a ContinuousUseDevice.
        /// </summary>
        Task<ContinuousUseDevice> UpdateContinuousUseDeviceAsync(ContinuousUseDeviceIdDto continuousUseDeviceIdDto);

        /// <summary>
        /// Gets a ContinuousUseDevice by id.
        /// </summary>
        Task<ContinuousUseDevice> GetContinuousUseDeviceByIdAsync(int id);

        /// <summary>
        /// Gets the list of ContinuousUseDevice.
        /// </summary>
        Task<List<ContinuousUseDevice>> GetContinuousUseDevicesdAsync();
    }
}
