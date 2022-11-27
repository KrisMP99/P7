using P7WebApp.Domain.Aggregates.ProfileAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfileByUserId(string userId);
        Task CreateProfile(string userId, string firstName, string lastName, string email, string userName);
    }
}
