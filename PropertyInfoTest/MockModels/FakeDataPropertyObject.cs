using PropertyInfo.API.Entities;
using PropertyInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInfoTest.MockModels
{
    public class FakeDataPropertyObject
    {
        public Owner FakeOwner { get; set; }
        public Property FakeProperty { get; set; }
        public PropertyDto FakePropertyDto { get; set; }
        public PropertyImage FakePropertyImage { get; set; }
    }
}
