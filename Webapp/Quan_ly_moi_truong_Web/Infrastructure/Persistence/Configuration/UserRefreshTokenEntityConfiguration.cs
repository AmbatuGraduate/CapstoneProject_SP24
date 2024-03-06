using Domain.Entities.UserRefreshToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class UserRefreshTokenEntityConfiguration : IEntityTypeConfiguration<UserRefreshTokens>
    {
        public void Configure(EntityTypeBuilder<UserRefreshTokens> builder)
        {
            ConfigureUserRefreshTokensTable(builder);
        }

        private void ConfigureUserRefreshTokensTable(EntityTypeBuilder<UserRefreshTokens> builder)
        {
            builder.ToTable("UserRefreshTokens")
                   .HasKey(userRefreshTokens => userRefreshTokens.UserRefreshTokenId);

            builder.Property(userRefreshTokens => userRefreshTokens.RefreshToken)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(userRefreshTokens => userRefreshTokens.Expire)
                   .IsRequired();
            builder.Property(userRefreshTokens => userRefreshTokens.CreateBy)
                   .HasMaxLength(50);
            builder.Property(userRefreshTokens => userRefreshTokens.UpdateBy)
                   .HasMaxLength(50);
        }
    }
}