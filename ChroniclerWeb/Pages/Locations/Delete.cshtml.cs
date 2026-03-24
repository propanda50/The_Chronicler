using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Locations
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public DeleteModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
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

        public async Task<IActionResult> OnPostAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            var location = await _context.Locations.FindAsync(Location.Id);
            if (location == null) return NotFound();

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Location '{location.Name}' deleted.";
            return RedirectToPage("/Locations/Index", new { campaignId });
        }
    }
}
