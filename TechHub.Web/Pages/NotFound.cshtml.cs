using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace TechHub.Web.Pages;

public class NotFoundModel : PageModel
{
    public void OnGet()
    {
        HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
    }
}
