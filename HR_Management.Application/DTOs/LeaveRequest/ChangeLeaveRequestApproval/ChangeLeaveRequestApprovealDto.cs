using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalDto : BaseDto
{
    public ILeaveRequestRepository.ApprovalStatuses approvalStatus { get; set; }
}