using Microsoft.EntityFrameworkCore;
using TechHub.Infrastructure.Data.Repositories;
using TechHub.Infrastructure.Data;

namespace TechHub.Infrastructure.Tests.Data.Repositories;

internal class RepositoryTestHelpers
{
    public static AppDbContext CreateInMemoryDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

        return new AppDbContext(dbContextOptionsBuilder.Options);
    }
}
