using System.ComponentModel.DataAnnotations;

namespace Radao.Dtos
{
    /// <summary>
    /// Class that represents an Device data transfer object with all of the arguments of the model
    /// </summary>
    public class DeviceFullDto
    {

        /// <summary>
        /// Device Model
        /// </summary>
        public String Model { get; set; };

        /// <summary>
        /// Device Serial Number 
        /// </summary>
        public String Serial { get; set; }
    }
}
