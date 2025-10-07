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
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new List<Role>
        {
            new() { Id = 1, Name = "Employee", DateCreated = DateTime.Now },
            new() { Id = 2, Name = "Admin", DateCreated = DateTime.Now },
            new() { Id = 3, Name = "HR", DateCreated = DateTime.Now },
            new() { Id = 4, Name = "Manager", DateCreated = DateTime.Now }
        });
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            FullName = "Amir Asefi",
            Mobile = "9123456789",
            CountryCode = 98,
            Email = "amirasefi.info@gmail.com",
            PasswordHash = "AQAAAAEAACcQAAAAECLoPiPh/lUma22MQhg2lQABEBIb/P2SIPqIBNC/Sg5QoQQMJqlXRYMclDsTIJEwIQ==",
            RoleId = 2,
            CreatedAt = DateTime.Now,
            Picture = "/images/user/default_profile.jpg"
        });

        modelBuilder.Entity<LeaveType>().HasData(new List<LeaveType>
        {
            new() { Id = 1, Name = "Annual Leave", DefaultDay = 26 },
            new() { Id = 2, Name = "Sick Leave", DefaultDay = 7 },
            new() { Id = 3, Name = "Hourly Leave", DefaultDay = 0 },
            new() { Id = 4, Name = "Marriage & Bereavement Leave", DefaultDay = 3 },
            new() { Id = 5, Name = "Maternity Leave", DefaultDay = 90 },
            new() { Id = 6, Name = "Unpaid Leave", DefaultDay = 30 }
        });

        //these are default status of each LeaveStatus
        modelBuilder.Entity<LeaveStatus>().HasData(new List<LeaveStatus>
        {
            new() { Id = 1, Name = "Pending" },
            new() { Id = 2, Name = "Approved" },
            new() { Id = 3, Name = "Rejected" },
            new() { Id = 4, Name = "Cancelled" }
        });

        modelBuilder.Entity<LeaveAllocation>().HasData(new List<LeaveAllocation>
        {
            new()
            {
                Id = 1, LeaveTypeId = 1, Period = DateTime.UtcNow.Year, DateCreated = DateTime.UtcNow, TotalDays = 26,
                UserId = 1
            },
            new()
            {
                Id = 2, LeaveTypeId = 2, Period = DateTime.UtcNow.Year, DateCreated = DateTime.UtcNow, TotalDays = 7,
                UserId = 1
            },
            new()
            {
                Id = 3, LeaveTypeId = 3, Period = DateTime.UtcNow.Year, DateCreated = DateTime.UtcNow, TotalDays = 0,
                UserId = 1
            },
            new()
            {
                Id = 4, LeaveTypeId = 4, Period = DateTime.UtcNow.Year, DateCreated = DateTime.UtcNow, TotalDays = 3,
                UserId = 1
            },
            new()
            {
                Id = 5, LeaveTypeId = 5, Period = DateTime.UtcNow.Year, DateCreated = DateTime.UtcNow, TotalDays = 90,
                UserId = 1
            },
            new()
            {
                Id = 6, LeaveTypeId = 6, Period = DateTime.UtcNow.Year, DateCreated = DateTime.UtcNow, TotalDays = 30,
                UserId = 1
            }
        });
    }
}