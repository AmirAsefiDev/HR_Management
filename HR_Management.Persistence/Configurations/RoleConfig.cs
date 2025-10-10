using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).HasMaxLength(50);

        builder.HasData(new List<Role>
        {
            new() { Id = 1, Name = "Employee", DateCreated = DateTime.Now },
            new() { Id = 2, Name = "Admin", DateCreated = DateTime.Now },
            new() { Id = 3, Name = "HR", DateCreated = DateTime.Now },
            new() { Id = 4, Name = "Manager", DateCreated = DateTime.Now }
        });
    }
}