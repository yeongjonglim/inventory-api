using Microsoft.AspNetCore.Authentication.JwtBearer;
using InventoryAPI.Application.Common.Interfaces;
using InventoryAPI.Application.Extensions;
using InventoryAPI.Infrastructure.Models;
using InventoryAPI.Infrastructure.Persistence;
using InventoryAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace InventoryAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                EnvironmentExtensions.GetDatabaseUrl("DATABASE_URL",
                    configuration.GetConnectionString("DefaultConnection")),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDomainEventService, DomainEventService>();

        // services.AddEntityFrameworkNpgsql<ApplicationDbContext>();
            // .AddDefaultIdentity<ApplicationUser>()
            // .AddRoles<IdentityRole>()
            // .AddEntityFrameworkStores<ApplicationDbContext>();

        // services.AddIdentityServer()
        //     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        // services.AddTransient<IIdentityService, IdentityService>();

        var jwtConfiguration = new JwtConfiguration();
        configuration.GetSection("Jwt").Bind(jwtConfiguration);
        services.AddSingleton(jwtConfiguration);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.Authority = jwtConfiguration.Issuer;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = jwtConfiguration.Audience,
                    ValidIssuer = jwtConfiguration.Issuer
                };
            });

        // services.AddAuthorization(options =>
        //     options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}