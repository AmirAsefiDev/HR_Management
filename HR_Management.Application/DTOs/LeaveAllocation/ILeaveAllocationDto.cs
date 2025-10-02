namespace HR_Management.Application.DTOs.LeaveAllocation;

public interface ILeaveAllocationDto
{
    public int TotalDays { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
}