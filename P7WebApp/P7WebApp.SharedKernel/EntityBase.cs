using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.SharedKernel
{
    public class EntityBase
    {
        public int Id { get; set; }

        private List<DomainEventBase> _domainEvents = new();

        public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

        public void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}