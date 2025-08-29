using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PropertyInfo.API.Entities;
using PropertyInfo.API.Models;
using PropertyInfo.API.Services;
using System.Text.Json;

namespace PropertyInfo.API.Controllers
{
    /// <summary>
    ///  Controller for http operations about properties info
    /// </summary>
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
        private readonly IPropertyImageInfoRepository _propertyImageInfoRepository;

        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        /// <summary>
        /// Constructor class PropertiesController
        /// </summary>
        /// <param name="logger">Dependency Logger</param>
        /// <param name="propertyInfoRepository">Property Repository</param>
        /// <param name="ownerRepository">Owner Repository</param>
        /// <param name="propertyImageInfoRepository">Property Image Repository</param>
        /// <param name="mapper">Dependency Mapper</param>
        /// <exception cref="ArgumentNullException">Exception invalid type</exception>
        public PropertiesController(ILogger<PropertiesController> logger,
            IPropertyInfoRepository propertyInfoRepository,
            IOwnerInfoRepository ownerRepository,
            IPropertyImageInfoRepository propertyImageInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _propertyInfoRepository = propertyInfoRepository ??
                throw new ArgumentNullException(nameof(propertyInfoRepository));

            _ownerRepository = ownerRepository ??
                throw new ArgumentNullException(nameof(ownerRepository));

            _propertyImageInfoRepository = propertyImageInfoRepository ??
                throw new ArgumentNullException(nameof(propertyImageInfoRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get operation to properties info
        /// </summary>
        /// <param name="name">Name of property</param>
        /// <param name="searchQuery">filter to query</param>
        /// <param name="pageNumber">number of page</param>
        /// <param name="pageSize">size of page</param>
        /// <returns></returns>
        [HttpGet(Name = "GetProperties")]
        public async Task<ActionResult> GetProperties(
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
              
                var propertiesListResult = new PropertyListDto();
                propertiesListResult.Properties = _mapper.Map<IEnumerable<PropertyDto>>(propertiesEntities);
                propertiesListResult.Pagination = paginationMetadata;

                return Ok(propertiesListResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(PropertiesController)}");
                return StatusCode(500, "We have problems with your request, contact your provider");
            }

        }

        /// <summary>
        /// POST operation to add property
        /// </summary>
        /// <param name="idOwner">owner identifier</param>
        /// <param name="property">data of property</param>
        /// <returns></returns>
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
                var resultId = await _propertyInfoRepository.AddPropertyInfo(
                    idOwner, finalProperty);

                finalProperty.IdProperty = resultId;

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

        /// <summary>
        /// POST operation to update property
        /// </summary>
        /// <param name="idProperty">property identifier</param>
        /// <param name="property">data of property</param>
        /// <returns></returns>
        [HttpPost("property/{idProperty}", Name = "UpdateProperty")]
        public async Task<ActionResult> UpdateProperty(int idProperty, PropertyDto property)
        {
            try
            {
                var propertyInfo = await _propertyInfoRepository.GetPropertyAsync(idProperty);
                if (propertyInfo == null)
                {
                    return NotFound("property doesn´t exist");
                }
                var finalProperty = _mapper.Map<Entities.Property>(property);
                await _propertyInfoRepository.UpdatePropertyInfo(
                    idProperty, finalProperty);

                finalProperty.IdProperty = idProperty;

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

        /// <summary>
        /// PATCH operation for partial update property
        /// </summary>
        /// <param name="IdProperty">property identifier</param>
        /// <param name="patchDocument">special format to update price property</param>
        /// <returns></returns>
        [HttpPatch("price/{IdProperty}")]
        public async Task<ActionResult> UpdatePropertyPrice(int IdProperty,
           JsonPatchDocument<PropertyForUpdateDto> patchDocument)
        {
            try
            {
                var propertyInfo = await _propertyInfoRepository.GetPropertyAsync(IdProperty);
                if (propertyInfo == null)
                {
                    return NotFound("property doesn´t exist");
                }

                var propertyInfoToPatch = _mapper.Map<PropertyForUpdateDto>(
                propertyInfo);

                patchDocument.ApplyTo(propertyInfoToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!TryValidateModel(propertyInfoToPatch))
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(propertyInfoToPatch, propertyInfo);
                await _propertyInfoRepository.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(PropertiesController)}");
                return StatusCode(500, "We have problems with your request, contact your provider");
            }
        }

        /// <summary>
        /// POST operation to create image property
        /// </summary>
        /// <param name="idProperty">property identifier</param>
        /// <param name="file">image property</param>
        /// <returns></returns>
        [HttpPost("image/{idProperty}", Name = "CreateImageProperty")]
        public async Task<ActionResult> CreateImageProperty(int idProperty, IFormFile file)
        {
            try
            {
                var propertyInfo = await _propertyInfoRepository.GetPropertyAsync(idProperty);
                if (propertyInfo == null)
                {
                    return NotFound("property doesn´t exist");
                }
                // Validate the input. Put a limit on filesize to avoid large uploads attacks. 
                // Only accept .pdf files (check content-type)
                if (file.Length == 0 || file.Length > 20971520 || (file.ContentType != "image/jpg" && file.ContentType != "image/jpeg"))
                {
                    return BadRequest("No file or an invalid one has been inputted.");
                }

                // Create the file path.  Avoid using file.FileName, as an attacker can provide a
                // malicious one, including full paths or relative paths.  
                var pathImage = @$"..\..\..\Images\uploaded_file_{idProperty}-{propertyInfo.CodeInternal}.jpg";
                var path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    pathImage);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var propertyImageInfo = new PropertyImage();
                propertyImageInfo.IdProperty = propertyInfo.IdProperty;
                propertyImageInfo.Enabled = true;
                propertyImageInfo.File = pathImage.Replace(@"..\","");

                var resultId = await _propertyImageInfoRepository.AddPropertyImageInfo(propertyImageInfo);

                propertyImageInfo.IdPropertyImage = resultId;
                var propertyImageInfoResult = new PropertyImageDto();
                _mapper.Map(propertyImageInfo, propertyImageInfoResult);

                return Ok(new { message = "Your file has been uploaded successfully.", imageInfo = propertyImageInfoResult });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(PropertiesController)}");
                return StatusCode(500, "We have problems with your request, contact your provider");
            }
        }
    }
}
