namespace HR_Management.Domain;

public class User : BaseDomainEntity
{
    public string FullName { get; set; }
    public string? Mobile { get; set; }
    public int CountryCode { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public int RoleId { get; set; } = 1;
    public Role Role { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
    public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

    public ICollection<LeaveRequestStatusHistory> LeaveRequestStatusHistories { get; set; } =
        new List<LeaveRequestStatusHistory>();

    public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}