using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class LeaveTypeConfig : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.ToTable("LeaveType");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.DefaultDay).IsRequired();
        builder.Property(x => x.HoursPerDay).IsRequired().HasDefaultValue(8);

        builder.HasData(new List<LeaveType>
        {
            new() { Id = 1, Name = "Annual Leave", DefaultDay = 26 },
            new() { Id = 2, Name = "Sick Leave", DefaultDay = 7 },
            new() { Id = 3, Name = "Hourly Leave", DefaultDay = 0 },
            new() { Id = 4, Name = "Marriage & Bereavement Leave", DefaultDay = 3 },
            new() { Id = 5, Name = "Maternity Leave", DefaultDay = 90 },
            new() { Id = 6, Name = "Unpaid Leave", DefaultDay = 30 }
        });
    }
}