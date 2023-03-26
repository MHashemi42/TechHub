using System.Text.RegularExpressions;

namespace TechHub.Infrastructure.Helpers;

internal static class HtmlHelpers
{
    public static string GetFirstParagraph(string html)
    {
        Match match = RegexHelpers.GetFirstParagraphFromHtml().Match(html);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return string.Empty;
    }
}
