namespace ManagementInventory.Application.Features.Inventory.Vm
{
    /// <summary>
    /// Response class to item object
    /// </summary>
    public class ItemCommonObjectVm
    {
        /// <summary>
        /// Identifier item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Item name
        /// </summary>
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
    }
}