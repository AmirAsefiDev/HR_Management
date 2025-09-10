namespace HR_Management.Application.DTOs.LeaveAllocation.UpdateLeaveAllocation;

public class UpdateLeaveAllocationRequestDto
{
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
    public int NumberOfDays { get; set; }
}