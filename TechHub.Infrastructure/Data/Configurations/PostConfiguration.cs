using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechHub.Core.Entities;

namespace TechHub.Infrastructure.Data.Configurations;

internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> post)
    {
        post.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(256);

        post.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(256);

        post.Property(p => p.Content)
            .IsRequired();

        post.Property(p => p.ThumbnailPath)
            .IsRequired()
            .HasMaxLength(512);

        post.HasMany(p => p.Tags)
            .WithMany(t => t.Posts);
    }
}
