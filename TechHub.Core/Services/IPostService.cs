using TechHub.Core.DTOs;
using TechHub.Core.Helpers;

namespace TechHub.Core.Services;

public interface IPostService
{
    Task<PagedList<PostSummaryDto>>
        GetPostSummaryDtosAsync(int currentPage, int pageSize,
            CancellationToken cancellationToken = default);
}
