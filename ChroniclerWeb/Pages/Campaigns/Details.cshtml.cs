using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Campaigns
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public DetailsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public Campaign Campaign { get; set; } = null!;
        public bool IsGameMaster { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var campaign = await _context.Campaigns
                .Include(c => c.Owner)
                .Include(c => c.Members).ThenInclude(m => m.User)
                .Include(c => c.Sessions)
                .Include(c => c.Characters)
                .Include(c => c.Locations)
                .Include(c => c.Events)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (campaign == null)
                return NotFound();

            if (!await _campaignService.IsUserMember(id, userId))
            {
                TempData["Error"] = "You don't have access to this campaign.";
                return RedirectToPage("/Campaigns/Index");
            }

            Campaign = campaign;
            IsGameMaster = await _campaignService.IsUserGameMaster(id, userId);

            return Page();
        }
    }
}
