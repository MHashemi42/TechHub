using System.Text.RegularExpressions;

namespace TechHub.Infrastructure.Helpers;

public static class HtmlHelpers
{
    public static string GetFirstParagraphFromHtml(string html)
    {
        Match match = RegexHelpers.GetFirstParagraphFromHtml().Match(html);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return string.Empty;
    }
}
