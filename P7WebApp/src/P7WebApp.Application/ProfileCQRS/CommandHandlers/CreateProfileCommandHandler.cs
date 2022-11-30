using MediatR;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.ProfileCQRS.Commands.CreateProfile;

namespace P7WebApp.Application.ProfileCQRS.CommandHandlers
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public CreateProfileCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
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