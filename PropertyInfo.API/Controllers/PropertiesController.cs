using Asp.Versioning;
using AutoMapper;
using PropertyInfo.API.Models;
using PropertyInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PropertyInfo.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "MustBeOwner")]
    [Route("api/v{version:apiVersion}/properties")]
    [ApiVersion(1)]
    [ApiVersion(2)]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyInfoRepository _propertyInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public PropertiesController(IPropertyInfoRepository propertyInfoRepository,
            IMapper mapper)
        {
            _propertyInfoRepository = propertyInfoRepository ??
                throw new ArgumentNullException(nameof(propertyInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties(
                    string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }

            var (propertiesEntities, paginationMetadata) = await _propertyInfoRepository
                .GetPropertiesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<PropertyDto>>(propertiesEntities));
        }
    }
}
