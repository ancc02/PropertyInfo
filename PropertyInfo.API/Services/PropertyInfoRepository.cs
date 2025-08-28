using PropertyInfo.API.DbContexts;
using PropertyInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropertyInfo.API.Services
{
    public class PropertyInfoRepository : IPropertyInfoRepository
    {
        private readonly PropertyInfoContext _context;

        public PropertyInfoRepository(PropertyInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<(IEnumerable<Property>, PaginationMetadata)> GetPropertiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Properties as IQueryable<Property>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                    || (a.Address != null && a.Address.Contains(searchQuery))
                    || (a.CodeInternal != null && a.CodeInternal.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }


        public async Task<int> AddPropertyInfo(int idOwner, Property propertyInfo)
        {
            propertyInfo.IdOwner = idOwner;
            _context.Properties.Add(propertyInfo);
            await SaveChangesAsync();

            return propertyInfo.IdProperty;
        }

        public async Task UpdatePropertyInfo(int idProperty, Property propertyInfo)
        {
            var currentProperty = await GetPropertyAsync(idProperty);
            currentProperty.Name = propertyInfo.Name;
            currentProperty.Address = propertyInfo.Address;
            currentProperty.CodeInternal = propertyInfo.CodeInternal;
            currentProperty.Price = propertyInfo.Price;
            _context.Properties.Entry(currentProperty).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task<Property?> GetPropertyAsync(int idProperty)
        {
            return await _context.Properties
                  .Where(c => c.IdProperty == idProperty).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
