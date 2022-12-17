using MediatR;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Responses.Profile;
using P7WebApp.Application.ProfileCQRS.Commands.SignInProfile;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace P7WebApp.Application.ProfileCQRS.CommandHandlers
{
    public class AuthenticateProfileCommandHandler : IRequestHandler<AuthenticateCommand, TokenResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthenticateProfileCommandHandler> _logger;

        public AuthenticateProfileCommandHandler(ITokenService tokenService, ILogger<AuthenticateProfileCommandHandler> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<TokenResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation($"AuthenticateProfileCommandHandler.Handle() began");

            try
            {
                var tokenResponse = await _tokenService.AuthenticateAsync(username: request.Username, password: request.Password);

                if (tokenResponse is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"AuthenticateProfileCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                    throw new InvalidCredentialsException("Username or password is incorrect");
                }
                else
                {
                    sw.Stop();
                    _logger.LogInformation($"AuthenticateProfileCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");
                    return tokenResponse;
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogWarning($"AuthenticateProfileCommandHandler.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw;
            }
        }
    }
}
