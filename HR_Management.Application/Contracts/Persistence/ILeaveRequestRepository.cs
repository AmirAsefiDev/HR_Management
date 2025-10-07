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

    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
    IQueryable<LeaveRequest> GetLeaveRequestsWithDetails();
    IQueryable<LeaveRequest> GetMyLeaveRequests(int userId);

    Task ChangeApprovalStatusAsync(LeaveRequest leaveRequest,
        ApprovalStatuses approvalStatuses = ApprovalStatuses.Pending);

    Task<bool> HasAnyLeaveRequestWithStatusIdAsync(int leaveStatusId);
    Task<bool> HasAnyLeaveRequestWithTypeIdAsync(int leaveTypeId);
}