namespace TechHub.Web.Models;

public record Pagination(string PageName, int CurrentPage, bool HasPrevious, bool HasNext);