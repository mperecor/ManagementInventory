using MediatR;

namespace ManagementInventory.Application.Features.Inventory.Command.DeleteItemByName.Event
{
    public class ItemRemovedDomainEvent : INotification
    {
        /// <summary>
        /// Item identifier
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Date expiration of item
        /// </summary>
        public DateTime? ExpirationDate { get; }

        /// <summary>
        /// Type of item
        /// </summary>
        public string? Type { get; }

        /// <summary>
        /// Price of item
        /// </summary>
        public decimal Price { get; }

        public ItemRemovedDomainEvent(int id, string name, DateTime? expirationDate, string? type, decimal price)
        {
            Id = id;
            Name = name;
            ExpirationDate = expirationDate;
            Type = type;
            Price = price;
        }
    }
}