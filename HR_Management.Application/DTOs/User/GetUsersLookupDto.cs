using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.User;

public class GetUsersLookupDto : BaseDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
}