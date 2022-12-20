using P7WebApp.Domain.Aggregates.ProfileAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfileByUserName(string username);
        //Task<Profile> GetProfileByUserId(string userId);
        Task<Profile> GetProfileById(int Id);
        Task CreateProfile(string firstName, string lastName, string email, string userName, string password);
    }
}
