using HR_Management.Domain;

namespace HR_Management.Application.Contracts.Persistence;

public interface ILeaveRequestStatusHistoryRepository : IGenericRepository<LeaveRequestStatusHistory>
{
    IQueryable<LeaveRequestStatusHistory> GetLeaveRequestStatusHistoriesByLeaveRequestId(int leaveRequestId);
    IQueryable<LeaveRequestStatusHistory> GetLeaveRequestStatusHistoriesWithDetails();

    Task<bool> HasAnyLeaveHistoryWithRequestIdAsync(int leaveRequestId);
    Task<bool> HasAnyLeaveHistoryWithStatusIdAsync(int leaveStatusId);
}