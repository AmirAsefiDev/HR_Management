using HR_Management.Domain;

namespace HR_Management.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    IQueryable<LeaveAllocation> GetLeaveAllocationsWithDetails();
    Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
    Task<bool> HasSufficientAllocationAsync(int userId, int leaveTypeId, int requestedAmount);
    Task<LeaveAllocation> GetUserAllocationAsync(int userId, int leaveTypeId);
    Task<bool> HasAnyLeaveAllocationWithTypeIdAsync(int leaveTypeId);
    Task DeleteAllAsync();
    Task AddRangeAsync(IEnumerable<LeaveAllocation> allocations);
}