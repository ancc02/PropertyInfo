using System.ComponentModel.DataAnnotations;

namespace PropertyInfo.API.Models
{
    /// <summary>
    /// Property update Data Transfer Object (DTO) 
    /// </summary>
    public class PropertyForUpdateDto
    {
        [Required]
        [Range(0, 9999999999.99)]
        public decimal Price { get; set; }
    }
}
