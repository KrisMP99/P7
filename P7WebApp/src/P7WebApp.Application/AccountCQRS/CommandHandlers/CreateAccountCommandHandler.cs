using MediatR;
using P7WebApp.Application.AccountCQRS.Commands.CreateUser;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Models;

namespace P7WebApp.Application.AccountCQRS.CommandHandlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public CreateAccountCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("entered");
                var result = await _identityService.CreateUserAsync(firstName: request.FirstName, lastName: request.LastName, username: request.Username, email: request.Email, password: request.Password);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}