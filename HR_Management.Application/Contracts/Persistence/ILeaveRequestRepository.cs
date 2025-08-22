using HR_Management.Domain;

namespace HR_Management.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    public enum ApprovalStatuses
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Cancelled = 4
    }

    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    Task ChangeApprovalStatus(LeaveRequest leaveRequest, ApprovalStatuses approvalStatuses = ApprovalStatuses.Pending);
}