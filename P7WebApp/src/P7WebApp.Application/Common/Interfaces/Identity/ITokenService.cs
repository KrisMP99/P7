using P7WebApp.Domain.Aggregates.AccountAggregate;

namespace P7WebApp.Application.Common.Interfaces.Identity
{
    public interface ITokenService
    {
        Task<Account> AuthenticateAsync(string username, string password);
    }
}