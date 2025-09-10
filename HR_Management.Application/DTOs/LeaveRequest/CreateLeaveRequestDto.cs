using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveRequest;

public class CreateLeaveRequestDto : BaseDto, ILeaveRequestDto
{
    public DateTime DateRequested { get; set; }

    public string RequestComments { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeId { get; set; }
    public int LeaveStatusId { get; set; } = 1;

    //public DateTime DateActioned { get; set; }
    //public bool? Aoorived { get; set; }
    //public bool Cancelled { get; set; }
}