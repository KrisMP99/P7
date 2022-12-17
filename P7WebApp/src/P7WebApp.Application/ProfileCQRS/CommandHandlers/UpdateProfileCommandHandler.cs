using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.ProfileCQRS.Commands.UpdateProfile;

namespace P7WebApp.Application.ProfileCQRS.CommandHandlers
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, int>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public UpdateProfileCommandHandler(ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<int> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}