using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Events
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
        public Event Event { get; set; } = null!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        [BindProperty]
        public bool RemoveImage { get; set; }

        public List<Session> AvailableSessions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();

            if (!await _campaignService.CanUserEdit(evt.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Event = evt;
            await LoadSessions(evt.CampaignId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(Event.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Event.Campaign");
            ModelState.Remove("ImageFile");

            if (!ModelState.IsValid)
            {
                await LoadSessions(Event.CampaignId);
                return Page();
            }

            var existing = await _context.Events.FindAsync(Event.Id);
            if (existing == null) return NotFound();

            existing.Title = Event.Title;
            existing.Description = Event.Description;
            existing.EventDate = Event.EventDate;
            existing.IsKeyEvent = Event.IsKeyEvent;
            existing.SessionId = Event.SessionId;

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

            await _context.SaveChangesAsync();

            TempData["Success"] = "Event updated!";
            return RedirectToPage("/Events/Details", new { id = Event.Id });
        }

        private async Task LoadSessions(int campaignId)
        {
            AvailableSessions = await _context.Sessions
                .Where(s => s.CampaignId == campaignId)
                .OrderBy(s => s.SessionNumber)
                .ToListAsync();
        }
    }
}
