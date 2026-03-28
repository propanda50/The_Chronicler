using Ganss.Xss;

namespace ChroniclerWeb.Services.HtmlSanitizerHelper
{
    public static class HtmlSanitizerHelper
    {
        private static readonly HtmlSanitizer _sanitizer;

        static HtmlSanitizerHelper()
        {
            _sanitizer = new HtmlSanitizer();

            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedTags.Add("b");
            _sanitizer.AllowedTags.Add("i");
            _sanitizer.AllowedTags.Add("strong");
            _sanitizer.AllowedTags.Add("em");
            _sanitizer.AllowedTags.Add("p");
            _sanitizer.AllowedTags.Add("ul");
            _sanitizer.AllowedTags.Add("ol");
            _sanitizer.AllowedTags.Add("li");
            _sanitizer.AllowedTags.Add("br");
            _sanitizer.AllowedTags.Add("h1");
            _sanitizer.AllowedTags.Add("h2");
            _sanitizer.AllowedTags.Add("h3");
            _sanitizer.AllowedTags.Add("blockquote");

            _sanitizer.AllowedAttributes.Clear();
        }

        public static string Sanitize(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            return _sanitizer.Sanitize(html);
        }
    }
}
