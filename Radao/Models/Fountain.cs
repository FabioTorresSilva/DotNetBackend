using Radao.Enums;
using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    public class Fountain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public SusceptibilityIndex SusceptibilityIndex { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        public List<WaterAnalysis> WaterAnalysis { get; set; }

        [Required]
        public Device Device { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public bool IsDrinkable { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
