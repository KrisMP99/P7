using P7WebApp.Application.Responses.Account;

namespace P7WebApp.Application.Common.Interfaces.Identity
{
    public interface ITokenService
    {
        Task<TokenResponse> AuthenticateAsync(string username, string password);
    }
}