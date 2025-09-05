namespace HR_Management.Domain;

public class User : BaseDomainEntity
{
    public string FullName { get; set; }
    public string Mobile { get; set; }
    public string? Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
    public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();
}