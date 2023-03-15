using TechHub.Core.Entities;

namespace TechHub.Core.Repositories;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Tag?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Add(Tag tag);
    void Remove(Tag tag);
}
