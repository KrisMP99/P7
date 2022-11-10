using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.User.Commands;

namespace P7WebApp.Application.User.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                (Result result, string userId) = await _identityService.CreateUserAsync(username: request.Username, email: request.Email, password: request.Password);
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
