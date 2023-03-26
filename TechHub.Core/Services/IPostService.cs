using TechHub.Core.DTOs;

namespace TechHub.Core.Services;

public interface IPostService
{
    Task<IEnumerable<PostSummaryDto>>
        GetPostSummaryDtosAsync(CancellationToken cancellationToken = default);
}
