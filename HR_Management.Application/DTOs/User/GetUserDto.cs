namespace HR_Management.Application.DTOs.User;

public class GetUserDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Mobile { get; set; }
    public string? Email { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}