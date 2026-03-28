using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;
using ChroniclerWeb.Services.FileUpload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace ChroniclerWeb.Pages.Files
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileUploadService _fileUploadService;
        private readonly ICampaignService _campaignService;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IFileUploadService fileUploadService,
            ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _fileUploadService = fileUploadService;
            _campaignService = campaignService;
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public IEnumerable<UploadedFile> Files { get; set; } = Enumerable.Empty<UploadedFile>();

        public List<Character> AvailableCharacters { get; set; } = new();
        public List<Location> AvailableLocations { get; set; } = new();
        public List<Event> AvailableEvents { get; set; } = new();
        public List<Session> AvailableSessions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserMember(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";
            Files = await _fileUploadService.GetFilesByCampaignAsync(campaignId);

            await LoadLinkedEntities(campaignId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            IFormFile file,
            int campaignId,
            FileType fileType,
            string? description,
            int? characterId,
            int? locationId,
            int? eventId,
            int? sessionId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserMember(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            if (file != null && file.Length > 0)
            {
                await _fileUploadService.UploadFileAsync(
                    file: file,
                    userId: userId,
                    campaignId: campaignId,
                    characterId: characterId,
                    locationId: locationId,
                    eventId: eventId,
                    sessionId: sessionId,
                    forumPostId: null,
                    fileType: fileType,
                    description: description);

                TempData["Success"] = "File uploaded successfully!";
            }

            return RedirectToPage(new { campaignId });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int fileId, int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            await _fileUploadService.DeleteFileAsync(fileId);
            TempData["Success"] = "File deleted.";
            return RedirectToPage(new { campaignId });
        }

        private async Task LoadLinkedEntities(int campaignId)
        {
            AvailableCharacters = await _context.Characters
                .Where(c => c.CampaignId == campaignId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            AvailableLocations = await _context.Locations
                .Where(l => l.CampaignId == campaignId)
                .OrderBy(l => l.Name)
                .ToListAsync();

            AvailableEvents = await _context.Events
                .Where(e => e.CampaignId == campaignId)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();

            AvailableSessions = await _context.Sessions
                .Where(s => s.CampaignId == campaignId)
                .OrderBy(s => s.SessionNumber)
                .ToListAsync();
        }
    }
}
