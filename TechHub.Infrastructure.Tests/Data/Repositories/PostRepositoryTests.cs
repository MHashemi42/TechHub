using Microsoft.EntityFrameworkCore;
using TechHub.Core.Entities;
using TechHub.Core.Helpers;
using TechHub.Infrastructure.Data;
using TechHub.Infrastructure.Data.Repositories;

namespace TechHub.Infrastructure.Tests.Data.Repositories;

public class PostRepositoryTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetAllAsync_NegativeOrZeroCurrentPage_ThrowsArgumentException(int currentPage)
    {
        AppDbContext dbContext = CreateInMemoryDbContext();
        PostRepository sut = CreateDefaultPostRepository(dbContext);

        await Assert.ThrowsAsync<ArgumentException>(() => 
            sut.GetAllAsync(currentPage, pageSize: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetAllAsync_NegativeOrZeroPageSize_ThrowsArgumentException(int pageSize)
    {
        AppDbContext dbContext = CreateInMemoryDbContext();
        PostRepository sut = CreateDefaultPostRepository(dbContext);

        await Assert.ThrowsAsync<ArgumentException>(() => 
            sut.GetAllAsync(currentPage: 1, pageSize));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetByIdAsync_NegativeOrZeroId_ThrowsArgumentException(int id)
    {
        AppDbContext dbContext = CreateInMemoryDbContext();
        PostRepository sut = CreateDefaultPostRepository(dbContext);

        await Assert.ThrowsAsync<ArgumentException>(() => sut.GetByIdAsync(id));
    }

    [Theory]
    [InlineData(2, 3, 3)]
    [InlineData(2, 6, 4)]
    [InlineData(4, 5, 0)]
    public async Task GetAllAsync_Pagination_ReturnsExpectedCount(int currentPage,
        int pageSize, int expectedCount)
    {
        AppDbContext dbContext = CreateInMemoryDbContext();
        PostRepository sut = CreateDefaultPostRepository(dbContext);
        UnitOfWork unitOfWork = CreateDefaultUnitOfWork(dbContext);
        const int TOTAL_POST_COUNT = 10;
        for (int i = 0; i < TOTAL_POST_COUNT; i++)
        {
            Post post = CreateTempPost();
            sut.Add(post);
        }
        await unitOfWork.SaveChangesAsync();

        PagedList<Post> posts = await sut.GetAllAsync(currentPage, pageSize);
        int actualCount = posts.Count;

        Assert.Equal(expectedCount, actualCount);
    }

    private static Post CreateTempPost()
    {
        return new Post
        {
            DatePublished = DateTimeOffset.UtcNow,
            AuthorId = nameof(Post.AuthorId),
            Content = nameof(Post.Content),
            Slug = nameof(Post.Slug),
            ThumbnailPath = nameof(Post.ThumbnailPath),
            Title = nameof(Post.Title)
        };
    }

    private static AppDbContext CreateInMemoryDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

        return new AppDbContext(dbContextOptionsBuilder.Options);
    }

    private static PostRepository CreateDefaultPostRepository(AppDbContext dbContext)
    {
        return new PostRepository(dbContext);
    }

    private static UnitOfWork CreateDefaultUnitOfWork(AppDbContext dbContext)
    {
        return new UnitOfWork(dbContext);
    }
}
