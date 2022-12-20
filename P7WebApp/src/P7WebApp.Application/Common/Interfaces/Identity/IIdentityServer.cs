using P7WebApp.Application.Common.Models;

namespace P7WebApp.Application.Common.Interfaces.Identity
{
    public interface IIdentityService
    {
    //    Task<string> GetUserNameAsync(string userId);
    //    Task<bool> IsInRoleAsync(string userId, string role);
    //    Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<int> CreateUserAsync(string firstName, string lastName, string email, string username, string password);
        //Task<Result> DeleteUserAsync(string userId);
    }
}