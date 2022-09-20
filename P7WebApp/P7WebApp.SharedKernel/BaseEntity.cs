using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.SharedKernel
{
    public class BaseEntity
    {
        public int Id { get; set; }

        private List<BaseDomainEvent> _domainEvents = new();

        public IEnumerable<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void RegisterDomainEvent(BaseDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        internal void ClearDomainEvents() => _domainEvents.Clear();
    }
}