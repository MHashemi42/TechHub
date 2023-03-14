namespace TechHub.Core.Entities;

public class Tag : EntityBase
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
