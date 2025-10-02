using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HR_Management.Application.Contracts.Infrastructure.Authentication;
using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR_Management.Infrastructure.Authentication;

public class JWTTokenService : IJWTTokenService
{
    private readonly JwtOptions _option;
    private readonly IRolePermissionService _rolePermissionService;

    public JWTTokenService(IOptions<JwtOptions> option, IRolePermissionService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
        _option = option.Value;
    }

    public async Task<TokenPairDto> GenerateAsync(UserTokenInput input, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var accessExp = now.AddMinutes(_option.AccessTokenMinutes);
        var refreshExp = now.AddDays(_option.RefreshTokenDays);

        var claims = new List<Claim>
        {
            new("sub", input.UserId.ToString()),
            new("name", input.FullName ?? ""),
            new("role", input.RoleName)
        };
        //add permission dynamically by role
        var permissions = _rolePermissionService.GetPermissionsByRole(input.RoleName);
        foreach (var permission in permissions) claims.Add(new Claim("permission", permission));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            _option.Issuer,
            _option.Audience,
            expires: accessExp,
            notBefore: now,
            signingCredentials: credentials,
            claims: claims
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
        var refreshToken = GenerateSecureRefreshToken();

        return new TokenPairDto
        {
            RefreshToken = refreshToken,
            RefreshTokenExpiresAtUtc = refreshExp,
            AccessToken = accessToken,
            AccessTokenExpiresAtUtc = accessExp
        };
    }

    public void SerRefreshTokenCookie(HttpContext httpContext, string refreshToken, DateTime expires)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = expires
        };
        httpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    private static string GenerateSecureRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        return Base64UrlEncode(bytes);
    }

    private static string Base64UrlEncode(byte[] input)
    {
        return Convert.ToBase64String(input)
            .Replace('+', '-').Replace('/', '_')
            .TrimEnd('=');
    }
}

public sealed class JwtOptions
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenMinutes { get; set; } = 15;
    public int RefreshTokenDays { get; set; } = 20;
}