﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using NSwag;
using NSwag.Generation.Processors.Security;
using P7WebApp.API.Services;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Infrastructure.Persistence;

namespace P7WebApp.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {

            IdentityModelEventSource.ShowPII = true;
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.MaxDepth = 10;
                });

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddEndpointsApiExplorer();

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "CleanArchitecture API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            return services;
        }
    }
}