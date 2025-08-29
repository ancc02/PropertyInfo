using PropertyInfo.API.Services;

namespace PropertyInfo.API.Models
{
    /// <summary>
    /// Property List Data Transfer Object (DTO) 
    /// </summary>
    public class PropertyListDto
    {
        public IEnumerable<PropertyDto> Properties { get; set; }
        public PaginationMetadata Pagination { get; set; }
    }
}
