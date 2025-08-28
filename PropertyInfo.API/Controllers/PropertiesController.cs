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
        private readonly ILogger<PropertiesController> _logger;
        private readonly IPropertyInfoRepository _propertyInfoRepository;
        private readonly IOwnerInfoRepository _ownerRepository;

        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public PropertiesController(ILogger<PropertiesController> logger,
            IPropertyInfoRepository propertyInfoRepository,
            IOwnerInfoRepository ownerRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _propertyInfoRepository = propertyInfoRepository ??
                throw new ArgumentNullException(nameof(propertyInfoRepository));

            _ownerRepository = ownerRepository ??
                throw new ArgumentNullException(nameof(ownerRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetProperties")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties(
                    string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(PropertiesController)}");
                return StatusCode(500, "We have problems with your request, contact your provider");
            }
            
        }

        [HttpPost("owner/{idOwner}", Name = "AddProperty")]
        public async Task<ActionResult> AddProperty(int idOwner, PropertyDto property)
        {
            try
            {
                var owner = await _ownerRepository.GetOwnerAsync(idOwner);
                if (owner == null)
                {
                    return NotFound("owner doesn´t exist");
                }
                var finalProperty = _mapper.Map<Entities.Property>(property);               
                await _propertyInfoRepository.AddPropertyInfo(
                    idOwner, finalProperty);

                var createdPropertyToReturn =
                    _mapper.Map<Models.PropertyDto>(finalProperty);

                return CreatedAtRoute("GetProperties",
                     new
                     {
                         name = createdPropertyToReturn.Name,
                     },
                     createdPropertyToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(PropertiesController)}");
                return StatusCode(500, "We have problems with your request, contact your provider");
            }            
        }
    }
}
