using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
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

    public IQueryable<LeaveRequest> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = _context.LeaveRequests
            .Include(l => l.LeaveStatus)
            .Include(l => l.LeaveType)
            .Include(l => l.User);
        return leaveRequests;
    }

    public IQueryable<LeaveRequest> GetMyLeaveRequests(int userId)
    {
        var leaveRequests = _context.LeaveRequests
            .Include(l => l.LeaveType)
            .Include(l => l.LeaveStatus)
            .Where(l => l.UserId == userId);
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