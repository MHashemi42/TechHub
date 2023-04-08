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
        AppDbContext dbContext = RepositoryTestHelpers.CreateInMemoryDbContext();
        _sut = new PostRepository(dbContext);
        _unitOfWork = new UnitOfWork(dbContext);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetAllAsync_NegativeOrZeroCurrentPage_ThrowsArgumentException(int currentPage)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetAllAsync(currentPage, pageSize: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetAllAsync_NegativeOrZeroPageSize_ThrowsArgumentException(int pageSize)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetAllAsync(currentPage: 1, pageSize));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetByIdAsync_NegativeOrZeroId_ThrowsArgumentException(int id)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(id));
    }

    [Theory]
    [InlineData(2, 3, 3)]
    [InlineData(2, 6, 4)]
    [InlineData(4, 5, 0)]
    public async Task GetAllAsync_Pagination_ReturnsExpectedCount(int currentPage,
        int pageSize, int expectedCount)
    {
        const int TOTAL_POST_COUNT = 10;
        for (int i = 0; i < TOTAL_POST_COUNT; i++)
        {
            Post post = CreateTempPost();
            _sut.Add(post);
        }
        await _unitOfWork.SaveChangesAsync();

        PagedList<Post> posts = await _sut.GetAllAsync(currentPage, pageSize);
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
}
