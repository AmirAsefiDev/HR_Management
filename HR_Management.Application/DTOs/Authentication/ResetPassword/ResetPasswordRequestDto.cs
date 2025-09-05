namespace HR_Management.Application.DTOs.Authentication.ResetPassword;

public class ResetPasswordRequestDto
{
    public string NewPassword { get; set; }

    public string Token { get; set; }
}