using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories;

public class LeaveRequestStatusHistoryRepository : GenericRepository<LeaveRequestStatusHistory>,
    ILeaveRequestStatusHistoryRepository
{
    private readonly LeaveManagementDbContext _context;

    public LeaveRequestStatusHistoryRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<LeaveRequestStatusHistory> GetLeaveRequestStatusHistoriesByLeaveRequestId(int leaveRequestId)
    {
        var leaveRequestStatusHistories = _context.LeaveRequestStatusHistories
            .Include(h => h.User)
            .Include(h => h.LeaveRequest)
            .Include(h => h.LeaveStatus)
            .Where(h => h.LeaveRequestId == leaveRequestId);
        return leaveRequestStatusHistories;
    }

    public IQueryable<LeaveRequestStatusHistory> GetLeaveRequestStatusHistoriesWithDetails()
    {
        var leaveRequestStatusHistories = _context.LeaveRequestStatusHistories
            .Include(h => h.User)
            .Include(h => h.LeaveRequest)
            .Include(h => h.LeaveStatus);
        return leaveRequestStatusHistories;
    }
}