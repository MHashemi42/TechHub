using System.Text.RegularExpressions;

namespace TechHub.Infrastructure.Helpers;

internal static partial class RegexHelpers
{
    [GeneratedRegex("<p>\\s*(.+?)\\s*</p>")]
    public static partial Regex GetFirstParagraphFromHtml();
}
