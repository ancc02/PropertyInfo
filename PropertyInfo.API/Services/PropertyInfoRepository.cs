using PropertyInfo.API.DbContexts;
using PropertyInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropertyInfo.API.Services
{
    public class PropertyInfoRepository : IPropertyInfoRepository
    {
        private readonly PropertyInfoContext _context;
        private readonly IOwnerInfoRepository _ownerRepository;

        public PropertyInfoRepository(PropertyInfoContext context, IOwnerInfoRepository ownerRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _ownerRepository = ownerRepository;
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
                    || (a.Address != null && a.Address.Contains(searchQuery)));
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


        public async Task AddPropertyInfo(int idOwner, Property propertyInfo)
        {
            var owner = await _ownerRepository.GetOwnerAsync(idOwner);
            if (owner != null)
            {
                propertyInfo.IdOwner = owner.IdOwner;
                _context.Properties.Add(propertyInfo);
            }

            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
