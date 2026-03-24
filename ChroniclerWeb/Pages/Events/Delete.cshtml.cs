using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Events
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public DeleteModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        [BindProperty]
        public Event Event { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();

            if (!await _campaignService.CanUserEdit(evt.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Event = evt;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            var evt = await _context.Events.FindAsync(Event.Id);
            if (evt == null) return NotFound();

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Event '{evt.Title}' deleted.";
            return RedirectToPage("/Events/Index", new { campaignId });
        }
    }
}
