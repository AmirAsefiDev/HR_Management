namespace HR_Management.Application.DTOs.LeaveAllocation;

public interface ILeaveAllocationDto
{
    public int LeaveTypeId { get; set; }
    public int UserId { get; set; }
}