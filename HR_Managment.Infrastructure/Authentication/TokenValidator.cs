using System.Security.Claims;
using HR_Management.Application.Contracts.Infrastructure.Authentication;
using HR_Management.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;

namespace HR_Management.Infrastructure.Authentication;

public class TokenValidator : ITokenValidator
{
    private readonly ILogger<TokenValidator> _logger;
    private readonly IUserRepository _userRepo;
    private readonly IUserTokenRepository _userTokenRepo;

    public TokenValidator(
        IUserRepository userRepo,
        IUserTokenRepository userTokenRepo,
        ILogger<TokenValidator> logger)
    {
        _userRepo = userRepo;
        _userTokenRepo = userTokenRepo;
        _logger = logger;
    }

    public async Task ExecuteAsync(TokenValidatedContext context)
    {
        var claimIdentity = context.Principal.Identity as ClaimsIdentity;
        if (claimIdentity?.Claims == null || !claimIdentity.Claims.Any())
        {
            context.Fail("claims not found..");
            return;
        }

        var userId = claimIdentity.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedId))
        {
            context.Fail("userID claim not found..");
            return;
        }

        var user = await _userRepo.Get(parsedId);
        if (!user.IsActive)
        {
            context.Fail("user is not active..");
            return;
        }

        var expireClaim = claimIdentity.FindFirst("exp")?.Value;
        if (expireClaim == null || !long.TryParse(expireClaim, out var exp))
        {
            context.Fail("Exception claim not found or invalid");
            return;
        }

        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
        if (expirationTime < DateTime.UtcNow)
        {
            context.Fail("Token has expired");
            return;
        }

        if (context.SecurityToken is not JsonWebToken jsonWebToken)
        {
            context.Fail("Invalid security token.");
            return;
        }

        if (!await _userTokenRepo.CheckExistToken(jsonWebToken.EncodedToken))
        {
            context.Fail("Token is not valid or is deprecated");
            return;
        }

        _logger.LogInformation("Token is valid  for userId: {UserId}", user.Id);
    }
}