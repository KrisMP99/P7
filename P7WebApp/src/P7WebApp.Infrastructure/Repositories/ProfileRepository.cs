using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.ProfileAggregate;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<ProfileRepository> _logger;

        public ProfileRepository(IApplicationDbContext context, ILogger<ProfileRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Profile> GetProfileByUserId(string userId)
        {
            try
            {
                _logger.LogInformation("trying to add profile");
                var result = await _context.Profiles.Where(p => p.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
                
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"creating profile failed with message {ex.Message}");
                throw;
            }
        }

        public async Task CreateProfile(string userId, string firstName, string lastName, string email, string userName)
        {
            var profile = new Profile(userId: userId, firstName: firstName, lastName: lastName, email: email, userName: userName);
            await _context.Profiles.AddAsync(profile);
        }
    }
}
