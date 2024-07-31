using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using YTodo.Persistence.Entities;

namespace YTodo.Persistence.EntityTypeConfigurations;

public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable("tb_RefreshToken");

        builder.HasKey(x => x.UserId);
        builder.Property(x => x.RefreshToken).HasMaxLength(100);

        builder
            .HasOne<UserEntity>(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}