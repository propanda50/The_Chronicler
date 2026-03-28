using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;
using System.Text.Json;
using ChroniclerWeb.Services.PredefinedLists;



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
        public string PredefinedClassesJson { get; set; } = "[]";
        public string PredefinedRacesJson { get; set; } = "[]";
        public string PredefinedRolesJson { get; set; } = "[]";

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserMember(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";

            // Pass predefined lists as JSON
            PredefinedClassesJson = JsonSerializer.Serialize(PredefinedLists.Classes);
            PredefinedRacesJson = JsonSerializer.Serialize(PredefinedLists.Races);
            PredefinedRolesJson = JsonSerializer.Serialize(PredefinedLists.Roles);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserMember(Character.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Character.Campaign");

            if (string.IsNullOrWhiteSpace(Character.Name))
            {
                ModelState.AddModelError("Character.Name", "Name is required.");
            }

            // Validate stat points - 60 total, each stat minimum 0, max 20
            const int maxPoints = 60;
            const int maxPerStat = 20;
            int totalPoints = Character.Strength + Character.Agility + Character.Health + Character.Intelligence + Character.Charisma + Character.Endurance;

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
            if (Character.Charisma < 0 || Character.Charisma > maxPerStat)
                ModelState.AddModelError("Character.Charisma", $"Charisma must be between 0 and {maxPerStat}.");
            if (Character.Endurance < 0 || Character.Endurance > maxPerStat)
                ModelState.AddModelError("Character.Endurance", $"Endurance must be between 0 and {maxPerStat}.");

            if (!ModelState.IsValid)
            {
                CampaignId = Character.CampaignId;
                var campaign = await _context.Campaigns.FindAsync(Character.CampaignId);
                CampaignName = campaign?.Name ?? "";
                return Page();
            }

            // Generate AI portrait if none provided
            if (string.IsNullOrEmpty(Character.PortraitData) && string.IsNullOrEmpty(Character.PortraitUrl))
            {
                // Use a placeholder - frontend handles AI generation
                Character.PortraitData = "";
                Character.PortraitContentType = "image/svg+xml";
            }

            // If PortraitUrl or PortraitData is set by JavaScript (from AI Horde), save it
            if (!string.IsNullOrEmpty(Character.PortraitUrl))
            {
                if (Character.PortraitUrl.StartsWith("data:image"))
                {
                    // Base64 data from AI Horde - save to PortraitData
                    var parts = Character.PortraitUrl.Split(',');
                    if (parts.Length == 2)
                    {
                        Character.PortraitData = parts[1];
                        Character.PortraitContentType = parts[0].Replace("data:", "").Replace(";base64", "");
                        Character.PortraitUrl = null; // Clear URL, use data
                    }
                }
                // If it's a regular URL, keep it as PortraitUrl
            }

            Character.CreatedAt = DateTime.UtcNow;
            Character.UpdatedAt = DateTime.UtcNow;

            // Initialize portrait fields if not set
            if (string.IsNullOrEmpty(Character.PortraitData) && string.IsNullOrEmpty(Character.PortraitUrl))
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
