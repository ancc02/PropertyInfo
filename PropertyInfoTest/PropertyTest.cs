using AutoMapper;
using Azure;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using Moq;
using PropertyInfo.API.Controllers;
using PropertyInfo.API.Entities;
using PropertyInfo.API.Models;
using PropertyInfo.API.Profiles;
using PropertyInfo.API.Services;
using PropertyInfoTest.MockModels;
using PropertyInfoTest.Utils;
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
                .ReturnsAsync(UnitTestProperty.GetFakeDataInfoProperty());

            var testController = new PropertiesController(mockLogger.Object, mockPropertyInfoRepository.Object,
                mockOwnerInfoRepository.Object, mockPropertyImageInfoRepository.Object, mapper);

            var actionResult = await testController.GetProperties("pro", null);
            OkObjectResult okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task TestAddProperty()
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

            FakeDataPropertyObject fakePropertyData = UnitTestProperty.GetFakeDataNewProperty();

            mockOwnerInfoRepository.Setup(ow => ow.GetOwnerAsync(1)).ReturnsAsync(fakePropertyData.FakeOwner);
            mockPropertyInfoRepository.Setup(pro => pro.AddPropertyInfo(fakePropertyData.FakeOwner.IdOwner, fakePropertyData.FakeProperty))
                .ReturnsAsync(1);

            var testController = new PropertiesController(mockLogger.Object, mockPropertyInfoRepository.Object,
                mockOwnerInfoRepository.Object, mockPropertyImageInfoRepository.Object, mapper);

            var actionResult = await testController.AddProperty(1, fakePropertyData.FakePropertyDto);
            CreatedAtRouteResult okResult = actionResult as CreatedAtRouteResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(201, okResult.StatusCode);
        }

        [Test]
        public async Task TestUpdateProperty()
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

            FakeDataPropertyObject fakePropertyData = UnitTestProperty.GetFakeDataNewProperty();

            mockPropertyInfoRepository.Setup(ow => ow.GetPropertyAsync(1)).ReturnsAsync(fakePropertyData.FakeProperty);
            mockPropertyInfoRepository.Setup(pro => pro.UpdatePropertyInfo(fakePropertyData.FakeProperty.IdProperty, fakePropertyData.FakeProperty));

            var testController = new PropertiesController(mockLogger.Object, mockPropertyInfoRepository.Object,
                mockOwnerInfoRepository.Object, mockPropertyImageInfoRepository.Object, mapper);

            var actionResult = await testController.UpdateProperty(1, fakePropertyData.FakePropertyDto);
            CreatedAtRouteResult okResult = actionResult as CreatedAtRouteResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(201, okResult.StatusCode);
        }

        [Test]
        public async Task TestUpdatePropertyPrice()
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

            FakeDataPropertyObject fakePropertyData = UnitTestProperty.GetFakeDataNewProperty();

            var patch = new JsonPatchDocument<PropertyForUpdateDto>();
            patch.Replace(p => p.Price, 2000000);

            mockPropertyInfoRepository.Setup(ow => ow.GetPropertyAsync(1)).ReturnsAsync(fakePropertyData.FakeProperty);

            var testController = new PropertiesController(mockLogger.Object, mockPropertyInfoRepository.Object,
                mockOwnerInfoRepository.Object, mockPropertyImageInfoRepository.Object, mapper);
            testController.ObjectValidator = new ObjectValidator();


            var actionResult = await testController.UpdatePropertyPrice(1, patch);
            NoContentResult okResult = actionResult as NoContentResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(204, okResult.StatusCode);
        }

        [Test]
        public async Task TestCreateImageProperty()
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

            var testController = new PropertiesController(mockLogger.Object, mockPropertyInfoRepository.Object,
                mockOwnerInfoRepository.Object, mockPropertyImageInfoRepository.Object, mapper);
        }


    }
}
