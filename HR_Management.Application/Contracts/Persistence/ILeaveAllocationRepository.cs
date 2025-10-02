using HR_Management.Domain;

namespace HR_Management.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    IQueryable<LeaveAllocation> GetLeaveAllocationsWithDetails();
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<bool> HasSufficientAllocation(int userId, int leaveTypeId, int requestedAmount);
    Task<LeaveAllocation> GetUserAllocation(int userId, int leaveTypeId, int requestedAmount);
}