using P7WebApp.Domain.Aggregates.ProfileAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfileById(string userId);
        Task CreateProfile(string userId);
    }
}
