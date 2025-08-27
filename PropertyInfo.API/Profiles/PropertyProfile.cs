using AutoMapper;

namespace PropertyInfo.API.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Entities.Property, Models.PropertyDto>();
        }
    }
}
