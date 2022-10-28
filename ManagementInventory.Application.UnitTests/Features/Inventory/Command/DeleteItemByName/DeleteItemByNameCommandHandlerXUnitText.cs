using AutoMapper;
using ManagementInventory.Application.Features.Inventory.Command.DeleteItemByName;
using ManagementInventory.Application.Features.Inventory.Vm;
using ManagementInventory.Application.Mappings;
using ManagementInventory.Application.UnitTests.Mocks;
using ManagementInventory.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace ManagementInventory.Application.UnitTests.Features.Inventory.Command.DeleteItemByName
{
    public class DeleteItemByNameCommandHandlerXUnitText
    {
        private readonly Mock<UnitOfWork> _unitOfWork;

        private readonly IMapper _mapper;

        private readonly Mock<ILogger<DeleteItemByNameCommandHandler>> _logger;

        public DeleteItemByNameCommandHandlerXUnitText()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<ItemMapping>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<DeleteItemByNameCommandHandler>>();

            MockItemRepository.AddDataItemRepository(_unitOfWork.Object.ManagementInventoryDbContext);
        }

        [Fact]
        public async Task DeleteItemByNameCommand_InputStreamer_ReturnsObject()
        {
            var itemInput = new DeleteItemByNameCommand("Item");

            var handler = new DeleteItemByNameCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);
            var result = await handler.Handle(itemInput, CancellationToken.None);

            result.ShouldBeOfType<ItemCommonObjectVm>();
        }
    }
}