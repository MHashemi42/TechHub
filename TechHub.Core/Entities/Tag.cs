using TechHub.Core.Entities.Base;

namespace TechHub.Core.Entities;

public class Tag : EntityBase
{
    public required string Name { get; set; }
    public required string Slug { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
