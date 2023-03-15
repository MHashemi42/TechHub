using Microsoft.EntityFrameworkCore;
using TechHub.Core.Entities;
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

    public async Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posts
            .Include(p => p.Tags)
            .ToListAsync(cancellationToken);
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
