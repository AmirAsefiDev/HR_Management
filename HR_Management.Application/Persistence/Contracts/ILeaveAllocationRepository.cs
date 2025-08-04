using HR_Management.Domain;

namespace HR_Management.Application.Persistence.Contracts;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
}