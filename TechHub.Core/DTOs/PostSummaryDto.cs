namespace TechHub.Core.DTOs;

public class PostSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string PreviewContent { get; set; } = default!;
    public string ThumbnailPath { get; set; } = default!;
    public string DatePublished { get; set; } = default!;

    public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
}
