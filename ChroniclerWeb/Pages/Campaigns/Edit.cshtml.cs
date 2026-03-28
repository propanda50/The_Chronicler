using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChroniclerWeb.Pages.Campaigns
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
        public Campaign Campaign { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            if (!await _campaignService.CanUserEdit(id, userId))
                return RedirectToPage("/Account/AccessDenied");

            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null) return NotFound();

            Campaign = campaign;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;

            if (!await _campaignService.CanUserEdit(Campaign.Id, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Campaign.Owner");

            if (!ModelState.IsValid) return Page();

            var existing = await _context.Campaigns.FindAsync(Campaign.Id);
            if (existing == null) return NotFound();

            existing.Name = Campaign.Name;
            existing.Description = Campaign.Description;
            existing.GameSystem = Campaign.GameSystem;
            existing.Status = Campaign.Status;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Campaign updated successfully!";
            return RedirectToPage("/Campaigns/Details", new { id = Campaign.Id });
        }
    }
}
