using System.ComponentModel.DataAnnotations;

namespace PropertyInfo.API.Models
{
    public class PropertyDto
    {
        public int IdProperty { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Price { get; set; }

        [Required]
        public string CodeInternal { get; set; }

        [Required]
        public DateTime Year { get; set; } = DateTime.Now;
    }
}
