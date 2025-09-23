using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestChangeStatusRequestDto
{
    public ILeaveRequestRepository.ApprovalStatuses ApprovalStatus { get; set; }
    public string Comment { get; set; }
}