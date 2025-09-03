using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.UserToken;
using HR_Management.Common;
using HR_Management.Domain;
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
        throw new NotImplementedException();
    }

    public async Task<UserTokenDto?> FindByRefreshToken(string refreshToken)
    {
        var hashedRefToken = SecurityHelper.GetSHA256Hash(refreshToken);
        var userToken = await _context.UserTokens
            .Include(userToken => userToken.User)
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
                TokenExp = userToken.TokenExp
            };
    }

    public async Task<bool> Logout(int userId)
    {
        throw new NotImplementedException();
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