using HR_Management.Domain;

namespace HR_Management.Application.Contracts.Persistence;

public interface ILeaveRequestStatusHistoryRepository : IGenericRepository<LeaveRequestStatusHistory>
{
    IQueryable<LeaveRequestStatusHistory> GetLeaveRequestStatusHistoriesByLeaveRequestId(int leaveRequestId);
}