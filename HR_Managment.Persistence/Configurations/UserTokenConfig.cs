using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Persistence.Configurations;

public class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.HashedToken).HasDefaultValue("nvarchar(max)");
        builder.Property(x => x.HashedRefreshToken).HasDefaultValue("nvarchar(max)");

        builder.Property(x => x.TokenExp);
        builder.Property(x => x.RefreshTokenExp);

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}