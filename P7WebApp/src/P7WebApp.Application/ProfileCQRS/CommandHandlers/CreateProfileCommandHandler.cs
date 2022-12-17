using MediatR;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.ProfileCQRS.Commands.CreateProfile;
using System.Diagnostics;

namespace P7WebApp.Application.ProfileCQRS.CommandHandlers
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<CreateProfileCommandHandler> _logger;

        public CreateProfileCommandHandler(IIdentityService identityService, ILogger<CreateProfileCommandHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation($"CreateProfileCommandHandler.Handle() began");

            try
            {
                var result = await _identityService.CreateUserAsync(firstName: request.FirstName, lastName: request.LastName, username: request.Username, email: request.Email, password: request.Password);
                
                sw.Stop();
                _logger.LogInformation($"CreateProfileCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogWarning($"CreateProfileCommandHandler.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw;
            }
        }
    }
}