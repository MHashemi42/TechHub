using TechHub.Core.Entities;

namespace TechHub.Core.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Post?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Add(Post post);
    void Remove(Post post);
}
