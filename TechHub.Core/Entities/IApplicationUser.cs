namespace TechHub.Core.Entities;

public interface IApplicationUser : IEntityBase
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string? AvatarPath { get; set; }

    public ICollection<Post> Posts { get; set; }
}