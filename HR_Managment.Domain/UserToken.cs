namespace HR_Management.Domain;

public class UserToken
{
    public int Id { get; set; }
    public string HashedToken { get; set; }
    public string HashedRefreshToken { get; set; }
    public DateTime TokenExp { get; set; }
    public DateTime RefreshTokenExp { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }
}