using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        [BindProperty]
        public Session Session { get; set; } = new();

        [BindProperty]
        public List<int> SelectedCharacterIds { get; set; } = new();

        [BindProperty]
        public List<int> SelectedLocationIds { get; set; } = new();

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public int NextSessionNumber { get; set; }
        public List<Character> AvailableCharacters { get; set; } = new();
        public List<Location> AvailableLocations { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            await LoadFormData(campaignId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(Session.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Session.Campaign");

            if (!ModelState.IsValid)
            {
                await LoadFormData(Session.CampaignId);
                return Page();
            }

            Session.CreatedAt = DateTime.UtcNow;
            Session.UpdatedAt = DateTime.UtcNow;

            _context.Sessions.Add(Session);
            await _context.SaveChangesAsync();

            // Add character tags
            foreach (var charId in SelectedCharacterIds)
            {
                _context.SessionCharacters.Add(new SessionCharacter
                {
                    SessionId = Session.Id,
                    CharacterId = charId
                });
            }

            // Add location tags
            foreach (var locId in SelectedLocationIds)
            {
                _context.SessionLocations.Add(new SessionLocation
                {
                    SessionId = Session.Id,
                    LocationId = locId
                });
            }

            // Update campaign timestamp
            var campaign = await _context.Campaigns.FindAsync(Session.CampaignId);
            if (campaign != null) campaign.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["Success"] = $"Session '{Session.Title}' created!";
            return RedirectToPage("/Sessions/Details", new { id = Session.Id });
        }

        private async Task LoadFormData(int campaignId)
        {
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignId = campaignId;
            CampaignName = campaign?.Name ?? "";

            NextSessionNumber = await _context.Sessions
                .Where(s => s.CampaignId == campaignId)
                .MaxAsync(s => (int?)s.SessionNumber) ?? 0;
            NextSessionNumber++;

            AvailableCharacters = await _context.Characters
                .Where(c => c.CampaignId == campaignId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            AvailableLocations = await _context.Locations
                .Where(l => l.CampaignId == campaignId)
                .OrderBy(l => l.Name)
                .ToListAsync();
        }
    }
}
