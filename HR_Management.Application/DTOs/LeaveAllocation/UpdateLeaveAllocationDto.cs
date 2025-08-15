using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveAllocation;

public class UpdateLeaveAllocationDto : BaseDto, ILeaveAllocationDto
{
    public int LeaveTypeId { get; set; }
    public int Priod { get; set; }
    public int NumberOfDays { get; set; }
}