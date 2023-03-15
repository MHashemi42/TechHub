using Microsoft.AspNetCore.Identity;
using TechHub.Core.Entities;
using TechHub.Core.Entities.Base;

namespace TechHub.Infrastructure.Data;

public class ApplicationUser : IdentityUser, IAuditableEntity
{
    public string DisplayName { get; set; } = default!;
    public string? AvatarPath { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ModifiedOnUtc { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
