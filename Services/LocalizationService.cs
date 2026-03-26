using System.Globalization;

namespace TheChronicler.Web.Services
{
    public class LocalizationService
    {
        public static readonly Dictionary<string, string> SupportedLanguages = new()
        {
            { "en", "English" },
            { "nl", "Nederlands" },
            { "fr", "Français" },
            { "de", "Deutsch" },
            { "es", "Español" },
            { "it", "Italiano" },
            { "pt", "Português" },
            { "pl", "Polski" },
            { "ru", "Русский" },
            { "ja", "日本語" },
            { "ko", "한국어" },
            { "zh", "中文" }
        };

        public static CultureInfo GetCultureInfo(string languageCode)
        {
            return languageCode.ToLower() switch
            {
                "nl" => new CultureInfo("nl-NL"),
                "fr" => new CultureInfo("fr-FR"),
                "de" => new CultureInfo("de-DE"),
                "es" => new CultureInfo("es-ES"),
                "it" => new CultureInfo("it-IT"),
                "pt" => new CultureInfo("pt-PT"),
                "pl" => new CultureInfo("pl-PL"),
                "ru" => new CultureInfo("ru-RU"),
                "ja" => new CultureInfo("ja-JP"),
                "ko" => new CultureInfo("ko-KR"),
                "zh" => new CultureInfo("zh-CN"),
                _ => new CultureInfo("en-US")
            };
        }

        public static string FormatDate(DateTime date, string languageCode)
        {
            var culture = GetCultureInfo(languageCode);
            return date.ToString("D", culture);
        }

        public static string FormatDateTime(DateTime date, string languageCode)
        {
            var culture = GetCultureInfo(languageCode);
            return date.ToString("f", culture);
        }

        public static string FormatShortDate(DateTime date, string languageCode)
        {
            var culture = GetCultureInfo(languageCode);
            return date.ToString("d", culture);
        }
    }
}
