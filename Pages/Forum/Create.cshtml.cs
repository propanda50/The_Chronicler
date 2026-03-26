using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Forum
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IAudioTranscriptionService _audioTranscriptionService;

        public CreateModel(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            ICampaignService campaignService,
            IFileUploadService fileUploadService,
            IAudioTranscriptionService audioTranscriptionService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
            _fileUploadService = fileUploadService;
            _audioTranscriptionService = audioTranscriptionService;
        }

        [BindProperty]
        public ForumPost Post { get; set; } = new();

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToPage("/Account/Login", new { returnUrl = $"/Forum/Create?campaignId={campaignId}" });
            }

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            if (!await _campaignService.IsUserMember(campaignId, userId))
            {
                TempData["Error"] = "You must be a member of this campaign to create forum posts.";
                return RedirectToPage("/Account/AccessDenied");
            }

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<IFormFile>? Files)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToPage("/Account/Login");
            }

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            if (!await _campaignService.IsUserMember(Post.CampaignId, userId))
            {
                TempData["Error"] = "You must be a member of this campaign to create forum posts.";
                return RedirectToPage("/Account/AccessDenied");
            }

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

            // Handle file uploads
            if (Files != null && Files.Count > 0)
            {
                foreach (var file in Files)
                {
                    if (file.Length > 0)
                    {
                        var fileType = DetermineFileType(file.ContentType);
                        await _fileUploadService.UploadFileAsync(file, userId, Post.CampaignId, null, null, fileType);
                    }
                }
            }

            TempData["Success"] = "Forum post created!";
            return RedirectToPage("/Forum/Index", new { campaignId = Post.CampaignId });
        }

        public async Task<IActionResult> OnPostTranscribeAudio()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null || !file.ContentType.StartsWith("audio/"))
            {
                return new JsonResult(new { error = "No audio file provided" }) { StatusCode = 400 };
            }

            try
            {
                using var stream = file.OpenReadStream();
                var transcript = await _audioTranscriptionService.TranscribeAudioAsync(stream);
                return new JsonResult(new { transcript });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message }) { StatusCode = 500 };
            }
        }

        private FileType DetermineFileType(string contentType)
        {
            if (contentType.StartsWith("image/")) return FileType.Image;
            if (contentType.StartsWith("audio/")) return FileType.Audio;
            if (contentType.Contains("pdf")) return FileType.Document;
            return FileType.Other;
        }
    }
}
