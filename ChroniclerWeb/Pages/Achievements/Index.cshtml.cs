using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Achievements
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAchievementService _achievementService;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IAchievementService achievementService)
        {
            _context = context;
            _userManager = userManager;
            _achievementService = achievementService;
        }

        public List<Achievement> AllAchievements { get; set; } = new();
        public Dictionary<int, PlayerAchievement> PlayerProgress { get; set; } = new();
        public IEnumerable<IGrouping<string, Achievement>> AchievementsByCategoryList { get; set; } = Enumerable.Empty<IGrouping<string, Achievement>>();
        public int CompletedCount { get; set; }
        public int TotalXP { get; set; }
        public int CompletionPercent { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User)!;

            // Ensure achievements exist
            await _achievementService.InitializeAchievementsAsync();

            // Get all achievements
            AllAchievements = await _achievementService.GetAllAchievementsAsync();

            // Get player progress
            var progress = await _achievementService.GetPlayerAchievementsAsync(userId);
            PlayerProgress = progress.ToDictionary(p => p.AchievementId);

            // Calculate stats
            CompletedCount = progress.Count(p => p.IsCompleted);
            TotalXP = progress.Where(p => p.IsCompleted).Sum(p => p.Achievement.XP);
            CompletionPercent = AllAchievements.Count > 0 
                ? (int)((CompletedCount / (double)AllAchievements.Count) * 100) 
                : 0;

            // Group by category
            AchievementsByCategoryList = AllAchievements.GroupBy(a => a.Category);
        }
    }
}
