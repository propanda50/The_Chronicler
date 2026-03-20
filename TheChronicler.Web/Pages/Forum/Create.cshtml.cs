using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Forum
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
        public ForumPost Post { get; set; } = new();

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserMember(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserMember(Post.CampaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            if (!ModelState.IsValid)
            {
                CampaignId = Post.CampaignId;
                return Page();
            }

            Post.AuthorId = userId;
            Post.CreatedAt = DateTime.UtcNow;
            Post.UpdatedAt = DateTime.UtcNow;

            _context.ForumPosts.Add(Post);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Forum post created!";
            return RedirectToPage("/Forum/Index", new { campaignId = Post.CampaignId });
        }
    }
}
