using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using ShippingService.Api.IntegrationTests.Infrastructure;
using ShippingService.Core.Dtos;
using Xunit;

namespace ShippingService.Api.IntegrationTests
{
    [Collection(nameof(TestServerClientCollection))]
    public class CarsTests
    {
        private readonly HttpClient _client;

        public CarsTests(TestServerClientFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task Get_should_return_Ok_with_results()
        {
            var result = await _client.GetAsync($"api/cars");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var cars = await result.Content.ReadAsJsonAsync<List<CarDto>>();
            cars.Count.Should().BeGreaterThan(0);
        }
    }
}
