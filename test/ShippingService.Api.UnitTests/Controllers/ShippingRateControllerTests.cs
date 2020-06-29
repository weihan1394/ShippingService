using System.Collections.Generic;
using System.Linq;
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
        public async Task GetAll_should_call_GetAllSortedByPlateAsync_onto_service()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            List<IFormFile> formFiles = new List<IFormFile>();
            formFiles.Add(file);
            //when
            await Controller.UploadShippingRate(formFiles, default);

            //then
            _shippingRateServiceMock.Verify(x => x.saveFile(It.IsAny<List<IFormFile>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        //[Theory, AutoData]
        //public async Task GetAll_should_return_Ok_with_expected_result(IEnumerable<CarDto> cars)
        //{
        //    //given
        //    _carServiceMock.Setup(x => x.GetAllSortedByPlateAsync(It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(cars);

        //    //when
        //    var result = await Controller.GetAll(default) as OkObjectResult;

        //    //then
        //    result.Should().NotBeNull();
        //    result.StatusCode.Should().Be(StatusCodes.Status200OK);
        //    result.Value.Should().BeAssignableTo<IEnumerable<CarDto>>();
        //    var value = result.Value as IEnumerable<CarDto>;
        //    value.Should().HaveCount(cars.Count());
        //}
    }
}
