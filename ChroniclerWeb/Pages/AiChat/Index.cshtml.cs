using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;
using ChroniclerWeb.Services.AIConversation;


namespace ChroniclerWeb.Pages.AiChat
{
    public class IndexModel : PageModel
    {
        private readonly IAIConversationService _aiService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IAIConversationService aiService,
            ILogger<IndexModel> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostSendMessageAsync([FromBody] ChatRequest request)
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                
                // Get campaign ID from query string if available
                int? campaignId = null;
                if (Request.Query.ContainsKey("campaignId") && int.TryParse(Request.Query["campaignId"], out int cId))
                {
                    campaignId = cId;
                }

                var response = await _aiService.SendMessageAsync(request.Message, userId, campaignId);

                return new JsonResult(new
                {
                    response = response.Response,
                    action = response.Action,
                    data = response.Data,
                    imageUrl = response.ImageUrl
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing AI chat message");
                return new JsonResult(new
                {
                    response = "Désolé, une erreur s'est produite. Réessaye dans quelques instants."
                });
            }
        }

        public async Task<IActionResult> OnPostGenerateImageAsync([FromBody] GenerateImageRequest request)
        {
            try
            {
                _logger.LogInformation("GenerateImage called with prompt: {Prompt}, type: {Type}", request.Prompt, request.Type);
                
                var context = request.Type switch
                {
                    "campaign_cover" => "fantasy RPG campaign cover art, epic medieval scene",
                    "character_portrait" => "fantasy RPG character portrait, detailed digital art",
                    "location" => "fantasy RPG location illustration, atmospheric environment",
                    "event" => "fantasy RPG event illustration, dramatic scene",
                    _ => "fantasy RPG illustration"
                };

                var imageUrl = await _aiService.GenerateImageAsync(request.Prompt, context);
                
                _logger.LogInformation("Generated image URL: {ImageUrl}", imageUrl);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return new JsonResult(new { imageUrl = "", success = false, error = "Failed to generate image URL" });
                }

                return new JsonResult(new { imageUrl = imageUrl, success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating image: {Message}", ex.Message);
                return new JsonResult(new { imageUrl = "", success = false, error = ex.Message });
            }
        }

        public class ChatRequest
        {
            public string Message { get; set; } = "";
        }

        public class GenerateImageRequest
        {
            public string Prompt { get; set; } = "";
            public string Type { get; set; } = "";
        }
    }
}