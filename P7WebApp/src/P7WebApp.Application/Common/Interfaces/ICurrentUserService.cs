namespace P7WebApp.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int ProfileId { get; }
        string? Username { get; }
        string? FirstName { get; }
        string? LastName { get; }
        string? FullName { get; } 
    }
}
