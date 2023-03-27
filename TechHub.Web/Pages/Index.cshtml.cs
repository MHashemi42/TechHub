using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechHub.Core.DTOs;
using TechHub.Core.Helpers;
using TechHub.Core.Services;
using TechHub.Web.Models;

namespace TechHub.Web.Pages;

#nullable disable warnings

public class IndexModel : PageModel
{
    private readonly IPostService _postService;

    public IndexModel(IPostService postService)
    {
        _postService = postService;
    }

    public PagedList<PostSummaryDto> PostSummaryDtos { get; set; }

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    public Pagination Pagination { get; set; }

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

        Pagination = new Pagination(PageName: "Index", CurrentPage,
            PostSummaryDtos.MetaData.HasPrevious, PostSummaryDtos.MetaData.HasNext);

        return Page();
    }
}
