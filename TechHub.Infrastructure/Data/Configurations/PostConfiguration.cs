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
            .HasMaxLength(150);

        post.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(150);

        post.Property(p => p.Content)
            .IsRequired();

        post.Property(p => p.ThumbnailPath)
            .IsRequired()
            .HasMaxLength(500);

        post.HasOne(p => p.Author)
            .WithMany(a => a.Posts);

        post.HasMany(p => p.Tags)
            .WithMany(t => t.Posts);
    }
}
