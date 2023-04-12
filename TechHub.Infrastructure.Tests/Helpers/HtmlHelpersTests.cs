namespace TechHub.Infrastructure.Tests.Helpers;

public class HtmlHelpersTests
{
    [Theory]
    [ClassData(typeof(GetFirstParagraphFromHtmlTestData))]
    public void Can_extract_the_first_paragraph_from_html(string expected, string html)
    {
        string firstParagraph = HtmlHelpers.GetFirstParagraphFromHtml(html);
        Assert.Equal(expected, firstParagraph);
    }

    public class GetFirstParagraphFromHtmlTestData : TheoryData<string, string>
    {
        public GetFirstParagraphFromHtmlTestData()
        {
            string expected = string.Empty;
            string html = string.Empty;
            Add(expected, html);

            expected = "Just a paragraph";
            html = $"<p>{expected}</p>";
            Add(expected, html);

            expected = string.Empty;
            html = "only text, not html";
            Add(expected, html);

            expected = "first paragraph";
            html = $"<p>{expected}</p><p>second paragraph</p>";
            Add(expected, html);

            expected = "a paragraph inside of a div element";
            html = $"<div><p>{expected}</p></div><p>another paragraph</p>";
            Add(expected, html);

            expected = "a paragraph with <i>italic</i> element";
            html = $"<p>{expected}</p>";
            Add(expected, html);

            expected = "a paragraph with class attribute";
            html = $"<p class=\"h4\">{expected}</p>";
            Add(expected, html);
        }
    }
}
