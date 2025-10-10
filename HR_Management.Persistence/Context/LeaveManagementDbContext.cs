using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Context;

public class LeaveManagementDbContext : DbContext, ILeaveManagementDbContext
{
    public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options) : base(options)
    {
    }

    public virtual DbSet<LeaveRequestStatusHistory> LeaveRequestStatusHistories { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    public virtual DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }
    public virtual DbSet<LeaveType> LeaveTypes { get; set; }
    public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; }
    public virtual DbSet<UserToken> UserTokens { get; set; }


    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.LastModifiedDate = DateTime.UtcNow;
            if (entry.State == EntityState.Added) entry.Entity.DateCreated = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.LastModifiedDate = DateTime.Now;
            if (entry.State == EntityState.Added) entry.Entity.DateCreated = DateTime.Now;
        }

        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
    }
}