using Microsoft.AspNetCore.Mvc;
using Moq;
using TechHub.Core.DTOs;
using TechHub.Core.Helpers;
using TechHub.Core.Services;
using TechHub.Web.Pages;

namespace TechHub.Web.Tests.Pages;

public class IndexTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Page_with_a_negative_or_zero_number_redirect_to_notfound_page(int pageNumber)
    {
        var postServiceMock = new Mock<IPostService>();
        var indexPage = new IndexModel(postServiceMock.Object)
        {
            CurrentPage = pageNumber
        };
        CancellationToken token = CancellationToken.None;

        IActionResult result = await indexPage.OnGetAsync(token);

        RedirectToPageResult redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("NotFound", redirectResult.PageName);
    }

    [Fact]
    public async Task Empty_page_list_redirect_to_notfound_page()
    {
        var postServiceMock = new Mock<IPostService>();
        postServiceMock.Setup(p =>
            p.GetPostSummaryDtosAsync(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(new PagedList<PostSummaryDto>(
                items: Enumerable.Empty<PostSummaryDto>(),
                count: 0,
                pageNumber: 1,
                pageSize: 1));

        var indexPage = new IndexModel(postServiceMock.Object);
        CancellationToken token = CancellationToken.None;

        IActionResult result = await indexPage.OnGetAsync(token);

        RedirectToPageResult redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("NotFound", redirectResult.PageName);
    }
}
