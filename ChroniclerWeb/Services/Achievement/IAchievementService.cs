using ChroniclerWeb.Models;

namespace ChroniclerWeb.Services.achievement
{
    public interface IAchievementService
    {
        Task<List<Achievement>> GetAllAchievementsAsync();
        Task<List<PlayerAchievement>> GetPlayerAchievementsAsync(string playerId);
        Task<ProgressResult> UpdateProgressAsync(string playerId, string achievementName, int amount = 1);
        Task<Achievement?> GetAchievementByNameAsync(string name);
        Task InitializeAchievementsAsync();
    }
}
