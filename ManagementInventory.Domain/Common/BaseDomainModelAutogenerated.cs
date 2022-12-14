using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementInventory.Domain.Common
{
    /// <summary>
    /// Abstract class contain primary key for alls tables
    /// </summary>
    public abstract class BaseDomainModelAutogenerated : BaseDomainModel
    {
        /// <summary>
        /// Primary key of identity type
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}