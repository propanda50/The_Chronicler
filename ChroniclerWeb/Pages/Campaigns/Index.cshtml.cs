using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Campaigns
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
        public string CurrentUserId { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            CurrentUserId = _userManager.GetUserId(User)!;

            var campaignIds = await _context.Campaigns
                .Where(c => c.OwnerId == CurrentUserId)
                .Select(c => c.Id)
                .Union(
                    _context.CampaignMembers
                        .Where(cm => cm.UserId == CurrentUserId)
                        .Select(cm => cm.CampaignId)
                )
                .ToListAsync();

            Campaigns = await _context.Campaigns
                .Include(c => c.Sessions)
                .Include(c => c.Characters)
                .Where(c => campaignIds.Contains(c.Id))
                .OrderByDescending(c => c.UpdatedAt)
                .ToListAsync();
        }
    }
}
