namespace HR_Management.Application.DTOs.Authentication.CreateToken;

public class CreateTokenResultDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}