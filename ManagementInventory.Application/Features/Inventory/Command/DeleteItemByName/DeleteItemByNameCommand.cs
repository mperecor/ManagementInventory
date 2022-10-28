using ManagementInventory.Application.Features.Inventory.Vm;
using MediatR;

namespace ManagementInventory.Application.Features.Inventory.Command.DeleteItemByName
{
    /// <summary>
    /// Classof entry to delete an item by name
    /// </summary>
    public class DeleteItemByNameCommand : IRequest<ItemCommonObjectVm>
    {
        /// <summary>
        /// Name item to delete
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Constructor to set a name of item to delete
        /// </summary>
        /// <param name="name"></param>
        public DeleteItemByNameCommand(string name)
        {
            Name = name;
        }
    }
}