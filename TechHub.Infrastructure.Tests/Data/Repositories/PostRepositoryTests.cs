using Microsoft.EntityFrameworkCore;
using TechHub.Core.Entities;
using TechHub.Core.Helpers;
using TechHub.Infrastructure.Data;
using TechHub.Infrastructure.Data.Repositories;

namespace TechHub.Infrastructure.Tests.Data.Repositories;

public class PostRepositoryTests
{
    private readonly PostRepository _sut;
    private readonly UnitOfWork _unitOfWork;
    public PostRepositoryTests()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
        var dbContext = new AppDbContext(dbContextOptionsBuilder.Options);

        _sut = new PostRepository(dbContext);
        _unitOfWork = new UnitOfWork(dbContext);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    public async Task GetAllAsync_ShouldThrowArgumentExceptionForNegativeOrZeroCurrentPageTheory
        (int invalidCurrentPage)
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.GetAllAsync(invalidCurrentPage, pageSize: 1));
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    public async Task GetAllAsync_ShouldThrowArgumentExceptionForNegativeOrZeroPageSizeTheory
        (int invalidPageSize)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetAllAsync(currentPage: 1, invalidPageSize));
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    public async Task GetByIdAsync_ShouldThrowArgumentExceptionForNegativeOrZeroIdTheory
        (int invalidId)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(invalidId));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnExpectedPagedList()
    {
        int totalPostCount = 15;
        var tempPost = new Post
        {
            DatePublished = DateTimeOffset.UtcNow,
            AuthorId = "authorId",
            Content = "content",
            Slug = "slug",
            ThumbnailPath = "thumbnailPath",
            Title = "title"
        };
        for (int i = 0; i < totalPostCount; i++)
        {
            _sut.Add(tempPost);
        }

        await _unitOfWork.SaveChangesAsync();

        PagedList<Post> posts = await _sut.GetAllAsync(currentPage: 2, pageSize: 5);
        Assert.Equal(expected: 5, actual: posts.Count);

        posts = await _sut.GetAllAsync(currentPage: 4, pageSize: 5);
        Assert.Empty(posts);
    }
}
