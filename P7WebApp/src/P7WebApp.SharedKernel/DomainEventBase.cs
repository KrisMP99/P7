using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.SharedKernel
{
    [NotMapped]
    public class DomainEventBase
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
