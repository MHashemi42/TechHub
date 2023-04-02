using Microsoft.EntityFrameworkCore;
using TechHub.Infrastructure.Data.Repositories;
using TechHub.Infrastructure.Data;

namespace TechHub.Infrastructure.Tests.Data.Repositories;

public class TagRepositoryTests
{
    private readonly TagRepository _sut;
    public TagRepositoryTests()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
        var dbContext = new AppDbContext(dbContextOptionsBuilder.Options);

        _sut = new TagRepository(dbContext);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowArgumentExceptionForNegativeOrZeroId()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(id: -1));
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(id: 0));
    }
}
