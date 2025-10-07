using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;

namespace HR_Management.Persistence.Repositories;

public class LeaveStatusRepository : GenericRepository<LeaveStatus>, ILeaveStatusRepository
{
    private readonly LeaveManagementDbContext _context;

    public LeaveStatusRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<LeaveStatus> GetLeaveStatusesWithDetails()
    {
        var leaveStatuses = _context.LeaveStatuses.AsQueryable();
        return leaveStatuses;
    }
}