using Microsoft.EntityFrameworkCore;
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

        public async Task<Profile> GetProfileByUserId(string userId)
        {
            try
            {
                var result = await _context.Profiles.Where(p => p.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
                
                return result;
            }
            catch(Exception)
            {
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
