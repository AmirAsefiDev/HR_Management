using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class LeaveRequestStatusHistoryConfig : IEntityTypeConfiguration<LeaveRequestStatusHistory>
{
    private readonly bool _isMemory;

    public LeaveRequestStatusHistoryConfig(bool isMemory = false)
    {
        _isMemory = isMemory;
    }

    public void Configure(EntityTypeBuilder<LeaveRequestStatusHistory> builder)
    {
        builder.ToTable("LeaveRequestStatusHistory");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.LeaveRequest)
            .WithMany(x => x.LeaveRequestStatusHistories)
            .HasForeignKey(x => x.LeaveRequestId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.LeaveStatus)
            .WithMany(x => x.LeaveRequestStatusHistories)
            .HasForeignKey(x => x.LeaveStatusId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User)
            .WithMany(x => x.LeaveRequestStatusHistories)
            .HasForeignKey(x => x.ChangedBy)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Comment).HasColumnType("nvarchar(500)");

        if (_isMemory)
            builder.HasData(new LeaveRequestStatusHistory
            {
                Id = 1,
                DateCreated = DateTime.UtcNow,
                LeaveRequestId = 1,
                LeaveStatusId = 1,
                Comment = "Test"
            });
    }
}