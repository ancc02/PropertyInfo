using PropertyInfo.API.DbContexts;
using PropertyInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropertyInfo.API.Services
{
    public class OwnerInfoRepository : IOwnerInfoRepository
    {

        private readonly PropertyInfoContext _context;

        public OwnerInfoRepository(PropertyInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<Owner?> GetOwnerAsync(int idOwner)
        {
            return await _context.Owners
                  .Where(c => c.IdOwner == idOwner).FirstOrDefaultAsync();
        }
    }
}
