using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).HasMaxLength(200);
        builder.Property(x => x.Mobile).HasMaxLength(20);
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.Property(x => x.Role).HasMaxLength(20);
        builder.Property(x => x.PasswordHash).HasDefaultValue("nvarchar(max)");

        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.HasIndex(x => x.Mobile).IsUnique();
    }
}