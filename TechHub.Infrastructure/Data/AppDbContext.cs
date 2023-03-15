using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TechHub.Core.Entities;
using TechHub.Core.Entities.Base;

namespace TechHub.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> entries = ChangeTracker
                        .Entries()
                        .Where(e => e.Entity is IAuditableEntity &&
                                (e.State == EntityState.Added ||
                                e.State == EntityState.Modified));

        foreach (EntityEntry entityEntry in entries)
        {
            DateTime utcNow = DateTime.UtcNow;

            ((IAuditableEntity)entityEntry.Entity).ModifiedOnUtc = utcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((IAuditableEntity)entityEntry.Entity).CreatedOnUtc = utcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
