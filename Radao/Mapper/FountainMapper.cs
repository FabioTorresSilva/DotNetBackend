using Radao.Models;
using Radao.Dtos;

namespace Radao.Mapper
{
    /// <summary>
    /// Maps Fountain 
    /// </summary>
    public class FountainMapper
    {
        /// <summary>
        /// Maps the FountainFullDTO to the Fountain model
        /// </summary>
        /// <param name="fountainDto"></param>
        /// <returns></returns>
        public Fountain FullDtoToFountain(FountainFullDto fountainDto)
        {
            return new Fountain(fountainDto.Description, fountainDto.SusceptibilityIndex, fountainDto.DeviceId, fountainDto.IsDrinkable, fountainDto.Longitude, fountainDto.Latitude);
        }

        /// <summary>
        /// Maps the FountainIdDTO to the Fountain model
        /// </summary>
        /// <param name="fountainIdDto"></param>
        /// <returns></returns>
        public Fountain IdDtoToFountain(FountainIdDto fountainIdDto)
        {
            return new Fountain(fountainIdDto.Id, fountainIdDto.Description, fountainIdDto.SusceptibilityIndex, fountainIdDto.DeviceId, fountainIdDto.IsDrinkable, fountainIdDto.Longitude, fountainIdDto.Latitude);
        }

        /// <summary>
        /// Maps the Fountain model to the FountainFullDTO
        /// </summary>
        /// <param name="fountain"></param>
        /// <returns></returns>
        public FountainFullDto FountainToFullDto(Fountain fountain)
        {
            return new FountainFullDto(fountain.Description, fountain.SusceptibilityIndex, fountain.DeviceId, fountain.IsDrinkable, fountain.Longitude, fountain.Latitude);
        }

        /// <summary>
        /// Maps the Fountain model to the FountainIdDTO
        /// </summary>
        /// <param name="fountain"></param>
        /// <returns></returns>
        public FountainIdDto FountainToIdDto(Fountain fountain)
        {
            return new FountainIdDto(fountain.Id, fountain.Description, fountain.SusceptibilityIndex, fountain.DeviceId, fountain.IsDrinkable, fountain.Longitude, fountain.Latitude);
        }
    }
}
