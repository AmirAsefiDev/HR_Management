using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class LeaveRequestConfig : IEntityTypeConfiguration<LeaveRequest>
{
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
            .WithMany()
            .HasForeignKey(x => x.LeaveTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.LeaveStatus)
            .WithMany()
            .HasForeignKey(x => x.LeaveStatusId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.DateRequested);
        builder.ToTable(t => t.HasCheckConstraint("CK_LeaveRequest_Date",
            "[StartDate] <= [EndDate]"));
    }
}