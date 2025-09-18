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
    IQueryable<LeaveRequest> GetLeaveRequestsWithDetails();
    IQueryable<LeaveRequest> GetMyLeaveRequests(int userId);
    Task ChangeApprovalStatus(LeaveRequest leaveRequest, ApprovalStatuses approvalStatuses = ApprovalStatuses.Pending);
}