using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Events
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

        public Event Event { get; set; } = null!;
        public string CampaignName { get; set; } = string.Empty;
        public bool IsGameMaster { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var evt = await _context.Events
                .Include(e => e.Campaign)
                .Include(e => e.Session)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evt == null) return NotFound();

            if (!await _campaignService.IsUserMember(evt.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Event = evt;
            CampaignName = evt.Campaign.Name;
            IsGameMaster = await _campaignService.IsUserGameMaster(evt.CampaignId, userId);

            return Page();
        }
    }
}
