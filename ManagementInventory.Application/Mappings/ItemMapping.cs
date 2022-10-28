using AutoMapper;
using ManagementInventory.Application.Features.Inventory.Command.AddItem;
using ManagementInventory.Application.Features.Inventory.Vm;
using ManagementInventory.Domain.Entities;

namespace ManagementInventory.Application.Mappings
{
    /// <summary>
    /// Class to map item object
    /// </summary>
    public class ItemMapping : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemMapping()
        {
            CreateMap<AddItemCommand, Item>();

            CreateMap<Item, ItemCommonObjectVm>();
        }
    }
}