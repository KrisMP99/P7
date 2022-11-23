using P7WebApp.Application.Common.Interfaces;
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
        public string? UserId => _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        public string? Username => _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Username")?.Value;
    }
}
