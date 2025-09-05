using Microsoft.AspNetCore.Http;

namespace HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;

public interface IJWTTokenService
{
    Task<TokenPairDto> GenerateAsync(UserTokenInput input, CancellationToken ct = default);
    void SerRefreshTokenCookie(HttpContext httpContext, string refreshToken, DateTime expires);
}