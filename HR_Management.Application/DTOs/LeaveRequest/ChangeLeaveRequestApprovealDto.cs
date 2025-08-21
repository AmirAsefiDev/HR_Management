using HR_Management.Application.DTOs.Common;
using HR_Management.Application.Persistence.Contracts;

namespace HR_Management.Application.DTOs.LeaveRequest;

public class ChangeLeaveRequestApprovalDto : BaseDto
{
    public ILeaveRequestRepository.ApprovalStatuses approvalStatus { get; set; }
}