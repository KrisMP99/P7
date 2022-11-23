using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Domain.Aggregates.AccountAggregate;
using P7WebApp.Domain.Identity;
using P7WebApp.Infrastructure.Common.Models;
using System.Text;

namespace P7WebApp.Infrastructure.Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Token _token;

        public TokenService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<Token> tokenOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _token = tokenOptions.Value;
        }

        public async Task<Account> AuthenticateAsync(string username, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user is not null)
                {
                    SignInResult signIn = await _signInManager.PasswordSignInAsync(user, password, true, false);

                    if (signIn.Succeeded)
                    {
                        var account = new Account(user.Id, user.UserName, new AccountProfile(user.FirstName, user.LastName, user.Email));

                        byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);

                        account.SetToken(issuer: _token.Issuer, audience: _token.Audience, expires: _token.Expiry, secret: secret);

                        return account;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}