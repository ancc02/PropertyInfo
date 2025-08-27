using PropertyInfo.API.Entities;

namespace PropertyInfo.API.Services
{
    public interface IOwnerInfoRepository
    {
        Task<Owner?> GetOwnerAsync(int idOwner);
    }
}
