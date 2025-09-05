namespace HR_Management.Application.DTOs.Authentication.Signup;

public class SignupRequestDto
{
    public string FullName { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}