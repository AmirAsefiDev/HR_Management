using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Contracts.Persistence.Context;

public interface ILeaveManagementDbContext
{
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveStatus> LeaveStatuses { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new());

    int SaveChanges();
}