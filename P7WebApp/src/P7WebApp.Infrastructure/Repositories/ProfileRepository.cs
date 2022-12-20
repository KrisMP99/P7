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

        //public async Task<Profile> GetProfileByUserId(string userId)
        //{
        //    try
        //    {
        //        _logger.LogInformation("trying to add profile");
        //        var result = await _context.Profiles.Where(p => p.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
                
        //        return result;
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogWarning($"creating profile failed with message {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task<Profile> GetProfileByUserName(string username)
        {
            try
            {
                //var result = await _context.Profiles.Where(p => p.UserName == username).AsNoTracking().FirstOrDefaultAsync();
                var result = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.UserName == username);
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Profile> GetProfileById(int Id)
        {
            try
            {
                //var result = await _context.Profiles.Where(p => p.UserName == username).AsNoTracking().FirstOrDefaultAsync();
                var result = await _context.Profiles.FindAsync(Id);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // TODO: Password hashing
        public async Task CreateProfile(string firstName, string lastName, string email, string userName, string password)
        {
            var profile = new Profile(firstName: firstName, lastName: lastName, email: email, userName: userName, password: password);
            await _context.Profiles.AddAsync(profile);
        }
    }
}
