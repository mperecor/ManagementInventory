﻿using ManagementInventory.Domain.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ManagementInventory.Domain.Entities
{
    /// <summary>
    /// Class contains fields of table Item
    /// </summary>
    public class Item : BaseDomainModelAutogenerated
    {
        /// <summary>
        /// Item Name
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Date expiration of item
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Type of item
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Price of item
        /// </summary>
        public decimal Price { get; set; }

        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);

        public void ClearDomainEvent() => _domainEvents?.Clear();
    }
}