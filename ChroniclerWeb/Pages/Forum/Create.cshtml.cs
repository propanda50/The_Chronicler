using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;
using ChroniclerWeb.Services.FileUpload;
using ChroniclerWeb.Services.AudioTranscription;

namespace ChroniclerWeb.Pages.Forum
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
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

        [BindProperty]
        public int CampaignId { get; set; }

        public string CampaignName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return RedirectToPage("/Account/Login", new { returnUrl = $"/Forum/Create?campaignId={campaignId}" });

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToPage("/Account/Login");

            if (!await _campaignService.CanUserAccess(campaignId, userId))
            {
                TempData["Error"] = "You must be a member of this campaign to create forum posts.";
                return RedirectToPage("/Account/AccessDenied");
            }

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? string.Empty;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<IFormFile>? files)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return RedirectToPage("/Account/Login");

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToPage("/Account/Login");

            Post.CampaignId = CampaignId;
            Post.AuthorId = userId;

            ModelState.Remove("Post.Author");
            ModelState.Remove("Post.AuthorId");
            ModelState.Remove("Post.Campaign");
            ModelState.Remove("Post.CampaignId");

            if (!await _campaignService.CanUserAccess(Post.CampaignId, userId))
            {
                TempData["Error"] = "You must be a member of this campaign to create forum posts.";
                return RedirectToPage("/Account/AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["Error"] = "Validation failed: " + string.Join("; ", errors);
                var campaign = await _context.Campaigns.FindAsync(CampaignId);
                CampaignName = campaign?.Name ?? string.Empty;
                return Page();
            }

            Post.CreatedAt = DateTime.UtcNow;
            Post.UpdatedAt = DateTime.UtcNow;

            _context.ForumPosts.Add(Post);
            await _context.SaveChangesAsync();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files.Where(f => f.Length > 0))
                {
                    var fileType = DetermineFileType(file.ContentType);
                    await _fileUploadService.UploadFileAsync(
                        file: file,
                        userId: userId,
                        campaignId: Post.CampaignId,
                        characterId: null,
                        locationId: null,
                        eventId: null,
                        sessionId: null,
                        forumPostId: Post.Id,
                        fileType: fileType,
                        description: $"Forum attachment for post #{Post.Id}: {Post.Title}");
                }
            }

            TempData["Success"] = "Forum post created!";
            return RedirectToPage("/Forum/Index", new { campaignId = Post.CampaignId });
        }

        public async Task<IActionResult> OnPostTranscribeAudioAsync([FromForm] IFormFile audioFile, [FromForm] string? language)
        {
            if (audioFile == null || audioFile.Length == 0 || !audioFile.ContentType.StartsWith("audio/"))
                return new JsonResult(new { error = "No valid audio file provided." }) { StatusCode = 400 };

            try
            {
                await using var stream = audioFile.OpenReadStream();
                var transcript = await _audioTranscriptionService.TranscribeAudioAsync(
                    stream,
                    language ?? "auto",
                    audioFile.FileName,
                    audioFile.ContentType);

                return new JsonResult(new { transcript });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message }) { StatusCode = 500 };
            }
        }

        private static FileType DetermineFileType(string contentType)
        {
            if (contentType.StartsWith("image/")) return FileType.Image;
            if (contentType.StartsWith("audio/")) return FileType.Audio;
            if (contentType.Contains("pdf")) return FileType.Document;
            return FileType.Other;
        }
    }
}
