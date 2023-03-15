using Microsoft.EntityFrameworkCore;
using TechHub.Core.Entities;
using TechHub.Core.Repositories;

namespace TechHub.Infrastructure.Data.Repositories;

internal class TagRepository : ITagRepository
{
    private readonly AppDbContext _dbContext;

    public TagRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Tag tag)
    {
        _dbContext.Tags.Add(tag);
    }

    public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tags
            .ToListAsync(cancellationToken);
    }

    public async Task<Tag?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tags
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public void Remove(Tag tag)
    {
        _dbContext.Tags.Remove(tag);
    }
}
