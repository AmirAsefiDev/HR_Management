namespace HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;

public class UserTokenInput
{
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string RoleName { get; set; }
}