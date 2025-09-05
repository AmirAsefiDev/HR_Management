namespace HR_Management.Application.DTOs.Authentication.Signup;

public class SignupDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExp { get; set; }
}