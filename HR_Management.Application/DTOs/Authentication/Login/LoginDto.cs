namespace HR_Management.Application.DTOs.Authentication.Login;

public class LoginDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExp { get; set; }
}