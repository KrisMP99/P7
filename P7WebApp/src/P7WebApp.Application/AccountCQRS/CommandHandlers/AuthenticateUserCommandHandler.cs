using MediatR;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using P7WebApp.Application.AccountCQRS.Commands.SignIn;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.Responses;

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
                var account = await _tokenService.AuthenticateAsync(username: request.Username, password: request.Password);

                if (account is null)
                {
                    throw new InvalidCredentialsException("Username or password is incorrect");
                }
                else
                {
                    var response = AuthenticateMapper.Mapper.Map<TokenResponse>(account);

                    if (response is null)
                    {
                        throw new Exception("Issue with mapper");
                    }

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
