using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveRequestStatusHistory;

public class LeaveRequestStatusHistoryDto : BaseDto
{
    public int LeaveRequestId { get; set; }
    public string LeaveRequestName { get; set; }
    public int LeaveStatusId { get; set; }
    public string LeaveStatusName { get; set; }
    public string Comment { get; set; }
    public int ChangedBy { get; set; }
    public string ChangerName { get; set; }
    public DateTime ChangedAt { get; set; }
}