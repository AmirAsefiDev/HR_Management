using HR_Management.Application.Persistence.Contracts;
using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    private readonly LeaveManagementDbContext _context;

    public LeaveRequestRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(l => l.LeaveStatus)
            .Include(l => l.LeaveType)
            .FirstOrDefaultAsync(l => l.Id == id);
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = await _context.LeaveRequests
            .Include(l => l.LeaveStatus)
            .Include(l => l.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task ChangeApprovalStatus(LeaveRequest leaveRequest,
        ILeaveRequestRepository.ApprovalStatuses approvalStatus = ILeaveRequestRepository.ApprovalStatuses.Pending)
    {
        leaveRequest.LeaveStatusId = (int)approvalStatus;
        _context.Entry(leaveRequest).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}