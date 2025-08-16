using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveStatus;

public class UpdateLeaveStatusDto : BaseDto, ILeaveStatusDto
{
    public string Name { get; set; }

    public string Description { get; set; }
}