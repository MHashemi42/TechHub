namespace TechHub.Core.DTOs;

public class PostSummaryDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string PreviewContent { get; set; }
    public required string ThumbnailPath { get; set; }
    public required string DatePublished { get; set; }

    public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
}
