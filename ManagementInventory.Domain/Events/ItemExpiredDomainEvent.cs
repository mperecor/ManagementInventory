using MediatR;

namespace ManagementInventory.Application.Features.Inventory.Command.AddItem.Event
{
    public class ItemExpiredDomainEvent : INotification
    {
        /// <summary>
        /// Item identifier
        /// </summary>
        public int ItemId { get; }

        public ItemExpiredDomainEvent(int itemId) => ItemId = itemId;
    }
}