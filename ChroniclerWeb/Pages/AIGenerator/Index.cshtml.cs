using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.AIGenerator
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAIGenerationService _aiGenerationService;
        private readonly IAIGameMasterService _aiGameMasterService;

        public IndexModel(IAIGenerationService aiGenerationService, IAIGameMasterService aiGameMasterService)
        {
            _aiGenerationService = aiGenerationService;
            _aiGameMasterService = aiGameMasterService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostGenerateName([FromBody] GenerateNameRequest request)
        {
            try
            {
                var name = _aiGenerationService.GenerateRandomCharacterName(request.Race, request.CharacterClass);
                return new JsonResult(new { name });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message }) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> OnPostGeneratePortrait([FromBody] GeneratePortraitRequest request)
        {
            try
            {
                var imageUrl = await _aiGameMasterService.GenerateCharacterPortraitAsync(
                    request.Race ?? "human",
                    request.CharacterClass ?? "fighter",
                    request.Gender ?? "male",
                    request.Style ?? "fantasy digital art"
                );
                return new JsonResult(new { imageUrl });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message }) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> OnPostGenerateLogo([FromBody] GenerateLogoRequest request)
        {
            try
            {
                var imageUrl = await _aiGameMasterService.GenerateLogoAsync(
                    request.Name ?? "My Campaign",
                    request.Theme ?? "medieval fantasy"
                );
                return new JsonResult(new { imageUrl });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message }) { StatusCode = 500 };
            }
        }
    }

    public class GenerateNameRequest
    {
        public string? Race { get; set; }
        public string? CharacterClass { get; set; }
    }

    public class GeneratePortraitRequest
    {
        public string? Race { get; set; }
        public string? CharacterClass { get; set; }
        public string? Gender { get; set; }
        public string? Style { get; set; }
        public string? Seed { get; set; }
    }

    public class GenerateLogoRequest
    {
        public string? Name { get; set; }
        public string? Theme { get; set; }
    }
}
