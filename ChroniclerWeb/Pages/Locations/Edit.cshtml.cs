using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Locations
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public EditModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        [BindProperty]
        public Location Location { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var location = await _context.Locations.FindAsync(id);
            if (location == null) return NotFound();

            if (!await _campaignService.CanUserEdit(location.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Location = location;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(Location.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Location.Campaign");

            if (!ModelState.IsValid) return Page();

            var existing = await _context.Locations.FindAsync(Location.Id);
            if (existing == null) return NotFound();

            existing.Name = Location.Name;
            existing.Description = Location.Description;
            existing.Region = Location.Region;
            existing.Type = Location.Type;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Location updated!";
            return RedirectToPage("/Locations/Details", new { id = Location.Id });
        }
    }
}
