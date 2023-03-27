using AutoMapper;
using Microsoft.Extensions.Logging;
using TechHub.Core.DTOs;
using TechHub.Core.Entities;
using TechHub.Core.Helpers;
using TechHub.Core.Repositories;
using TechHub.Core.Services;

namespace TechHub.Infrastructure.Services;

internal class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ILogger<PostService> _logger;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository,
        ILogger<PostService> logger, IMapper mapper)
    {
        _postRepository = postRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PagedList<PostSummaryDto>>
        GetPostSummaryDtosAsync(int currentPage, int pageSize,
            CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting Posts from database.");
        PagedList<Post> posts = await _postRepository
            .GetAllAsync(currentPage, pageSize,cancellationToken);

        _logger.LogInformation("Mapping Posts to PostSummaryDtos.");
        PagedList<PostSummaryDto> postSummaryDtos = 
            _mapper.Map<PagedList<PostSummaryDto>>(source: posts);

        _logger.LogInformation("Returning PostSummaryDtos.");
        return postSummaryDtos;
    }
}
