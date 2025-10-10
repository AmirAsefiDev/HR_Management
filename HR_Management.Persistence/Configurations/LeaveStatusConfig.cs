using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class LeaveStatusConfig : IEntityTypeConfiguration<LeaveStatus>
{
    public void Configure(EntityTypeBuilder<LeaveStatus> builder)
    {
        builder.ToTable("LeaveStatus");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasData(new List<LeaveStatus>
        {
            new() { Id = 1, Name = "Pending" },
            new() { Id = 2, Name = "Approved" },
            new() { Id = 3, Name = "Rejected" },
            new() { Id = 4, Name = "Cancelled" }
        });
    }
}