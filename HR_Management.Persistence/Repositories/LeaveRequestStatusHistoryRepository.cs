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
            .Where(h => h.LeaveRequestId == leaveRequestId)
            .AsQueryable();
        return leaveRequestStatusHistories;
    }

    public IQueryable<LeaveRequestStatusHistory> GetLeaveRequestStatusHistoriesWithDetails()
    {
        var leaveRequestStatusHistories = _context.LeaveRequestStatusHistories
            .Include(h => h.User)
            .Include(h => h.LeaveRequest)
            .Include(h => h.LeaveStatus)
            .AsQueryable();
        return leaveRequestStatusHistories;
    }

    public async Task<bool> HasAnyLeaveHistoryWithRequestIdAsync(int leaveRequestId)
    {
        var isExistsLeaveHistoryWithRequestId =
            await _context.LeaveRequestStatusHistories.AnyAsync(lh => lh.LeaveRequestId == leaveRequestId);
        return isExistsLeaveHistoryWithRequestId;
    }

    public async Task<bool> HasAnyLeaveHistoryWithStatusIdAsync(int leaveStatusId)
    {
        var isExistsLeaveHistoryWithStatusId =
            await _context.LeaveRequests.AnyAsync(lh => lh.LeaveStatusId == leaveStatusId);
        return isExistsLeaveHistoryWithStatusId;
    }
}