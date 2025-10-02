using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveAllocation.UpdateLeaveAllocation;

public class UpdateLeaveAllocationDto : BaseDto, ILeaveAllocationDto
{
    public int TotalDays { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
}