using MediatR;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.ProfileCQRS.Commands.CreateProfile;

namespace P7WebApp.Application.ProfileCQRS.CommandHandlers
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, int>
    {
        private readonly IIdentityService _identityService;

        public CreateProfileCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<int> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.CreateUserAsync(firstName: request.FirstName, lastName: request.LastName, email: request.Email, username: request.Username, password: request.Password);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}