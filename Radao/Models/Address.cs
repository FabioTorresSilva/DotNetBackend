using System.ComponentModel.DataAnnotations;

namespace Radao.Models
{
    /// <summary>
    /// Represents an address, including district, country, civil parish, and related fountains.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the unique identifier for the address.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the district of the address.
        /// This field is required and has a maximum length of 50 characters.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the country of the address.
        /// This field is required and has a maximum length of 50 characters.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the parish of the address.
        /// This field is required and has a maximum length of 50 characters.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Parish { get; set; }

        /// <summary>
        /// Gets or sets the list of fountains associated with this address.
        /// This field is required and establishes a relationship with the Fountain model.
        /// </summary>
        [Required]
        public List<Fountain> Fountains { get; set; }
    }
}
