using Microsoft.IdentityModel.Tokens;
using P7WebApp.Domain.Identity;

namespace P7WebApp.Application.Common.Interfaces.Identity
{
    public interface ITokenService
    {
        Task<(ApplicationUser User, string Token)> AuthenticateAsync(string username, string password);
    }
}