using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence;

public class LeaveManagementDbContext : DbContext
{
    public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options) : base(options)
    {
    }

    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveStatus> LeaveStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
        SeedData(modelBuilder);
    }

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

    private void SeedData(ModelBuilder modelBuilder)
    {
        //these are default status of each LeaveStatus
        modelBuilder.Entity<LeaveStatus>().HasData(new LeaveStatus { Id = 1, Name = "Pending" });
        modelBuilder.Entity<LeaveStatus>().HasData(new LeaveStatus { Id = 2, Name = "Approved" });
        modelBuilder.Entity<LeaveStatus>().HasData(new LeaveStatus { Id = 3, Name = "Rejected" });
        modelBuilder.Entity<LeaveStatus>().HasData(new LeaveStatus { Id = 4, Name = "Cancelled" });
    }
}