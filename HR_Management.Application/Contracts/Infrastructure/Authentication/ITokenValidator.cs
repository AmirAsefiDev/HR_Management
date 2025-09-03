using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace HR_Management.Application.Contracts.Infrastructure.Authentication;

public interface ITokenValidator
{
    Task ExecuteAsync(TokenValidatedContext context);
}