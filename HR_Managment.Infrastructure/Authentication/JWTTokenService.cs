using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR_Management.Infrastructure.Authentication;

public class JWTTokenService : IJWTTokenService
{
    private readonly JwtOptions _option;

    public JWTTokenService(IOptions<JwtOptions> option)
    {
        _option = option.Value;
    }

    public async Task<TokenPairDto> GenerateAsync(UserTokenInput input, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var accessExp = now.AddMinutes(_option.AccessTokenMinutes);
        var refreshExp = now.AddDays(_option.RefreshTokenDays);

        var claims = new List<Claim>
        {
            new("UserId", input.UserId.ToString()),
            new("FullName", input.FullName ?? ""),
            new("Email", input.Email ?? ""),
            new("Role", input.Role ?? "")
        };
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