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

    [Fact]
    public async Task GetAllAsync_ShouldThrowArgumentExceptionForNegativeOrZeroCurrentPage()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.GetAllAsync(currentPage: -1, pageSize: 1));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.GetAllAsync(currentPage: 0, pageSize: 1));
    }

    [Fact]
    public async Task GetAllAsync_ShouldThrowArgumentExceptionForNegativeOrZeroPageSize()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetAllAsync(currentPage: 1, pageSize: -1));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.GetAllAsync(currentPage: 1, pageSize: 0));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowArgumentExceptionForNegativeOrZeroId()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(id: -1));
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(id: 0));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnExpectedPagedListCount()
    {
        int totalPostCount = 10;
        DateTimeOffset utcNow = DateTimeOffset.UtcNow;
        for (int i = 0; i < totalPostCount; i++)
        {
            _sut.Add(new Post
            {
                DatePublished = utcNow,
                AuthorId = "authorId",
                Content = "content",
                Slug = "slug",
                ThumbnailPath = "thumbnailPath",
                Title = "title"
            });
        }

        await _unitOfWork.SaveChangesAsync();

        PagedList<Post> posts = await _sut.GetAllAsync(currentPage: 2, pageSize: 3);
        Assert.Equal(expected: 3, actual: posts.Count);

        posts = await _sut.GetAllAsync(currentPage: 2, pageSize: 6);
        Assert.Equal(expected: 4, actual: posts.Count);

        posts = await _sut.GetAllAsync(currentPage: 4, pageSize: 5);
        Assert.Empty(posts);
    }
}
