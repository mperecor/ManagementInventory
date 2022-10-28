using AutoMapper;
using ManagementInventory.Application.Features.Inventory.Command.AddItem;
using ManagementInventory.Application.Features.Inventory.Vm;
using ManagementInventory.Application.Mappings;
using ManagementInventory.Application.UnitTests.Mocks;
using ManagementInventory.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace ManagementInventory.Application.UnitTests.Features.Inventory.Command.Additem
{
    public class AddItemCommandHandlerXUnitTest
    {
        private readonly Mock<UnitOfWork> _unitOfWork;

        private readonly IMapper _mapper;

        private readonly Mock<ILogger<AddItemCommandHandler>> _logger;

        public AddItemCommandHandlerXUnitTest()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<ItemMapping>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<AddItemCommandHandler>>();

            MockItemRepository.AddDataItemRepository(_unitOfWork.Object.ManagementInventoryDbContext);
        }

        [Fact]
        public async Task AddItemCommand_InputStreamer_ReturnsObject()
        {
            var itemInput = new AddItemCommand
            {
                Name = "Item prueba",
                ExpirationDate = DateTime.Today.AddDays(365),
                Price = 1,
                Type = "Prueba"
            };

            var handler = new AddItemCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);
            var result = await handler.Handle(itemInput, CancellationToken.None);

            result.ShouldBeOfType<ItemCommonObjectVm>();
        }
    }
}