using ERP.Application.Interfaces.Email;
using ERP.Infrastructure.ExternalServices.Email;
using HR_Management.Application.Contracts.Infrastructure.Authentication;
using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using HR_Management.Application.Contracts.Infrastructure.Path;
using HR_Management.Application.Models;
using HR_Management.Infrastructure.Authentication;
using HR_Management.Infrastructure.Job;
using HR_Management.Infrastructure.Path;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR_Management.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Email
        services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        // Auth
        services.AddScoped<ITokenValidator, TokenValidator>();
        services.AddSingleton<IRolePermissionService, RolePermissionService>();
        services.Configure<JwtOptions>(configuration.GetSection("JWTConfig"));
        services.AddScoped<IJWTTokenService, JWTTokenService>();

        // Permission Policies
        services.AddAuthorization(options => { options.AddPermissionPolicies(); });

        services.AddScoped<IPathService, PathService>();

        services.AddHostedService<YearlyLeaveAllocationJob>();

        return services;
    }
}