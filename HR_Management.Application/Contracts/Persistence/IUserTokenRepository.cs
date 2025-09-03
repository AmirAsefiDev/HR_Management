using HR_Management.Application.DTOs.UserToken;

namespace HR_Management.Application.Contracts.Persistence;

public interface IUserTokenRepository
{
    Task SaveToken(UserTokenDto userToken);
    Task<bool> CheckExistToken(string token);
    Task<UserTokenDto?> FindByRefreshToken(string refreshToken);
    Task<bool> Logout(int userId);
}