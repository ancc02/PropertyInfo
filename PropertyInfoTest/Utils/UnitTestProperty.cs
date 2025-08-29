using PropertyInfo.API.Entities;
using PropertyInfo.API.Models;
using PropertyInfo.API.Services;
using PropertyInfoTest.MockModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInfoTest.Utils
{
    internal class UnitTestProperty
    {
        public static (IEnumerable<Property>, PaginationMetadata) GetFakeDataInfoProperty()
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

        public static FakeDataPropertyObject GetFakeDataNewProperty()
        {
            FakeDataPropertyObject fakePropertyData = new FakeDataPropertyObject();
            fakePropertyData.FakeOwner = new Owner()
            {
                IdOwner = 1,
                Name = "Name owner fake",
                Address = "address number fake",
                Photo = "files/photos/owner3.pdf",
                Birthday = new DateTime(2000, 1, 10)
            };
            fakePropertyData.FakePropertyDto = new PropertyDto()
            {
                Name = "Property number fake",
                Address = "address property number fake",
                Price = 1500000,
                CodeInternal = "0101",
                Year = new DateTime(2005, 5, 10)
            };

            fakePropertyData.FakeProperty = new Property()
            {
                IdProperty = 1,
                Name = "Property number fake",
                Address = "address property number fake",
                Price = 1500000,
                CodeInternal = "0101",
                Year = new DateTime(2005, 5, 10)
            };

            return fakePropertyData;
        }
    }
}
