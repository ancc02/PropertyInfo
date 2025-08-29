using System.ComponentModel.DataAnnotations;

namespace PropertyInfo.API.Models
{
    /// <summary>
    /// Property Image Data Transfer Object (DTO) 
    /// </summary>
    public class PropertyImageDto
    {
        public int IdPropertyImage { get; set; }

        public string File { get; set; }

        public bool Enabled { get; set; }
    }
}
