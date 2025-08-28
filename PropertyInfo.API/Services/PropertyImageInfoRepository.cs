using PropertyInfo.API.DbContexts;
using PropertyInfo.API.Entities;

namespace PropertyInfo.API.Services
{
    public class PropertyImageInfoRepository : IPropertyImageInfoRepository
    {
        private readonly PropertyInfoContext _context;

        public PropertyImageInfoRepository(PropertyInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> AddPropertyImageInfo(PropertyImage propertyImageInfo)
        {
            _context.PropertyImages.Add(propertyImageInfo);
            await SaveChangesAsync();

            return propertyImageInfo.IdProperty;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
