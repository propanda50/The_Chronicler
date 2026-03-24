using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public Campaign Campaign { get; set; } = null!;
        public List<Session> Sessions { get; set; } = new();
        public bool IsGameMaster { get; set; }

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserMember(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Campaign = (await _context.Campaigns.FindAsync(campaignId))!;
            if (Campaign == null) return NotFound();

            IsGameMaster = await _campaignService.IsUserGameMaster(campaignId, userId);

            Sessions = await _context.Sessions
                .Include(s => s.SessionCharacters)
                .Include(s => s.SessionLocations)
                .Include(s => s.Events)
                .Where(s => s.CampaignId == campaignId)
                .OrderByDescending(s => s.SessionNumber)
                .ToListAsync();

            return Page();
        }
    }
}
