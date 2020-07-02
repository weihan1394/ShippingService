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

namespace ShippingService.Core.UnitTests.Services
{
    public class CarServiceTests
    {
        //private static readonly Fixture _fixture = new Fixture();

        //private readonly CarService _service;
        //private readonly Mock<CarsContext> _dbContextMock;

        //public CarServiceTests()
        //{
        //    _dbContextMock = new Mock<CarsContext>(new DbContextOptionsBuilder<CarsContext>().Options);

        //    _service = new CarService(_dbContextMock.Object);
        //}

        //[Theory, AutoData]
        //public async Task GetAllSortedByPlateAsync_should_return_expected_result(int rand1, int rand2, int expectedId)
        //{
        //    //given
        //    var cars = new List<car>();
        //    _fixture.AddManyTo(cars, rand1);
        //    cars.Add(new car { id = expectedId, plate = "0" });
        //    _fixture.AddManyTo(cars, rand2);

        //    _dbContextMock.Setup(x => x.cars).Returns(cars.GetMockDbSetObject());

        //    //when
        //    var result = await _service.GetAllSortedByPlateAsync(default);

        //    //then
        //    result.First().Id.Should().Be(expectedId);
        //}
    }
}
