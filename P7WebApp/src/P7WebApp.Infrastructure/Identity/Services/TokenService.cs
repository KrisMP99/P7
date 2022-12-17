using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Responses.Profile;
using P7WebApp.Infrastructure.Common.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace P7WebApp.Infrastructure.Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Token _token;
        private readonly ILogger<TokenService> _logger;

        public TokenService(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            IOptions<Token> tokenOptions,
            IUnitOfWork unitOfWork,
            ILogger<TokenService> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _token = tokenOptions.Value;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TokenResponse> AuthenticateAsync(string username, string password)
        {
            try
            {
                var userManagerStopWatch = Stopwatch.StartNew();
                var user = await _userManager.FindByNameAsync(username);
                userManagerStopWatch.Stop();
                _logger.LogInformation($"user manager milliseconds: {userManagerStopWatch.ElapsedMilliseconds}");

                if (user is not null)
                {
                    var signInStopWatch = Stopwatch.StartNew();

                    SignInResult signIn = await _signInManager.PasswordSignInAsync(user.UserName, password, true, false);

                    signInStopWatch.Stop();

                    _logger.LogInformation($"sign in milliseconds {signInStopWatch.ElapsedMilliseconds}");

                    if (signIn.Succeeded)
                    {
                        byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);
                        string token = SetToken(issuer: _token.Issuer, audience: _token.Audience, expires: _token.Expiry, secret: secret, user: user);

                        var profile = await _unitOfWork.ProfileRepository.GetProfileByUserId(user.Id);

                        var tokenResponse = new TokenResponse(token: token, userId: profile.Id, firstname: user.FirstName, lastname: user.LastName, email: user.Email, username: user.UserName);

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