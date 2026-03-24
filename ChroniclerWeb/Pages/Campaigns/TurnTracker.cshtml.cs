using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Campaigns
{
    [Authorize]
    public class TurnTrackerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public TurnTrackerModel(
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
        public List<CombatantViewModel> Combatants { get; set; } = new();
        public List<Character> AvailableCharacters { get; set; } = new();

        public class CombatantViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Type { get; set; } = "";
            public string Class { get; set; } = "";
            public int Initiative { get; set; }
            public int CurrentHp { get; set; }
            public int MaxHp { get; set; }
            public int Order { get; set; }
            public bool IsActive { get; set; }
            public string? PortraitUrl { get; set; }
            public int HpPercent => MaxHp > 0 ? (CurrentHp * 100 / MaxHp) : 0;
            public string HpClass => HpPercent > 50 ? "hp-full" : HpPercent > 25 ? "hp-mid" : "hp-low";
        }

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";

            // Load combatants from session or database
            AvailableCharacters = await _context.Characters
                .Where(c => c.CampaignId == campaignId)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddCombatantAsync(int campaignId, string name, int initiative, 
            string type, int currentHp, int maxHp, int? characterId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return Forbid();

            // Add combatant logic here
            TempData["Success"] = $"{name} added to combat!";
            return RedirectToPage(new { campaignId });
        }
    }
}
