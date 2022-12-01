using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Infrastructure.Common.Models;
using P7WebApp.Infrastructure.Identity;
using P7WebApp.Infrastructure.Identity.Services;
using P7WebApp.Infrastructure.Persistence;
using P7WebApp.Infrastructure.Services;
using System.Text;

namespace P7WebApp.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    .EnableRetryOnFailure()));

            services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            // Needed for PostgreSQL date times to work (we probably need to revisit this...)
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


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

            services
                .AddAuthorization();

            byte[] key = Encoding.ASCII.GetBytes(configuration.GetSection("token").GetSection("secret").Value); // This should probably be retrieved some other way
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<ITokenService, TokenService>();
            services.Configure<Token>(configuration.GetSection("token"));

            return services;
        }
    }
}