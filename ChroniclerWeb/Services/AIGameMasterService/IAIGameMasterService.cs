namespace ChroniclerWeb.Services.AIGameMasterService
{
    public interface IAIGameMasterService
    {
        Task<string> GenerateCharacterPortraitAsync(string race, string characterClass, string gender, string style = "fantasy digital art");
        Task<string> GenerateCombatNarrationAsync(string attackerName, string targetName, int damage, bool isCritical);
        Task<string> GenerateImageAsync(string prompt);
        Task<string> GenerateLogoAsync(string name, string theme);
        Task<string> GenerateLootAsync(string encounterType, string playerLevel);
        Task<string> GenerateNPCResponseAsync(string npcName, string npcPersonality, string playerMessage, string campaignContext);
        Task<string> GenerateQuestAsync(string campaignContext, string playerLevel, string region);
        Task<string> GenerateRandomEncounterAsync(string biome, string playerLevel);
        Task<string> GenerateTextAsync(string prompt);
    }
}