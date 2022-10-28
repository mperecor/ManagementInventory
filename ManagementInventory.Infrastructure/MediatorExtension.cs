using ManagementInventory.Domain.Entities;
using ManagementInventory.Infrastructure.Persistence;
using MediatR;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ManagementInventoryDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Item>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvent());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}