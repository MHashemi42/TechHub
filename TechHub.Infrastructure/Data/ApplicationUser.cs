using Microsoft.AspNetCore.Identity;
using TechHub.Core.Entities;
using TechHub.Core.Entities.Base;

namespace TechHub.Infrastructure.Data;

public class ApplicationUser : IdentityUser, IAuditableEntity
{
    public string DisplayName { get; set; } = default!;
    public string? AvatarPath { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
