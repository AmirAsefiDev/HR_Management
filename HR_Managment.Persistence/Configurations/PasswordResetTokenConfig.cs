using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class PasswordResetTokenConfig : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable("PasswordResetToken");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.User)
            .WithMany(u => u.PasswordResetTokens)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(p => p.Token).HasDefaultValue("nvarchar(max)");
        builder.Property(p => p.CreateAt).HasDefaultValue(DateTime.UtcNow);
        builder.Property(p => p.IsUsed).HasDefaultValue(false);
    }
}