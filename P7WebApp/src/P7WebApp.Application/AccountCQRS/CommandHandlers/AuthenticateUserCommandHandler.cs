using MediatR;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using P7WebApp.Application.AccountCQRS.Commands.SignIn;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Responses.Account;

namespace P7WebApp.Application.AccountCQRS.CommandHandlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateCommand, TokenResponse>
    {
        private readonly ITokenService _tokenService;

        public AuthenticateUserCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<TokenResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tokenResponse = await _tokenService.AuthenticateAsync(username: request.Username, password: request.Password);

                if (tokenResponse is null)
                {
                    throw new InvalidCredentialsException("Username or password is incorrect");
                }
                else
                {
                    return tokenResponse;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
