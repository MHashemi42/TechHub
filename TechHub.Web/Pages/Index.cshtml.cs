using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechHub.Core.DTOs;
using TechHub.Core.Services;

namespace TechHub.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IPostService _postService;
    public IEnumerable<PostSummaryDto> PostSummaryDtos { get; set; }
        = Enumerable.Empty<PostSummaryDto>();
    public IndexModel(IPostService postService)
    {
        _postService = postService;
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        PostSummaryDtos = await _postService.GetPostSummaryDtosAsync(cancellationToken);
        if (!PostSummaryDtos.Any())
        {
            return RedirectToPage("NotFound");
        }

        return Page();
    }
}
