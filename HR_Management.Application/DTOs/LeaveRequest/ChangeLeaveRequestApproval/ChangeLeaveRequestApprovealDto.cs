using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestChangeStatusDto : BaseDto
{
    public ILeaveRequestRepository.ApprovalStatuses approvalStatus { get; set; }
    public string Comment { get; set; }
}