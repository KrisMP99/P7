namespace P7WebApp.SharedKernel
{
    public class DomainEventBase
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
