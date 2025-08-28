using PropertyInfo.API.Entities;

namespace PropertyInfo.API.Services
{
    public interface IPropertyImageInfoRepository
    {
        Task<int> AddPropertyImageInfo(PropertyImage propertyImageInfo);
        Task<bool> SaveChangesAsync();
    }
}
