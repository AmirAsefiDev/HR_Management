using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.UserToken;
using HR_Management.Common;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories;

public class UserTokenRepository : IUserTokenRepository
{
    private readonly LeaveManagementDbContext _context;

    public UserTokenRepository(LeaveManagementDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckExistToken(string token)
    {
        var hashedToken = SecurityHelper.GetSHA256Hash(token);
        var userToken = await _context.UserTokens.FirstOrDefaultAsync(ut => ut.HashedToken == hashedToken);
        return userToken is not null;
    }

    public async Task<UserTokenDto?> FindByRefreshToken(string refreshToken)
    {
        var hashedRefToken = SecurityHelper.GetSHA256Hash(refreshToken);
        var userToken = await _context.UserTokens
            .Include(userToken => userToken.User)
            .Include(userToken => userToken.User.Role)
            .FirstOrDefaultAsync(t => t.HashedRefreshToken == hashedRefToken);
        return userToken == null
            ? null
            : new UserTokenDto
            {
                UserId = userToken.UserId,
                User = userToken.User,
                HashedRefreshToken = userToken.HashedRefreshToken,
                HashedToken = userToken.HashedToken,
                RefreshTokenExp = userToken.RefreshTokenExp,
                TokenExp = userToken.TokenExp,
                Role = userToken.User.Role
            };
    }

    public async Task<bool> Logout(int userId)
    {
        var userTokens = await _context.UserTokens
            .Where(t => t.UserId == userId)
            .ToListAsync();
        _context.UserTokens.RemoveRange(userTokens);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task SaveToken(UserTokenDto userToken)
    {
        await _context.UserTokens.AddAsync(new UserToken
        {
            UserId = userToken.UserId,
            HashedRefreshToken = userToken.HashedRefreshToken,
            HashedToken = userToken.HashedToken,
            RefreshTokenExp = userToken.RefreshTokenExp,
            TokenExp = userToken.TokenExp
        });
        await _context.SaveChangesAsync();
    }
}