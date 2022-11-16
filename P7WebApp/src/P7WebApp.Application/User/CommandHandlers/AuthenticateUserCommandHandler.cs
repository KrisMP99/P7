using MediatR;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.Responses;
using P7WebApp.Application.User.Commands.SignIn;

namespace P7WebApp.Application.User.CommandHandlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateCommand, TokenResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly ICurrentUserService _currentUserService;

        public AuthenticateUserCommandHandler(ITokenService tokenService, ICurrentUserService currentUserService)
        {
            _tokenService = tokenService;
            _currentUserService = currentUserService;
        }

        public async Task<TokenResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                (var user, var token) = await _tokenService.AuthenticateAsync(username: request.Username, password: request.Password);
                
                if (user is null || token is null)
                {
                    throw new InvalidCredentialsException("Username or password is incorrect");
                }
                else
                {
                    TokenResponse response = new TokenResponse(user, token);
                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
