using Microsoft.AspNetCore.Identity;
using TechHub.Core.Entities;

namespace TechHub.Infrastructure.Data;

#nullable disable

internal class ApplicationUser : IdentityUser, IApplicationUser
{
    public string DisplayName { get; set; }
    public string AvatarPath { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public ICollection<Post> Posts { get; set; }
}
