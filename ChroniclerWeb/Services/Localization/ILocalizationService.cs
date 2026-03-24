using System.Globalization;

namespace ChroniclerWeb.Services.Localization
{
    public interface ILocalizationService
    {
        static abstract string FormatDate(DateTime date, string languageCode);
        static abstract string FormatDateTime(DateTime date, string languageCode);
        static abstract string FormatShortDate(DateTime date, string languageCode);
        static abstract CultureInfo GetCultureInfo(string languageCode);
    }
}