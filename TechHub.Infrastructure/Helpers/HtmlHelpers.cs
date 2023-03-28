using HtmlAgilityPack;

namespace TechHub.Infrastructure.Helpers;

public static class HtmlHelpers
{
    public static string GetFirstParagraphFromHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return string.Empty;
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        HtmlNode firstParagraph = doc.DocumentNode.SelectSingleNode("//p[1]");

        return firstParagraph?.InnerHtml ?? string.Empty;
    }
}
