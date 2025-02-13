using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string District { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string CivilParish { get; set; }

        [Required]
        public List<Fountain> Fountains { get; set; }
    }
}
