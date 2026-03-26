using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Characters
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

        public Character Character { get; set; } = null!;
        public string CampaignName { get; set; } = string.Empty;
        public bool IsGameMaster { get; set; }
        public List<Session> AppearsInSessions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var character = await _context.Characters
                .Include(c => c.Campaign)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (character == null) return NotFound();

            if (!await _campaignService.IsUserMember(character.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Character = character;
            CampaignName = character.Campaign.Name;
            IsGameMaster = await _campaignService.IsUserGameMaster(character.CampaignId, userId);

            AppearsInSessions = await _context.SessionCharacters
                .Where(sc => sc.CharacterId == id)
                .Include(sc => sc.Session)
                .Select(sc => sc.Session)
                .OrderByDescending(s => s.SessionDate)
                .ToListAsync();

            return Page();
        }
    }
}
