using TechHub.Core.Entities.Base;

namespace TechHub.Core.Entities;

public class Post : EntityBase
{
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string Content { get; set; }
    public required string ThumbnailPath { get; set; }
    public DateTimeOffset DatePublished { get; set; }
    public required string AuthorId { get; set; }

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
