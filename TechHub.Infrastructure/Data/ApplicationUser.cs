using Microsoft.AspNetCore.Identity;
using TechHub.Core.Entities;

namespace TechHub.Infrastructure.Data;

internal class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = default!;
    public string? AvatarPath { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
