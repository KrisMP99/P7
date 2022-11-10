using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Data;
using P7WebApp.Infrastructure.Identity;
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
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IAuditableEntitySaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

            return services;
        }
    }
}