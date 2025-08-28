using PropertyInfo.API.Services;

namespace PropertyInfo.API.Models
{
    public class PropertyListDto
    {
        public IEnumerable<PropertyDto> Properties { get; set; }
        public PaginationMetadata Pagination { get; set; }
    }
}
