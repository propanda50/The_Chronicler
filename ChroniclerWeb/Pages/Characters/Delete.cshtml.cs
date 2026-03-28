using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;

namespace ChroniclerWeb.Pages.Characters
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
        public Character Character { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return NotFound();

            if (!await _campaignService.CanUserEdit(character.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Character = character;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            var character = await _context.Characters.FindAsync(Character.Id);
            if (character == null) return NotFound();

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Character '{character.Name}' deleted.";
            return RedirectToPage("/Characters/Index", new { campaignId });
        }
    }
}
