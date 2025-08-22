using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly LeaveManagementDbContext _context;

    public LeaveAllocationRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocation = await _context.LeaveAllocations
            .Include(l => l.LeaveType)
            .FirstOrDefaultAsync(l => l.Id == id);
        return leaveAllocation;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Include(l => l.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }
}