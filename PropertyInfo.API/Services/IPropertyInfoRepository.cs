using PropertyInfo.API.Entities;

namespace PropertyInfo.API.Services
{
    /// <summary>
    /// Property Repository Contract
    /// </summary>
    public interface IPropertyInfoRepository
    {
        Task<(IEnumerable<Property>, PaginationMetadata)> GetPropertiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);

        Task<int> AddPropertyInfo(int idOwner, Property propertyInfo);

        Task UpdatePropertyInfo(int idProperty, Property propertyInfo);

        Task<Property?> GetPropertyAsync(int idProperty);

        Task<bool> SaveChangesAsync();
    }
}
