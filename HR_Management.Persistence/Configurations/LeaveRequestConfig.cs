using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class LeaveRequestConfig : IEntityTypeConfiguration<LeaveRequest>
{
    private readonly bool _isMemory;

    public LeaveRequestConfig(bool isMemory = false)
    {
        _isMemory = isMemory;
    }

    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.ToTable("LeaveRequest");

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();

        builder.Property(x => x.RequestComments)
            .HasMaxLength(1000);

        builder.Property(x => x.DateRequested)
            .HasColumnType("datetime2");

        builder.HasOne(x => x.LeaveType)
            .WithMany(x => x.LeaveRequests)
            .HasForeignKey(x => x.LeaveTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.LeaveStatus)
            .WithMany(x => x.LeaveRequests)
            .HasForeignKey(x => x.LeaveStatusId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.DateRequested);
        if (!_isMemory) //because in memory database doesn't support this feature.
            builder.ToTable(t => t.HasCheckConstraint("CK_LeaveRequest_Date",
                "[StartDate] <= [EndDate]"));

        builder.HasOne(x => x.User)
            .WithMany(x => x.LeaveRequests)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}