using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Campaigns
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
        public Campaign Campaign { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserOwner(id, userId))
                return RedirectToPage("/Account/AccessDenied");

            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null) return NotFound();

            Campaign = campaign;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserOwner(Campaign.Id, userId))
                return RedirectToPage("/Account/AccessDenied");

            var campaign = await _context.Campaigns.FindAsync(Campaign.Id);
            if (campaign == null) return NotFound();

            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Campaign '{campaign.Name}' has been deleted.";
            return RedirectToPage("/Campaigns/Index");
        }
    }
}
