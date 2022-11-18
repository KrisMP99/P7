using MediatR;
using P7WebApp.Application.AccountCQRS.Commands.UpdateAccountProfile;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Domain.Aggregates.AccountAggregate;

namespace P7WebApp.Application.AccountCQRS.CommandHandlers
{
	public class UpdateAccountProfileCommandHandler : IRequestHandler<UpdateAccountProfileCommand, int>
    {
		private readonly ICurrentUserService _currentUserService;
		private readonly IIdentityService _identityService;

		public UpdateAccountProfileCommandHandler(ICurrentUserService currentUserService, IIdentityService identityService)
		{
			_currentUserService = currentUserService;
			_identityService = identityService;
		}

		public async Task<int> Handle(UpdateAccountProfileCommand request, CancellationToken cancellationToken)
        {
			try
			{
				var profile = AuthenticateMapper.Mapper.Map<AccountProfile>(request);

				if (profile is null)
				{
					throw new Exception("Issue with mapper");
				}

				var account = await _identityService.GetUserAccount(_currentUserService.UserId);
				
				account.EditProfileInformation(profile);
				throw new NotImplementedException();
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
