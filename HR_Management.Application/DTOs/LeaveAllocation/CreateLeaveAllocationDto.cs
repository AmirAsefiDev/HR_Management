namespace HR_Management.Application.DTOs.LeaveAllocation;

public class CreateLeaveAllocationDto : ILeaveAllocationDto
{
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
    public int TotalDays { get; set; }
}