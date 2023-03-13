using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechHub.Core.Entities;

namespace TechHub.Infrastructure.Data;

internal class AppDbContext : IdentityDbContext<ApplicationUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public DbSet<Post> Posts => Set<Post>();
	public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
    }
}
