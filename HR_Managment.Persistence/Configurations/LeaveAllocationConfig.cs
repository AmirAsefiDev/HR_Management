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

        builder.Property(x => x.NumberOfDays).IsRequired();
        builder.Property(x => x.Period).IsRequired();

        builder.HasOne(x => x.LeaveType)
            .WithMany()
            .HasForeignKey(x => x.LeaveTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}