﻿using P7WebApp.Application.Common.Models;

namespace P7WebApp.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<Result> LoginUserAsync(string username, string password);

        Task<(Result Result, string UserId)> CreateUserAsync(string firstName, string lastName, string username, string email, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}