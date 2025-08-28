using AutoMapper;

namespace PropertyInfo.API.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Entities.Property, Models.PropertyDto>();
            CreateMap<Models.PropertyDto, Entities.Property>();
            CreateMap<Entities.Property, Models.PropertyForUpdateDto>();
            CreateMap<Models.PropertyForUpdateDto, Entities.Property>();
        }
    }
}
