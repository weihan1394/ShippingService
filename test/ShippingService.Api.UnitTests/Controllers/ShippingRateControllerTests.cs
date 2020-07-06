using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using ShippingService.Api.Controllers;
using ShippingService.Core.Dtos;
using ShippingService.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.IO;
using System.Text;

namespace ShippingService.Api.UnitTests.Controllers
{
    public class ShippingRateControllerTests : ControllerTestsBase<ShippingRateController>
    {
        private readonly Mock<IShippingRateService> _shippingRateServiceMock;

        public ShippingRateControllerTests()
        {
            _shippingRateServiceMock = Mocker.GetMock<IShippingRateService>();
        }

        [Fact]
        public async Task uploadShippingRate_should_call_saveFile()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            List<IFormFile> formFiles = new List<IFormFile>();
            formFiles.Add(file);
            //when
            await Controller.uploadShippingRate(formFiles, default);

            //then
            _shippingRateServiceMock.Verify(x => x.saveFile(It.IsAny<List<IFormFile>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory, AutoData]
        public async Task uploadShippingRate_should_return_Ok_with_expected_result()
        {
            //given
            _shippingRateServiceMock.Setup(x => x.saveFile(It.IsAny<List<IFormFile>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            List<IFormFile> formFiles = new List<IFormFile>();
            formFiles.Add(file);
            //when
            var result = await Controller.uploadShippingRate(formFiles, default) as OkObjectResult;

            //then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task getExpressRate_should_call_retrieveExpress()
        {
            //when
            Controller.getExpressRate(default);

            //then
            _shippingRateServiceMock.Verify(x => x.retrieveExpress(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory, AutoData]
        public async Task getExpressRate_should_return_Ok_with_expected_result(List<ExpressDto> expressDtos)
        {
            //given
            _shippingRateServiceMock.Setup(x => x.retrieveExpress(It.IsAny<CancellationToken>())).Returns(expressDtos);

            //when
            var result = Controller.getExpressRate(default) as OkObjectResult;

            //then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
