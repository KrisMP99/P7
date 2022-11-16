using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Domain.Identity;
using P7WebApp.Infrastructure.Common.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public async Task<(ApplicationUser? User, string? Token)> AuthenticateAsync(string username, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user is not null)
                {
                    SignInResult signIn = await _signInManager.PasswordSignInAsync(user.NormalizedUserName, password, true, false);

                    if (signIn.Succeeded)
                    {
                        byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);
                        var handler = new JwtSecurityTokenHandler();

                        var descriptor = new SecurityTokenDescriptor
                        {
                            Issuer = _token.Issuer,
                            Audience = _token.Audience,
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                            new Claim("UserId", user.Id),
                            new Claim("FirstName", $"{user.FirstName}"),
                            new Claim("LastName", $"{user.FirstName}"),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.UserName)
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var jwtToken = handler.CreateToken(descriptor);

                        await _userManager.UpdateAsync(user);

                        return (user, handler.WriteToken(jwtToken));
                    }
                }

                return (null, null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
