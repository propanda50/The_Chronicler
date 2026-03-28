using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Events
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
        public Event Event { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public List<Session> AvailableSessions { get; set; } = new();

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
            if (!await _campaignService.CanUserEdit(Event.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Event.Campaign");
            ModelState.Remove("ImageFile");

            if (!ModelState.IsValid)
            {
                await LoadFormData(Event.CampaignId);
                return Page();
            }

            Event.CreatedAt = DateTime.UtcNow;

            // Handle image upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await ImageFile.CopyToAsync(ms);
                Event.ImageData = Convert.ToBase64String(ms.ToArray());
                Event.ImageContentType = ImageFile.ContentType;
            }

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Event '{Event.Title}' created!";
            return RedirectToPage("/Events/Details", new { id = Event.Id });
        }

        private async Task LoadFormData(int campaignId)
        {
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignId = campaignId;
            CampaignName = campaign?.Name ?? "";

            AvailableSessions = await _context.Sessions
                .Where(s => s.CampaignId == campaignId)
                .OrderBy(s => s.SessionNumber)
                .ToListAsync();
        }
    }
}
