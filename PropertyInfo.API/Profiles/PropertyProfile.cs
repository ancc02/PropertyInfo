using AutoMapper;

namespace PropertyInfo.API.Profiles
{
    /// <summary>
    /// Profile to map entitie to dto
    /// </summary>
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
