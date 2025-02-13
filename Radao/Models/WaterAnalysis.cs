using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    public class WaterAnalysis
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double RadonConcentration { get; set; }

        [Required]
        public Fountain Fountain { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public Device Device { get; set; }

        [Required]
        public int DeviceId { get; set; }
    }
}
