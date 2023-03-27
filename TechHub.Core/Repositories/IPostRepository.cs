using TechHub.Core.Entities;
using TechHub.Core.Helpers;

namespace TechHub.Core.Repositories;

public interface IPostRepository
{
    Task<PagedList<Post>> GetAllAsync(int currentPage, int pageSize,
        CancellationToken cancellationToken = default);
    Task<Post?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Add(Post post);
    void Remove(Post post);
}
