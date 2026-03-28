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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public DetailsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public ForumPost Post { get; set; } = null!;
        public List<ForumReplyViewModel> Replies { get; set; } = new();

        [BindProperty]
        public string ReplyContent { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return RedirectToPage("/Account/Login");

            var post = await _context.ForumPosts
                .Include(p => p.Author)
                .Include(p => p.Replies).ThenInclude(r => r.Author)
                .Include(p => p.Campaign)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return NotFound();

            if (!await _campaignService.IsUserMember(post.CampaignId, userId))
            {
                TempData["Error"] = "You must be a member of this campaign to view forum posts.";
                return RedirectToPage("/Account/AccessDenied");
            }

            Post = post;

            Replies = post.Replies
                .OrderBy(r => r.CreatedAt)
                .Select(r => new ForumReplyViewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    AuthorName = r.Author?.DisplayName ?? "Unknown",
                    CreatedAt = r.CreatedAt
                }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostReplyAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return RedirectToPage("/Account/Login");

            var post = await _context.ForumPosts.FindAsync(id);
            if (post == null)
                return NotFound();

            if (!await _campaignService.IsUserMember(post.CampaignId, userId))
                return Forbid();

            if (string.IsNullOrWhiteSpace(ReplyContent))
            {
                TempData["Error"] = "Reply content cannot be empty.";
                return RedirectToPage(new { id });
            }

            var reply = new ForumReply
            {
                Content = ReplyContent.Trim(),
                PostId = id,
                AuthorId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.ForumReplies.Add(reply);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Reply posted!";
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostDeleteReplyAsync(int id, int replyId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return RedirectToPage("/Account/Login");

            var reply = await _context.ForumReplies.FindAsync(replyId);
            if (reply == null || reply.PostId != id)
                return NotFound();

            var post = await _context.ForumPosts.FindAsync(id);
            if (post == null)
                return NotFound();

            bool isAuthor = reply.AuthorId == userId;
            bool isGM = await _campaignService.IsUserGameMaster(post.CampaignId, userId);

            if (!isAuthor && !isGM)
                return Forbid();

            _context.ForumReplies.Remove(reply);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Reply deleted.";
            return RedirectToPage(new { id });
        }

        public class ForumReplyViewModel
        {
            public int Id { get; set; }
            public string Content { get; set; } = string.Empty;
            public string AuthorName { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
        }
    }
}
