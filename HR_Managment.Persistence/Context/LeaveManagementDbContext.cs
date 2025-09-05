using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Context;

public class LeaveManagementDbContext : DbContext, ILeaveManagementDbContext
{
    public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options) : base(options)
    {
    }

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
            entry.Entity.LastModifiedDate = DateTime.Now;
            if (entry.State == EntityState.Added) entry.Entity.DateCreated = DateTime.Now;
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
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            FullName = "Amir Asefi",
            Mobile = "9123456789",
            PasswordHash = "AQAAAAEAACcQAAAAECLoPiPh/lUma22MQhg2lQABEBIb/P2SIPqIBNC/Sg5QoQQMJqlXRYMclDsTIJEwIQ==",
            Role = "Admin",
            CreatedAt = DateTime.Now
        });

        //these are default status of each LeaveStatus
        modelBuilder.Entity<LeaveStatus>().HasData(new LeaveStatus { Id = 1, Name = "Pending" });
        modelBuilder.Entity<LeaveStatus>().HasData(new LeaveStatus { Id = 2, Name = "Approved" });
        modelBuilder.Entity<LeaveStatus>().HasData(new LeaveStatus { Id = 3, Name = "Rejected" });
        modelBuilder.Entity<LeaveStatus>().HasData(new LeaveStatus { Id = 4, Name = "Cancelled" });
    }
}