namespace HR_Management.Application.DTOs.User.EditProfile;

public class EditProfileDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string? Mobile { get; set; } = null;
    public string Email { get; set; }
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}