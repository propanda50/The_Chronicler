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

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        [BindProperty]
        public bool RemoveImage { get; set; }

        public string CampaignName { get; set; } = string.Empty;
        public List<Character> AvailableCharacters { get; set; } = new();
        public List<Location> AvailableLocations { get; set; } = new();
        public bool IsGameMaster { get; set; }
        public bool IsNotesOnly { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var session = await _context.Sessions
                .Include(s => s.SessionCharacters)
                .Include(s => s.SessionLocations)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null) return NotFound();

            var isGM = await _campaignService.CanUserEdit(session.CampaignId, userId);
            var canAddNotes = await _campaignService.CanUserAddNotes(session.CampaignId, userId);

            if (!isGM && !canAddNotes)
                return RedirectToPage("/Account/AccessDenied");

            IsGameMaster = isGM;
            IsNotesOnly = !isGM && canAddNotes;

            Session = session;
            SelectedCharacterIds = session.SessionCharacters.Select(sc => sc.CharacterId).ToList();
            SelectedLocationIds = session.SessionLocations.Select(sl => sl.LocationId).ToList();

            await LoadFormData(session.CampaignId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;

            var isGM = await _campaignService.CanUserEdit(Session.CampaignId, userId);
            var canAddNotes = await _campaignService.CanUserAddNotes(Session.CampaignId, userId);

            if (!isGM && !canAddNotes)
                return RedirectToPage("/Account/AccessDenied");

            var existing = await _context.Sessions
                .Include(s => s.SessionCharacters)
                .Include(s => s.SessionLocations)
                .FirstOrDefaultAsync(s => s.Id == Session.Id);

            if (existing == null) return NotFound();

            if (isGM)
            {
                // FULL UPDATE - GameMaster only
                existing.Title = Session.Title;
                existing.SessionNumber = Session.SessionNumber;
                existing.SessionDate = Session.SessionDate;
                existing.Summary = Session.Summary;
                existing.Notes = Session.Notes;
                existing.UpdatedAt = DateTime.UtcNow;

                // Handle image removal
                if (RemoveImage)
                {
                    existing.ImageData = null;
                    existing.ImageContentType = null;
                    existing.ImageUrl = null;
                }

                // Handle new image upload
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await ImageFile.CopyToAsync(ms);
                    existing.ImageData = Convert.ToBase64String(ms.ToArray());
                    existing.ImageContentType = ImageFile.ContentType;
                }

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
            }
            else
            {
                // NOTES ONLY - CanAddNotes players
                existing.Notes = Session.Notes;
                existing.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = isGM ? "Session updated!" : "Notes updated!";
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
