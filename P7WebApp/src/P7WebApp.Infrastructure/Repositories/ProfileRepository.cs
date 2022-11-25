using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.ProfileAggregate;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IApplicationDbContext _context;

        public ProfileRepository(IApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public Task<Profile> GetProfileById(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateProfile(string userId)
        {
            var profile = new Profile(userId);
            await _context.Profiles.AddAsync(profile);
        }
    }
}
