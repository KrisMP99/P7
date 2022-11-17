﻿using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Identity;
using System.Security.Claims;

namespace P7WebApp.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? UserId => _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault()?.Value;
    }
}