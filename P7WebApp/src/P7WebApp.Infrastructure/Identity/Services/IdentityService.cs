﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Models;

namespace P7WebApp.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        //private readonly IAuthorizationService _authorizationService;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(
            //UserManager<ApplicationUser> userManager, 
            //IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, 
            //IAuthorizationService authorizationService,
            IUnitOfWork unitOfWork)
        {
            //_userManager = userManager;
            //_userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            //_authorizationService = authorizationService;
            _unitOfWork = unitOfWork;
        }

        //public async Task<string> GetUserNameAsync(string userId)
        //{
        //    var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        //    return user.UserName;
        //}

        public async Task<int> CreateUserAsync(string firstName, string lastName, string email, string username, string password)
        {
            await _unitOfWork.ProfileRepository.CreateProfile(
                firstName, 
                lastName, 
                email, 
                username,
                password);

            var rowsAffected = await _unitOfWork.CommitChangesAsync(CancellationToken.None);

            return rowsAffected;
        }

        //public async Task<bool> IsInRoleAsync(string userId, string role)
        //{
        //    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        //    return user != null && await _userManager.IsInRoleAsync(user, role);
        //}

        //public async Task<bool> AuthorizeAsync(string userId, string policyName)
        //{
        //    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        //    var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        //    return result.Succeeded;
        //}

        //public async Task<Result> DeleteUserAsync(string userId)
        //{
        //    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        //    return user != null ? await DeleteUserAsync(user) : Result.Success();
        //}

        //public async Task<Result> DeleteUserAsync(ApplicationUser user)
        //{
        //    var result = await _userManager.DeleteAsync(user);

        //    return result.ToApplicationResult();
        //}
    }
}