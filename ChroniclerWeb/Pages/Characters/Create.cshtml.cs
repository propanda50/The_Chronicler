using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Characters
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        [BindProperty]
        public Character Character { get; set; } = new();

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(Character.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Character.Campaign");

            if (string.IsNullOrWhiteSpace(Character.Name))
            {
                ModelState.AddModelError("Character.Name", "Name is required.");
            }

// Validate stat points - 54 total, each stat minimum 0, max 18
            const int maxPoints = 54;
            const int maxPerStat = 18;
            int totalPoints = Character.Strength + Character.Agility + Character.Health + Character.Intelligence;
            
            if (totalPoints > maxPoints)
            {
                ModelState.AddModelError("", $"Total stat points ({totalPoints}) exceeds maximum ({maxPoints}).");
            }
            
            // Validate stat values are in range
            if (Character.Strength < 0 || Character.Strength > maxPerStat)
                ModelState.AddModelError("Character.Strength", $"Strength must be between 0 and {maxPerStat}.");
            if (Character.Agility < 0 || Character.Agility > maxPerStat)
                ModelState.AddModelError("Character.Agility", $"Agility must be between 0 and {maxPerStat}.");
            if (Character.Health < 0 || Character.Health > maxPerStat)
                ModelState.AddModelError("Character.Health", $"Health must be between 0 and {maxPerStat}.");
            if (Character.Intelligence < 0 || Character.Intelligence > maxPerStat)
                ModelState.AddModelError("Character.Intelligence", $"Intelligence must be between 0 and {maxPerStat}.");

            if (!ModelState.IsValid)
            {
                CampaignId = Character.CampaignId;
                var campaign = await _context.Campaigns.FindAsync(Character.CampaignId);
                CampaignName = campaign?.Name ?? "";
                return Page();
            }

            // Generate AI portrait if none provided
if (string.IsNullOrEmpty(Character.PortraitData))
{
    // In a real implementation, this would call an AI image generation service
    // For now, we'll use a placeholder that indicates AI generation is required
    Character.PortraitData = ""; // Will be handled by the frontend
    Character.PortraitContentType = "image/svg+xml";
    // The actual portrait URL will be set by the JavaScript frontend
}

            Character.CreatedAt = DateTime.UtcNow;
            Character.UpdatedAt = DateTime.UtcNow;
            
            // Initialize portrait fields if not set
            if (string.IsNullOrEmpty(Character.PortraitData))
            {
                Character.PortraitData = string.Empty;
                Character.PortraitContentType = "image/svg+xml";
            }

            _context.Characters.Add(Character);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Character '{Character.Name}' created!";
            return RedirectToPage("/Characters/Details", new { id = Character.Id });
        }
    }
}
