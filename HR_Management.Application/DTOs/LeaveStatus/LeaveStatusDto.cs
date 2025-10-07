using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveStatus;

public class LeaveStatusDto : BaseDto, ILeaveStatusDto
{
    public DateTime DateCreated { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
}