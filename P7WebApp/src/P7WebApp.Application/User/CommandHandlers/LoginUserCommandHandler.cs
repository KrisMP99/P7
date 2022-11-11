using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.User.Commands.SignIn;

namespace P7WebApp.Application.User.CommandHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly IIdentityService _identityService;
        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Result result = await _identityService.LoginUserAsync(request.Username, request.Password);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
