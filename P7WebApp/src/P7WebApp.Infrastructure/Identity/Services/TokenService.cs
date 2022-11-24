using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Responses.Account;
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

        public async Task<TokenResponse> AuthenticateAsync(string username, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user is not null)
                {
                    SignInResult signIn = await _signInManager.PasswordSignInAsync(user, password, true, false);

                    if (signIn.Succeeded)
                    {
                        byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);
                        string token = SetToken(issuer: _token.Issuer, audience: _token.Audience, expires: _token.Expiry, secret: secret, user: user);
                        
                        var tokenResponse = new TokenResponse(token: token, userId: user.Id, firstname: user.FirstName, lastname: user.LastName, email: user.Email, username: user.UserName);

                        return tokenResponse;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string SetToken(string issuer, string audience, int expires, byte[] secret, ApplicationUser user)
        {
            try
            {
                if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || expires == 0 || secret is null)
                {
                    throw new Exception("Was given invalid date for creation of token"); // TODO: Make own exception?
                }
                else
                {
                    var handler = new JwtSecurityTokenHandler();
                    var descriptor = new SecurityTokenDescriptor
                    {
                        Issuer = issuer,
                        Audience = audience,
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserId", user.Id),
                            new Claim("FirstName", $"{user.FirstName}"),
                            new Claim("LastName", $"{user.LastName}"),
                            new Claim("Username", user.UserName),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.Id)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(expires),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var jwtToken = handler.CreateToken(descriptor);
                    var writtenToken = handler.WriteToken(jwtToken);

                    return writtenToken;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}