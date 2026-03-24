namespace ChroniclerWeb.Services.AIGeneration
{
    public interface IAIGenerationService
    {
        string GenerateRandomBackground();
        string GenerateRandomCampaignLogo(string theme = "");
        string GenerateRandomCharacterName(string race = "", string characterClass = "");
        string GenerateRandomCharacterPortrait(string race, string characterClass, string gender = "");
        string GenerateRandomClassName(string race = "");
        string GenerateRandomPersonalityTrait();
    }
}