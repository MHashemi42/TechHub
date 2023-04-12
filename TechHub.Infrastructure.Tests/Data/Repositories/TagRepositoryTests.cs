using Microsoft.EntityFrameworkCore;
using TechHub.Infrastructure.Data.Repositories;
using TechHub.Infrastructure.Data;

namespace TechHub.Infrastructure.Tests.Data.Repositories;

public class TagRepositoryTests
{
    private readonly TagRepository _sut;
    public TagRepositoryTests()
    {
        AppDbContext dbContext = RepositoryTestHelpers.CreateInMemoryDbContext();
        _sut = new TagRepository(dbContext);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Tag_with_a_negative_or_zero_id_is_invalid(int id)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(id));
    }
}
