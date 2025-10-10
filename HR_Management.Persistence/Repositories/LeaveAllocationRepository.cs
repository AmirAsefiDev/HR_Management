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

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id)
    {
        var leaveAllocation = await _context.LeaveAllocations
            .Include(l => l.LeaveType)
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.Id == id);
        return leaveAllocation;
    }

    public async Task<bool> HasSufficientAllocationAsync(int userId, int leaveTypeId, int requestedAmount)
    {
        var allocation = await _context.LeaveAllocations
            .FirstOrDefaultAsync(la => la.UserId == userId && la.LeaveTypeId == leaveTypeId);
        if (allocation == null)
            return false;
        return allocation.RemainingDays >= requestedAmount;
    }

    public async Task<LeaveAllocation> GetUserAllocationAsync(int userId, int leaveTypeId, int requestedAmount)
    {
        var userAllocation = await _context.LeaveAllocations
            .FirstOrDefaultAsync(la => la.UserId == userId && la.LeaveTypeId == leaveTypeId);
        return userAllocation;
    }

    public IQueryable<LeaveAllocation> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = _context.LeaveAllocations
            .Include(l => l.LeaveType)
            .Include(l => l.User)
            .AsQueryable();
        return leaveAllocations;
    }

    public async Task<bool> HasAnyLeaveAllocationWithTypeIdAsync(int leaveTypeId)
    {
        var isExistsLeaveAllocationWithTypeId =
            await _context.LeaveAllocations.AnyAsync(lr => lr.LeaveTypeId == leaveTypeId);
        return isExistsLeaveAllocationWithTypeId;
    }

    public async Task DeleteAllAsync()
    {
        _context.LeaveAllocations.RemoveRange(_context.LeaveAllocations);
        await _context.SaveChangesAsync();
    }

    public async Task AddRange(IEnumerable<LeaveAllocation> allocations)
    {
        await _context.LeaveAllocations.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }
}