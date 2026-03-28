using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Locations
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
        public Location Location { get; set; } = new();

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
            if (!await _campaignService.CanUserEdit(Location.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Location.Campaign");

            if (!ModelState.IsValid)
            {
                CampaignId = Location.CampaignId;
                var campaign = await _context.Campaigns.FindAsync(Location.CampaignId);
                CampaignName = campaign?.Name ?? "";
                return Page();
            }

            Location.CreatedAt = DateTime.UtcNow;
            Location.UpdatedAt = DateTime.UtcNow;

            _context.Locations.Add(Location);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Location '{Location.Name}' created!";
            return RedirectToPage("/Locations/Details", new { id = Location.Id });
        }
    }
}
