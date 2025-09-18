using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveRequest;

public class LeaveRequestListDto : BaseDto
{
    public int LeaveTypeId { get; set; }
    public string LeaveTypeName { get; set; }
    public DateTime DateRequested { get; set; }
    public int LeaveStatusId { get; set; }
    public string LeaveStatusName { get; set; }
    public int CreatorId { get; set; }
    public string CreatorName { get; set; }
}