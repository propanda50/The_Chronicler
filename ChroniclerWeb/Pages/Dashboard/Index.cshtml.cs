using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;

namespace ChroniclerWeb.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Campaign> Campaigns { get; set; } = new();
        public List<Session> RecentSessions { get; set; } = new();
        public int TotalCampaigns { get; set; }
        public int TotalSessions { get; set; }
        public int TotalCharacters { get; set; }
        public int TotalLocations { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User)!;

            // Get campaigns owned or member of
            var campaignIds = await _context.Campaigns
                .Where(c => c.OwnerId == userId)
                .Select(c => c.Id)
                .Union(
                    _context.CampaignMembers
                        .Where(cm => cm.UserId == userId)
                        .Select(cm => cm.CampaignId)
                )
                .ToListAsync();

            Campaigns = await _context.Campaigns
                .Include(c => c.Sessions)
                .Where(c => campaignIds.Contains(c.Id))
                .OrderByDescending(c => c.UpdatedAt)
                .ToListAsync();

            RecentSessions = await _context.Sessions
                .Include(s => s.Campaign)
                .Where(s => campaignIds.Contains(s.CampaignId))
                .OrderByDescending(s => s.SessionDate)
                .Take(5)
                .ToListAsync();

            TotalCampaigns = Campaigns.Count;
            TotalSessions = await _context.Sessions.CountAsync(s => campaignIds.Contains(s.CampaignId));
            TotalCharacters = await _context.Characters.CountAsync(c => campaignIds.Contains(c.CampaignId));
            TotalLocations = await _context.Locations.CountAsync(l => campaignIds.Contains(l.CampaignId));
        }
    }
}
