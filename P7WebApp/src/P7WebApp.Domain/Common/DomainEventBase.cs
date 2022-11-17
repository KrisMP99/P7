using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.Domain.Common
{
    [NotMapped]
    public class DomainEventBase
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
