using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechHub.Infrastructure.Data.Configurations;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> user)
    {
        user.HasMany(u => u.Posts)
            .WithOne()
            .HasForeignKey(p => p.AuthorId);

        user.Property(u => u.DisplayName)
            .IsRequired()
            .HasMaxLength(256);

        user.Property(u => u.AvatarPath)
            .HasMaxLength(512);
    }
}
