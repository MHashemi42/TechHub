using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechHub.Core.DTOs;
using TechHub.Core.Helpers;
using TechHub.Core.Services;

namespace TechHub.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IPostService _postService;
    public PagedList<PostSummaryDto> PostSummaryDtos { get; set; }
        = new PagedList<PostSummaryDto>();

    public IndexModel(IPostService postService)
    {
        _postService = postService;
    }

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        if (CurrentPage < 1)
        {
            return RedirectToPage("NotFound");
        }

        PostSummaryDtos = await _postService
            .GetPostSummaryDtosAsync(CurrentPage, pageSize: 10, cancellationToken);
        if (!PostSummaryDtos.Any())
        {
            return RedirectToPage("NotFound");
        }

        return Page();
    }
}
