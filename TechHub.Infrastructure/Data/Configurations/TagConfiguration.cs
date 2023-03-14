using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechHub.Core.Entities;

namespace TechHub.Infrastructure.Data.Configurations;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> tag)
    {
        tag.Property(t => t.Name)
           .IsRequired()
           .HasMaxLength(256);

        tag.Property(t => t.Slug)
           .IsRequired()
           .HasMaxLength(256);

        tag.HasMany(t => t.Posts)
           .WithMany(p => p.Tags);
    }
}
