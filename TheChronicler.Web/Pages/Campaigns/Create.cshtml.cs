using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;

namespace TheChronicler.Web.Pages.Campaigns
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Campaign Campaign { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            Campaign.OwnerId = userId;
            Campaign.CreatedAt = DateTime.UtcNow;
            Campaign.UpdatedAt = DateTime.UtcNow;

            // Remove navigation property validation
            ModelState.Remove("Campaign.Owner");
            ModelState.Remove("Campaign.OwnerId");

            if (!ModelState.IsValid)
                return Page();

            _context.Campaigns.Add(Campaign);

            // Add owner as GM member
            var membership = new CampaignMember
            {
                Campaign = Campaign,
                UserId = userId,
                Role = CampaignRole.GameMaster,
                CanAddNotes = true
            };
            _context.CampaignMembers.Add(membership);

            await _context.SaveChangesAsync();

            TempData["Success"] = $"Campaign '{Campaign.Name}' created successfully!";
            return RedirectToPage("/Campaigns/Details", new { id = Campaign.Id });
        }
    }
}
