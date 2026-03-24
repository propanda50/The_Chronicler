namespace ChroniclerWeb.Services.RandomNameGenerator
{
    public interface IRandomNameGeneratorService
    {
        static abstract string GenerateName(string race = "");
        static abstract string GeneratePlaceName();
    }
}