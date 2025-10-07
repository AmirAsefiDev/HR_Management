using HR_Management.Application.DTOs.UserToken;

namespace HR_Management.Application.Contracts.Persistence;

public interface IUserTokenRepository
{
    Task SaveTokenAsync(UserTokenDto userToken);
    Task<bool> CheckExistTokenAsync(string token);
    Task<UserTokenDto?> FindByRefreshTokenAsync(string refreshToken);
    Task<bool> LogoutAsync(int userId);
}