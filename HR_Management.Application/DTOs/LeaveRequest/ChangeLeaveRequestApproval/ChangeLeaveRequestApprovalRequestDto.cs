using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalRequestDto
{
    public ILeaveRequestRepository.ApprovalStatuses ApprovalStatus { get; set; }
}