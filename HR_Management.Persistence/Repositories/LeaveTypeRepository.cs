using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;

namespace HR_Management.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    private readonly LeaveManagementDbContext _context;

    public LeaveTypeRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<LeaveType> GetLeaveTypesWithDetails()
    {
        var leaveTypes = _context.LeaveTypes.AsQueryable();
        return leaveTypes;
    }
}