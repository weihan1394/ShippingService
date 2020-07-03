using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using ShippingService.Core.Models;
using ShippingService.Core.Services;
using ShippingService.Core.UnitTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using ShippingService.Core.Repositories;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Text;
using ShippingService.Core.Dtos;
using System.Threading;

namespace ShippingService.Core.UnitTests.Services
{
    public class ShippingRateServiceTest
    {
        private static readonly Fixture _fixture = new Fixture();

        private readonly ShippingRateService _shippingRateService;
        private readonly Mock<IShippingBulkRepository> _shippingBulkRepository;
        private readonly Mock<IShippingExpressRepository> _shippingExpressRepository;
        private readonly Mock<IShippingPostalRepository> _shippingPostalRepository;
        private readonly Mock<DBContext> _dbContectmock;

        public ShippingRateServiceTest()
        {
            _shippingBulkRepository = new Mock<IShippingBulkRepository>();
            _shippingExpressRepository = new Mock<IShippingExpressRepository>();
            _shippingPostalRepository = new Mock<IShippingPostalRepository>();

            _shippingRateService = new ShippingRateService(_shippingExpressRepository.Object, _shippingBulkRepository.Object, _shippingPostalRepository.Object);
        }

        [Fact]
        public async Task SizeConverter_should_return_LessThen1KB()
        {
            string result = _shippingRateService.SizeConverter(1);

            result.Should().Be("Less then 1KB");
        }

        [Fact]
        public async Task SizeConverter_should_return_1KB()
        {
            string result = _shippingRateService.SizeConverter(1024);

            result.Should().Be("1KB");
        }

        [Fact]
        public async Task SizeConverter_should_return_1MB()
        {
            string result = _shippingRateService.SizeConverter(1048576);

            result.Should().Be("1MB");
        }

        [Fact]
        public async Task SizeConverter_should_return_1GB()
        {
            string result = _shippingRateService.SizeConverter(1073741824);

            result.Should().Be("1GB");
        }

        [Fact]
        public async Task saveFile_test()
        {
            string rootPath = Directory.GetCurrentDirectory();
            string testAssetsFolder = @"Assets\";
            string testFile = "test.xlsx";
            string testFilePath = Path.Combine(rootPath, testAssetsFolder, testFile);

            // Arrange.
            var fileMock = new Mock<IFormFile>();
            var physicalFile = new FileInfo(testFilePath);
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            using (FileStream fs = physicalFile.OpenRead())
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    writer.WriteLine(temp.GetString(b));
                }
            }
            writer.Flush();
            ms.Position = 0;
            var fileName = physicalFile.Name;
            //Setup mock file using info from physical file
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));

            List<IFormFile> files = new List<IFormFile>();
            files.Add(fileMock.Object);

            await _shippingRateService.saveFile(files, testAssetsFolder, "upload", default);

            testFilePath.Should().Be("1GB");

        }


        [Fact]
        public async Task retrieveExpress_test()
        {
            // prepare data
            var expressMock = _fixture.Build<express>()
                .CreateMany(1);
            List<express> lsExpress = new List<express>(expressMock);
            _shippingExpressRepository.Setup(x => x.retrieveAll(It.IsAny<CancellationToken>())).Returns(lsExpress);
            
            List<ExpressDto> lsExpressDto = _shippingRateService.retrieveExpress(default);

            lsExpressDto.Count().Should().Be(1);
        }
    }
}
