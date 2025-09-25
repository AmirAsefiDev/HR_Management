using HR_Management.Domain;

namespace HR_Management.Application.DTOs.UserToken;

public class UserTokenDto
{
    public int Id { get; set; }
    public string HashedToken { get; set; }
    public string HashedRefreshToken { get; set; }
    public DateTime TokenExp { get; set; }
    public DateTime RefreshTokenExp { get; set; }

    public Domain.User User { get; set; }
    public int UserId { get; set; }
    public Role Role { get; set; }
}