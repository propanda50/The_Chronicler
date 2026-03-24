using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Sessions
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

        public Session Session { get; set; } = null!;
        public bool IsGameMaster { get; set; }
        public int? PreviousSessionId { get; set; }
        public int? NextSessionId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var session = await _context.Sessions
                .Include(s => s.Campaign)
                .Include(s => s.SessionCharacters).ThenInclude(sc => sc.Character)
                .Include(s => s.SessionLocations).ThenInclude(sl => sl.Location)
                .Include(s => s.Events)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null) return NotFound();

            if (!await _campaignService.IsUserMember(session.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Session = session;
            IsGameMaster = await _campaignService.IsUserGameMaster(session.CampaignId, userId);

            // Navigation
            PreviousSessionId = await _context.Sessions
                .Where(s => s.CampaignId == session.CampaignId && s.SessionNumber < session.SessionNumber)
                .OrderByDescending(s => s.SessionNumber)
                .Select(s => (int?)s.Id)
                .FirstOrDefaultAsync();

            NextSessionId = await _context.Sessions
                .Where(s => s.CampaignId == session.CampaignId && s.SessionNumber > session.SessionNumber)
                .OrderBy(s => s.SessionNumber)
                .Select(s => (int?)s.Id)
                .FirstOrDefaultAsync();

            return Page();
        }
    }
}
