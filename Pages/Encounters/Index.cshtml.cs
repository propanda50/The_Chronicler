using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Encounters
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = "";
        public List<MonsterDto> Monsters { get; set; } = new();

        public class MonsterDto
        {
            public string Name { get; set; } = "";
            public double ChallengeRating { get; set; }
            public int ArmorClass { get; set; }
            public int HitPoints { get; set; }
            public int AverageDamage { get; set; }
            public string Type { get; set; } = "";
            public string Size { get; set; } = "";
        }

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";

            // Load SRD monsters (sample data - in production, use a proper database)
            Monsters = GetSampleMonsters();

            return Page();
        }

        private List<MonsterDto> GetSampleMonsters()
        {
            return new List<MonsterDto>
            {
                new() { Name = "Goblin", ChallengeRating = 0.25, ArmorClass = 15, HitPoints = 7, AverageDamage = 5, Type = "Humanoid", Size = "Small" },
                new() { Name = "Orc", ChallengeRating = 0.5, ArmorClass = 13, HitPoints = 15, AverageDamage = 9, Type = "Humanoid", Size = "Medium" },
                new() { Name = "Wolf", ChallengeRating = 0.25, ArmorClass = 13, HitPoints = 11, AverageDamage = 7, Type = "Beast", Size = "Medium" },
                new() { Name = "Skeleton", ChallengeRating = 0.25, ArmorClass = 13, HitPoints = 13, AverageDamage = 5, Type = "Undead", Size = "Medium" },
                new() { Name = "Zombie", ChallengeRating = 0.25, ArmorClass = 8, HitPoints = 22, AverageDamage = 7, Type = "Undead", Size = "Medium" },
                new() { Name = "Bandit", ChallengeRating = 0.25, ArmorClass = 12, HitPoints = 11, AverageDamage = 5, Type = "Humanoid", Size = "Medium" },
                new() { Name = "Cultist", ChallengeRating = 0.25, ArmorClass = 12, HitPoints = 9, AverageDamage = 7, Type = "Humanoid", Size = "Medium" },
                new() { Name = "Animated Armor", ChallengeRating = 1, ArmorClass = 18, HitPoints = 40, AverageDamage = 10, Type = "Construct", Size = "Medium" },
                new() { Name = "Barbed Devil", ChallengeRating = 5, ArmorClass = 15, HitPoints = 102, AverageDamage = 17, Type = "Fiend", Size = "Medium" },
                new() { Name = "Ogre", ChallengeRating = 2, ArmorClass = 11, HitPoints = 59, AverageDamage = 13, Type = "Giant", Size = "Large" },
                new() { Name = "Owlbear", ChallengeRating = 3, ArmorClass = 13, HitPoints = 59, AverageDamage = 17, Type = "Monstrosity", Size = "Large" },
                new() { Name = "Troll", ChallengeRating = 5, ArmorClass = 15, HitPoints = 84, AverageDamage = 21, Type = "Giant", Size = "Large" },
                new() { Name = "Wyvern", ChallengeRating = 6, ArmorClass = 13, HitPoints = 110, AverageDamage = 22, Type = "Dragon", Size = "Large" },
                new() { Name = "Young Dragon", ChallengeRating = 7, ArmorClass = 17, HitPoints = 142, AverageDamage = 30, Type = "Dragon", Size = "Large" },
                new() { Name = "Giant Spider", ChallengeRating = 1, ArmorClass = 14, HitPoints = 26, AverageDamage = 9, Type = "Beast", Size = "Large" },
                new() { Name = "Bugbear", ChallengeRating = 1, ArmorClass = 14, HitPoints = 27, AverageDamage = 11, Type = "Humanoid", Size = "Medium" },
                new() { Name = "Hobgoblin", ChallengeRating = 1, ArmorClass = 18, HitPoints = 11, AverageDamage = 7, Type = "Humanoid", Size = "Medium" },
                new() { Name = "Gnoll", ChallengeRating = 1, ArmorClass = 15, HitPoints = 22, AverageDamage = 9, Type = "Humanoid", Size = "Medium" },
                new() { Name = "Kobold", ChallengeRating = 0.125, ArmorClass = 12, HitPoints = 5, AverageDamage = 3, Type = "Humanoid", Size = "Small" },
                new() { Name = "Gnome", ChallengeRating = 0.25, ArmorClass = 15, HitPoints = 7, AverageDamage = 4, Type = "Humanoid", Size = "Small" }
            };
        }
    }
}
