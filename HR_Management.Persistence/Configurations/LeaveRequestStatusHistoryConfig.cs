using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class LeaveRequestStatusHistoryConfig : IEntityTypeConfiguration<LeaveRequestStatusHistory>
{
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
    }
}