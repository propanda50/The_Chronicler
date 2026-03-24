using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using Microsoft.EntityFrameworkCore;


namespace ChroniclerWeb.Services.achievement
{
   
    public class AchievementService : IAchievementService
    {
        private readonly ApplicationDbContext _context;

        public AchievementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            return await _context.Achievements.ToListAsync();
        }

        public async Task<List<PlayerAchievement>> GetPlayerAchievementsAsync(string playerId)
        {
            return await _context.PlayerAchievements
                .Include(pa => pa.Achievement)
                .Where(pa => pa.PlayerId == playerId)
                .ToListAsync();
        }

        public async Task<ProgressResult> UpdateProgressAsync(string playerId, string achievementName, int amount = 1)
        {
            var achievement = await GetAchievementByNameAsync(achievementName);
            if (achievement == null)
                return new ProgressResult { Success = false, Message = "Achievement not found" };

            var playerAchievement = await _context.PlayerAchievements
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerId && pa.AchievementId == achievement.Id);

            if (playerAchievement == null)
            {
                playerAchievement = new PlayerAchievement
                {
                    PlayerId = playerId,
                    AchievementId = achievement.Id,
                    CurrentProgress = 0,
                    FirstProgressAt = DateTime.UtcNow
                };
                _context.PlayerAchievements.Add(playerAchievement);
            }

            if (playerAchievement.IsCompleted)
                return new ProgressResult { Success = false, Message = "Already completed", IsCompleted = true };

            playerAchievement.CurrentProgress += amount;

            // Check if completed
            var targetProgress = achievement.RequiredCount ?? 1;
            if (playerAchievement.CurrentProgress >= targetProgress)
            {
                playerAchievement.IsCompleted = true;
                playerAchievement.CompletedAt = DateTime.UtcNow;
                playerAchievement.CurrentProgress = targetProgress;

                return new ProgressResult
                {
                    Success = true,
                    Message = $"Achievement Unlocked: {achievement.Name}!",
                    IsCompleted = true,
                    XP = achievement.XP,
                    Achievement = achievement
                };
            }

            await _context.SaveChangesAsync();

            return new ProgressResult
            {
                Success = true,
                Message = $"Progress: {playerAchievement.CurrentProgress}/{targetProgress}",
                IsCompleted = false,
                Progress = playerAchievement.CurrentProgress,
                Required = targetProgress
            };
        }

        public async Task<Achievement?> GetAchievementByNameAsync(string name)
        {
            return await _context.Achievements
                .FirstOrDefaultAsync(a => a.Name == name);
        }

        // Initialize achievements in database
        public async Task InitializeAchievementsAsync()
        {
            foreach (var (name, desc, icon, category, xp, required) in AchievementList.All)
            {
                if (!await _context.Achievements.AnyAsync(a => a.Name == name))
                {
                    _context.Achievements.Add(new Achievement
                    {
                        Name = name,
                        Description = desc,
                        Icon = icon,
                        Category = category,
                        XP = xp,
                        RequiredCount = required,
                        Type = required.HasValue ? AchievementType.Progress : AchievementType.FirstTime
                    });
                }
            }
            await _context.SaveChangesAsync();
        }
    }

    public class ProgressResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public bool IsCompleted { get; set; }
        public int XP { get; set; }
        public int Progress { get; set; }
        public int Required { get; set; }
        public Achievement? Achievement { get; set; }
    }
}
