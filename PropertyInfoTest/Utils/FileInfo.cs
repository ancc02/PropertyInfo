using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInfoTest.Utils
{
    public static class FileInfoProperty
    {
        public static IFormFile AsMockIFormFile(this FileInfo physicalFile)
        {
            var fileMock = new Mock<IFormFile>();

            fileMock.Setup(_ => _.FileName).Returns(physicalFile.Name);
            fileMock.Setup(_ => _.ContentType).Returns("image/jpg");
            fileMock.Setup(_ => _.Length).Returns(physicalFile.Length);
            fileMock.Setup(m => m.ContentDisposition).Returns(string.Format("inline; filename={0}", physicalFile.Name));

            return fileMock.Object;
        }
    }
}
