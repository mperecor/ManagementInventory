using ManagementInventory.Application.Features.Inventory.Vm;
using MediatR;

namespace ManagementInventory.Application.Features.Inventory.Command.AddItem
{
    public class AddItemCommand : IRequest<ItemCommonObjectVm>
    {
        public string Name { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? Type { get; set; }

        public decimal Price { get; set; }
    }
}