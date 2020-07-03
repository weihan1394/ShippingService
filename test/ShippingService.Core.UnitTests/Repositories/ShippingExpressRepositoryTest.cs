using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ShippingService.Core.Models;
using ShippingService.Core.Repositories;
using Xunit;

namespace ShippingService.Core.UnitTests.Repositories
{
    public class ShippingExpressRepositoryTest
    {
        private static readonly Fixture _fixture = new Fixture();

        private readonly ShippingExpressRepository _repository;

        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;
        protected readonly DBContext dbContext;

        public ShippingExpressRepositoryTest()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<DBContext>()
                    .UseSqlite(_connection)
                    .Options;
            dbContext = new DBContext(options);
            if (dbContext != null)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            _repository = new ShippingExpressRepository(dbContext);
        }

        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await dbContext.Database.CanConnectAsync());
        }

        [Fact]
        public async Task deleteAllRecords_should_call_GetAllSortedByPlateAsync_onto_service()
        {
            //when
            bool result = await _repository.deleteAllRecords(default);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task insertRecords_should_return_true()
        {
            //prepare data
            var bulks = _fixture.Build<express>()
                .CreateMany(20);

            List<express> lsShippingExpress = new List<express>(bulks);
            bool result = await _repository.insertRecords(lsShippingExpress, default);

            // assert
            result.Should().BeTrue();
        }
    }
}
