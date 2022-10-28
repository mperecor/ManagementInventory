using ManagementInventory.Infrastructure.Persistence;
using ManagementInventory.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ManagementInventory.Application.UnitTests.Mocks
{
    /// <summary>
    /// Mock class unit of work
    /// </summary>
    public static class MockUnitOfWork
    {
        /// <summary>
        /// Method to get a mock unit of work
        /// </summary>
        /// <returns>Return a unit of work mocked</returns>
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<ManagementInventoryDbContext>()
                .UseInMemoryDatabase(databaseName: $"ManagementInventoryDbContext-{dbContextId}")
                .Options;

            Mock<IMediator> mediator = new Mock<IMediator>();

            var managementInventoryDbContextFake = new ManagementInventoryDbContext(options, mediator.Object);

            managementInventoryDbContextFake.Database.EnsureDeleted();

            var mockUnitOfWork = new Mock<UnitOfWork>(managementInventoryDbContextFake);

            return mockUnitOfWork;
        }
    }
}