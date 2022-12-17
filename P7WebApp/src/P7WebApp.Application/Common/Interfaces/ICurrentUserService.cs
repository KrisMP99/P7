namespace P7WebApp.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? Username { get; }
        string? FirstName { get; }
        string? LastName { get; }
        string? FullName { get; } 
    }
}
