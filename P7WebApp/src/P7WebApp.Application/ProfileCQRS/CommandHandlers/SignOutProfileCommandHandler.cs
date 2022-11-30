using MediatR;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.ProfileCQRS.Commands.SignOutProfile;

namespace P7WebApp.Application.ProfileCQRS.CommandHandlers
{
    public class SignOutProfileCommandHandler : IRequestHandler<SignOutProfileCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public SignOutProfileCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(SignOutProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _identityService.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
