using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Characters
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

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(Character.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            ModelState.Remove("Character.Campaign");

            if (!ModelState.IsValid) return Page();

            var existing = await _context.Characters.FindAsync(Character.Id);
            if (existing == null) return NotFound();

            existing.Name = Character.Name;
            existing.Description = Character.Description;
            existing.Role = Character.Role;
            existing.Race = Character.Race;
            existing.Class = Character.Class;
            existing.Status = Character.Status;
            existing.Type = Character.Type;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Character updated!";
            return RedirectToPage("/Characters/Details", new { id = Character.Id });
        }
    }
}
