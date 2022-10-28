using ManagementInventory.Domain.Common;
using ManagementInventory.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ManagementInventory.Infrastructure.Persistence
{
    /// <summary>
    /// Class that manages the database context
    /// </summary>
    public class ManagementInventoryDbContext : DbContext
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor class
        /// </summary>
        /// <param name="options"></param>
        public ManagementInventoryDbContext(DbContextOptions<ManagementInventoryDbContext> options) : base(options) { }

        public ManagementInventoryDbContext(DbContextOptions<ManagementInventoryDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Class that manages the database context
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdateAt = DateTime.UtcNow;
                        break;
                }
            }

            await _mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Overloaded method to indicate in the creation of the context the restrictions, keys, indexes and tables of the database
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>().HasIndex(x => x.Name).IsUnique();
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Database table
        /// </summary>
        public DbSet<Item>? Items { get; set; }
    }
}