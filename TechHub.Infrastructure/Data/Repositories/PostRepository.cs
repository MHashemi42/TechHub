using Microsoft.EntityFrameworkCore;
using TechHub.Core.Entities;
using TechHub.Core.Helpers;
using TechHub.Core.Repositories;

namespace TechHub.Infrastructure.Data.Repositories;

internal class PostRepository : IPostRepository
{
    private readonly AppDbContext _dbContext;

    public PostRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Post post)
    {
        _dbContext.Posts.Add(post);
    }

    public async Task<PagedList<Post>> GetAllAsync(int currentPage, int pageSize,
        CancellationToken cancellationToken = default)
    {
        List<Post> posts = await _dbContext.Posts
            .AsNoTracking()
            .Include(p => p.Tags)
            .OrderByDescending(p => p.DatePublished)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        int count = await _dbContext.Posts.CountAsync(cancellationToken);

        return new PagedList<Post>(posts, count, currentPage, pageSize);
    }

    public async Task<Post?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posts
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Remove(Post post)
    {
        _dbContext.Posts.Remove(post);
    }
}
