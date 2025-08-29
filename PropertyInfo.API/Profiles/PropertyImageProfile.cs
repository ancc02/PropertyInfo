using AutoMapper;

namespace PropertyInfo.API.Profiles
{
    /// <summary>
    /// Profile to map entitie to dto
    /// </summary>
    public class PropertyImageProfile : Profile
    {
        public PropertyImageProfile()
        {
            CreateMap<Entities.PropertyImage, Models.PropertyImageDto>();
        }
    }
}
