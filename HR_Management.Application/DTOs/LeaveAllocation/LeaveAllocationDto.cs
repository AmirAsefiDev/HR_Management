using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveAllocation;

public class LeaveAllocationDto : BaseDto, ILeaveAllocationDto
{
    public DateTime DateCreated { get; set; }
    public int TotalDays { get; set; }
    public int Period { get; set; }
    public string LeaveTypeName { get; set; }
    public string FullName { get; set; }
    public int LeaveTypeId { get; set; }
    public int UserId { get; set; }
}