using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Services.Common;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Domain.Identity;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Common.Models;
using P7WebApp.Infrastructure.Data;
using P7WebApp.Infrastructure.Identity;
using P7WebApp.Infrastructure.Identity.Services;
using P7WebApp.Infrastructure.Persistence;
using P7WebApp.Infrastructure.Persistence.Intercepters;
using P7WebApp.Infrastructure.Repositories;
using P7WebApp.Infrastructure.Services;

namespace P7WebApp.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();    

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddHttpContextAccessor();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IAuditableEntitySaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

            services.AddScoped<ITokenService, TokenService>();

            services.Configure<Token>(configuration.GetSection("token"));

            return services;
        }
    }
}