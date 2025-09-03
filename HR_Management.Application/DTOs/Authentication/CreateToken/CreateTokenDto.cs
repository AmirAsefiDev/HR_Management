namespace HR_Management.Application.DTOs.Authentication.CreateToken;

public class CreateTokenDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string Email { get; set; }
}