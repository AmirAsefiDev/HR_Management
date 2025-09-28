using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.User;

public class GetUsersDto : BaseDto
{
    public string FullName { get; set; }
    public string? Mobile { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }

    public string Picture { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; }
}