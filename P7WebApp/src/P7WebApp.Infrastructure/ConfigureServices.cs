using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Infrastructure.Data;
using P7WebApp.Infrastructure.Identity;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Repositories;
using P7WebApp.Infrastructure.Persistence;

namespace P7WebApp.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            //services
            //    .AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddUserStore<ApplicationDbContext>();
            //
            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            //
            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
