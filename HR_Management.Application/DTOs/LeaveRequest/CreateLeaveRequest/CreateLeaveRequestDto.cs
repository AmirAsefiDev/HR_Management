using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveRequest.CreateLeaveRequest;

public class CreateLeaveRequestDto : BaseDto, ILeaveRequestDto
{
    public DateTime DateRequested { get; set; } = DateTime.UtcNow;

    public string RequestComments { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeId { get; set; }
    public int LeaveStatusId { get; set; } = 1;
}