using MediatR;
using Microsoft.EntityFrameworkCore;
using P7WebApp.Domain.Common;

namespace P7WebApp.Infrastructure.Common
{
    public static class MediatorExtesions
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
        {
            var entities = context.ChangeTracker
                .Entries<EntityBase>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
