using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    public class ContinuousUseDevice
    {
        [Required]
        public Fountain Fountain { get; set; }

        public int FountainId { get; set; }

        [Required]
        public int AnalysisFrequency { get; set; }

        [Required]
        public DateOnly LastAnalysisDate { get; set; }
    }
}
