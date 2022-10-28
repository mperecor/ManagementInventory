using AutoFixture;
using ManagementInventory.Domain.Entities;
using ManagementInventory.Infrastructure.Persistence;

namespace ManagementInventory.Application.UnitTests.Mocks
{
    /// <summary>
    /// Mock class for to item repository
    /// </summary>
    public class MockItemRepository
    {
        /// <summary>
        /// Mock insert a new item in a repository
        /// </summary>
        /// <param name="managementInventoryDbContextFake">Context database fake</param>
        public static void AddDataItemRepository(ManagementInventoryDbContext managementInventoryDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var items = fixture.CreateMany<Item>().ToList();

            items.Add(fixture.Build<Item>()
                .With(x => x.Name, "Item")
                .Create());

            managementInventoryDbContextFake.Items!.AddRange(items);
            managementInventoryDbContextFake.SaveChanges();
        }
    }
}