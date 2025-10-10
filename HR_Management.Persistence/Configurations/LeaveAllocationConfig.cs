using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class LeaveAllocationConfig : IEntityTypeConfiguration<LeaveAllocation>
{
    public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
    {
        builder.ToTable("LeaveAllocation");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TotalDays).IsRequired();
        builder.Property(x => x.Period).IsRequired();

        builder.HasOne(x => x.LeaveType)
            .WithMany(x => x.LeaveAllocations)
            .HasForeignKey(x => x.LeaveTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User)
            .WithMany(x => x.LeaveAllocations)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.RemainingDays)
            .HasComputedColumnSql("[TotalDays] - [UsedDays]", true);

        builder.HasData(new List<LeaveAllocation>
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