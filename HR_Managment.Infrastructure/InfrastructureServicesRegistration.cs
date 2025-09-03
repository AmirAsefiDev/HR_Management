using HR_Management.Application.Contracts.Infrastructure;
using HR_Management.Application.Contracts.Infrastructure.Authentication;
using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using HR_Management.Application.Models;
using HR_Management.Infrastructure.Authentication;
using HR_Management.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR_Management.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();

        services.AddScoped<ITokenValidator, TokenValidator>();
        services.Configure<JwtOptions>(configuration.GetSection("JWTConfig"));
        services.AddScoped<IJWTTokenService, JWTTokenService>();
        return services;
    }
}