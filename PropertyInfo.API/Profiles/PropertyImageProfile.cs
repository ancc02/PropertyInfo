using AutoMapper;

namespace PropertyInfo.API.Profiles
{
    public class PropertyImageProfile : Profile
    {
        public PropertyImageProfile()
        {
            CreateMap<Entities.PropertyImage, Models.PropertyImageDto>();
        }
    }
}
