using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Locations
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

        public Location Location { get; set; } = null!;
        public string CampaignName { get; set; } = string.Empty;
        public bool IsGameMaster { get; set; }
        public List<Session> VisitedInSessions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var location = await _context.Locations
                .Include(l => l.Campaign)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null) return NotFound();

            if (!await _campaignService.IsUserMember(location.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Location = location;
            CampaignName = location.Campaign.Name;
            IsGameMaster = await _campaignService.IsUserGameMaster(location.CampaignId, userId);

            VisitedInSessions = await _context.SessionLocations
                .Where(sl => sl.LocationId == id)
                .Include(sl => sl.Session)
                .Select(sl => sl.Session)
                .OrderByDescending(s => s.SessionDate)
                .ToListAsync();

            return Page();
        }
    }
}
