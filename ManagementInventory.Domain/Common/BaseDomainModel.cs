using System.ComponentModel.DataAnnotations;

namespace ManagementInventory.Domain.Common
{
    /// <summary>
    /// Abstract class that contains the domain base fields for all tables
    /// </summary>
    public abstract class BaseDomainModel
    {
        /// <summary>
        /// Register update date
        /// </summary>
        public DateTime? UpdateAt { get; set; }

        /// <summary>
        /// Register create date
        /// </summary>
        [Required]
        public DateTime CreateAt { get; set; }
    }
}