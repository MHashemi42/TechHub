﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> entries = ChangeTracker
                        .Entries()
                        .Where(e => e.Entity is IEntityBase &&
                                (e.State == EntityState.Added ||
                                e.State == EntityState.Modified));

        foreach (EntityEntry entityEntry in entries)
        {
            DateTime utcNow = DateTime.UtcNow;

            ((IEntityBase)entityEntry.Entity).DateModified = utcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((IEntityBase)entityEntry.Entity).DateCreated = utcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}