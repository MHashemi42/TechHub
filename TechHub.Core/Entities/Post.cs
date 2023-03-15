using TechHub.Core.Entities.Base;

namespace TechHub.Core.Entities;

public class Post : EntityBase
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string ThumbnailPath { get; set; } = default!;
    public DateTime DatePublished { get; set; }
    public string AuthorId { get; set; } = default!;

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
