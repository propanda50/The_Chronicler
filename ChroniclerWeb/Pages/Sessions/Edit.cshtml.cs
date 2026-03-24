using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Sessions
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public EditModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        [BindProperty]
        public Session Session { get; set; } = null!;

        [BindProperty]
        public List<int> SelectedCharacterIds { get; set; } = new();

        [BindProperty]
        public List<int> SelectedLocationIds { get; set; } = new();

        public string CampaignName { get; set; } = string.Empty;
        public List<Character> AvailableCharacters { get; set; } = new();
        public List<Location> AvailableLocations { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var session = await _context.Sessions
                .Include(s => s.SessionCharacters)
                .Include(s => s.SessionLocations)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null) return NotFound();

            if (!await _campaignService.CanUserEdit(session.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Session = session;
            SelectedCharacterIds = session.SessionCharacters.Select(sc => sc.CharacterId).ToList();
            SelectedLocationIds = session.SessionLocations.Select(sl => sl.LocationId).ToList();

            await LoadFormData(session.CampaignId);
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

            var existing = await _context.Sessions
                .Include(s => s.SessionCharacters)
                .Include(s => s.SessionLocations)
                .FirstOrDefaultAsync(s => s.Id == Session.Id);

            if (existing == null) return NotFound();

            existing.Title = Session.Title;
            existing.SessionNumber = Session.SessionNumber;
            existing.SessionDate = Session.SessionDate;
            existing.Summary = Session.Summary;
            existing.Notes = Session.Notes;
            existing.UpdatedAt = DateTime.UtcNow;

            // Update character tags
            _context.SessionCharacters.RemoveRange(existing.SessionCharacters);
            foreach (var charId in SelectedCharacterIds)
            {
                _context.SessionCharacters.Add(new SessionCharacter { SessionId = existing.Id, CharacterId = charId });
            }

            // Update location tags
            _context.SessionLocations.RemoveRange(existing.SessionLocations);
            foreach (var locId in SelectedLocationIds)
            {
                _context.SessionLocations.Add(new SessionLocation { SessionId = existing.Id, LocationId = locId });
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Session updated!";
            return RedirectToPage("/Sessions/Details", new { id = Session.Id });
        }

        private async Task LoadFormData(int campaignId)
        {
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";

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
