namespace HR_Management.Application.DTOs.User.EditProfile;

public class EditProfileRequestDto
{
    public string FullName { get; set; }
    public string? Mobile { get; set; } = null;
    public string Email { get; set; }
}