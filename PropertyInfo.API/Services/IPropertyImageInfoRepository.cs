using PropertyInfo.API.Entities;

namespace PropertyInfo.API.Services
{
    /// <summary>
    /// Property Image Repository Contract
    /// </summary>
    public interface IPropertyImageInfoRepository
    {
        Task<int> AddPropertyImageInfo(PropertyImage propertyImageInfo);
        Task<bool> SaveChangesAsync();
    }
}
