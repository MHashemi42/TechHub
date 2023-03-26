using AutoMapper;
using Microsoft.Extensions.Logging;
using TechHub.Core.DTOs;
using TechHub.Core.Entities;
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

    public async Task<IEnumerable<PostSummaryDto>> GetPostSummaryDtosAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting Posts from database.");
        IEnumerable<Post> posts = await _postRepository.GetAllAsync(cancellationToken);

        _logger.LogInformation("Mapping Posts to PostSummaryDto.");
        IEnumerable<PostSummaryDto> postSummaryDtos = 
            _mapper.Map<IEnumerable<PostSummaryDto>>(source: posts);

        _logger.LogInformation("Returning postSummaryDtos.");
        return postSummaryDtos;
    }
}
