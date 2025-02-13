using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateOnly ExpirationDate { get; set; }
    }
}
