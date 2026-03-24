using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;


namespace ChroniclerWeb.Pages.Forum
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public string SelectedCategory { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public List<PostViewModel> Posts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int campaignId, string? category)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToPage("/Account/Login", new { returnUrl = $"/Forum/Index?campaignId={campaignId}" });
            }

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            if (!await _campaignService.IsUserMember(campaignId, userId))
            {
                TempData["Error"] = "You must be a member of this campaign to view the forum.";
                return RedirectToPage("/Campaigns/Index");
            }

            CampaignId = campaignId;
            SelectedCategory = category ?? "";

            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";

            var query = _context.ForumPosts
                .Include(p => p.Author)
                .Include(p => p.Replies)
                .Where(p => p.CampaignId == campaignId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SelectedCategory) && Enum.TryParse<ForumCategory>(SelectedCategory, out var cat))
            {
                query = query.Where(p => p.Category == cat);
                CategoryName = cat.ToString();
            }

            var posts = await query
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            Posts = posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Category = p.Category.ToString(),
                AuthorName = p.Author?.DisplayName ?? "Unknown",
                CreatedAt = p.CreatedAt,
                ReplyCount = p.Replies.Count
            }).ToList();

            return Page();
        }

        public class PostViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public string AuthorName { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public int ReplyCount { get; set; }
        }
    }
}
