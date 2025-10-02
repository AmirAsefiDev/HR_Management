namespace HR_Management.Domain;

public class PasswordResetToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Token { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime ExpireAt { get; set; }
    public bool IsUsed { get; set; } = false;
}