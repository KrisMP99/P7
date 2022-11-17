using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.Domain.Common
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        private List<DomainEventBase> _domainEvents = new();

        public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

        public void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}