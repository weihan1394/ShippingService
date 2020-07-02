using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShippingService.Core.Models;
using ShippingService.Core.Repositories;
using Xunit;

namespace ShippingService.Core.UnitTests.Repositories
{
    public class ShippingBulkRepositoryTest
    {
        private static readonly Fixture _fixture = new Fixture();

        private readonly ShippingBulkRepository _repository;
        private readonly Mock<DBContext> _dbContextMock;

        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly DbContext dbContext;


        public void Dispose()
        {
            _connection.Close();
        }

        public ShippingBulkRepositoryTest()
        {
            _dbContextMock = new Mock<DBContext>(new DbContextOptionsBuilder<DBContext>().Options);
            _repository = new ShippingBulkRepository(_dbContextMock.Object);


            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<DbContext>()
                    .UseSqlite(_connection)
                    .Options;
            dbContext = new DbContext(options);
            if (dbContext != null)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
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
            await _repository.deleteAllRecords(default);

            //then
            _dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
