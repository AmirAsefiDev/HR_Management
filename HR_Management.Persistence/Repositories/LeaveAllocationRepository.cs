using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
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

    public async Task<bool> HasSufficientAllocation(int userId, int leaveTypeId, int requestedAmount)
    {
        var allocation = await _context.LeaveAllocations
            .FirstOrDefaultAsync(la => la.UserId == userId && la.LeaveTypeId == leaveTypeId);
        if (allocation == null)
            return false;
        return allocation.RemainingDays >= requestedAmount;
    }

    public async Task<LeaveAllocation> GetUserAllocation(int userId, int leaveTypeId, int requestedAmount)
    {
        var userAllocation = await _context.LeaveAllocations
            .FirstOrDefaultAsync(la => la.UserId == userId && la.LeaveTypeId == leaveTypeId);
        return userAllocation;
    }

    public IQueryable<LeaveAllocation> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = _context.LeaveAllocations
            .Include(l => l.LeaveType);
        return leaveAllocations;
    }
}