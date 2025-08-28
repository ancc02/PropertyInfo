using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PropertyInfo.API.Controllers;
using PropertyInfo.API.Entities;
using PropertyInfo.API.Models;
using PropertyInfo.API.Profiles;
using PropertyInfo.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInfoTest
{
    [TestFixture]
    public class PropertyTest
    {
        [Test]
        public async Task TestGetProperties()
        {
            var mockLogger = new Mock<ILogger<PropertiesController>>();
            var mockPropertyInfoRepository = new Mock<IPropertyInfoRepository>();
            var mockOwnerInfoRepository = new Mock<IOwnerInfoRepository>();
            var mockPropertyImageInfoRepository = new Mock<IPropertyImageInfoRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PropertyProfile());
            });
            var mapper = mockMapper.CreateMapper();

            mockPropertyInfoRepository.Setup(pro => pro.GetPropertiesAsync("pro", null, 1, 10))
                .ReturnsAsync(GetFakeDataInfoProperty());

            var testController = new PropertiesController(mockLogger.Object, mockPropertyInfoRepository.Object,
                mockOwnerInfoRepository.Object, mockPropertyImageInfoRepository.Object, mapper);

            var actionResult = await testController.GetProperties("pro", null);
            OkObjectResult okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        public (IEnumerable<Property>, PaginationMetadata) GetFakeDataInfoProperty()
        {
            var fakeData = new List<Property> {new Property()
            {
                IdProperty = 1,
                Name = "Property number one fake",
                Address = "address property number one fake",
                Price = 1500000,
                CodeInternal = "0101",
                Year = new DateTime(2005, 5, 10),
                IdOwner = 1
            },
               new Property()
               {
                   IdProperty = 2,
                   Name = "Property number two fake",
                   Address = "address property number two fake",
                   Price = 1500000,
                   CodeInternal = "0101",
                   Year = new DateTime(2005, 5, 10),
                   IdOwner = 2
               },
               new Property()
               {
                   IdProperty = 3,
                   Name = "Property number three fake ",
                   Address = "address property number three fake",
                   Price = 1500000,
                   CodeInternal = "0101",
                   Year = new DateTime(2005, 5, 10),
                   IdOwner = 3
               }};

            return (fakeData, new PaginationMetadata(3, 1, 10));
        }
    }
}
