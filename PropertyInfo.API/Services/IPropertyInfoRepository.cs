using PropertyInfo.API.Entities;

namespace PropertyInfo.API.Services
{
    public interface IPropertyInfoRepository
    {
        Task<(IEnumerable<Property>, PaginationMetadata)> GetPropertiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);

        Task AddPropertyInfo(int idOwner, Property propertyInfo);

        Task<bool> SaveChangesAsync();
    }
}
