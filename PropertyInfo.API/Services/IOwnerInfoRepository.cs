using PropertyInfo.API.Entities;

namespace PropertyInfo.API.Services
{
    /// <summary>
    /// Owner Repository Contract
    /// </summary>
    public interface IOwnerInfoRepository
    {
        Task<Owner?> GetOwnerAsync(int idOwner);
    }
}
