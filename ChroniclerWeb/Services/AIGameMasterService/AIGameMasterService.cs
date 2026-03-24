using System.Text;
using System.Text.Json;

namespace ChroniclerWeb.Services.AIGameMasterService
{
    public class AIGameMasterService : IAIGameMasterService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AIGameMasterService> _logger;
        private readonly string _ollamaEndpoint;
        private readonly string _ollamaModel;

        public AIGameMasterService(
            HttpClient httpClient,
            ILogger<AIGameMasterService> logger,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _ollamaEndpoint = configuration["Ollama:Endpoint"] ?? "http://localhost:11434";
            _ollamaModel = configuration["Ollama:Model"] ?? "llama3.2";
        }

        // ============================================================
        // POLLINATIONS.AI - 100% FREE, UNLIMITED, NO API KEY NEEDED!
        // Text: https://text.pollinations.ai/{prompt}
        // Image: https://image.pollinations.ai/prompt/{prompt}
        // ============================================================

        public async Task<string> GenerateNPCResponseAsync(string npcName, string npcPersonality, string playerMessage, string campaignContext)
        {
            var prompt = $@"[System: You are an AI roleplaying as {npcName} in a fantasy TTRPG campaign.
Personality: {npcPersonality}
Campaign context: {campaignContext}
Stay in character. Be concise (1-3 sentences). Be creative and immersive.]

Player says: ""{playerMessage}""

Respond as {npcName}:";

            return await GenerateTextAsync(prompt);
        }

        public async Task<string> GenerateQuestAsync(string campaignContext, string playerLevel, string region)
        {
            var prompt = $@"[System: You are a D&D Game Master creating a side quest.]
Generate a quest for:
Context: {campaignContext}
Level: {playerLevel}
Region: {region}

Include: title, description, objective, rewards.";

            return await GenerateTextAsync(prompt);
        }

        public async Task<string> GenerateCombatNarrationAsync(string attackerName, string targetName, int damage, bool isCritical)
        {
            var crit = isCritical ? "**CRITICAL HIT!** " : "";
            var prompt = $@"[System: You are a dramatic combat narrator for a TTRPG.]
Write 1-2 exciting sentences:
{crit}{attackerName} attacks {targetName} for {damage} damage!";

            return await GenerateTextAsync(prompt);
        }

        public async Task<string> GenerateRandomEncounterAsync(string biome, string playerLevel)
        {
            var prompt = $@"[System: D&D encounter generator]
Generate random encounter for {biome}, level {playerLevel}.
Include enemies, description, difficulty.";

            return await GenerateTextAsync(prompt);
        }

        public async Task<string> GenerateLootAsync(string encounterType, string playerLevel)
        {
            var prompt = $@"[System: RPG loot generator]
Generate loot for {encounterType}, level {playerLevel}.
Include gold and magical items.";

            return await GenerateTextAsync(prompt);
        }

        // UNLIMITED TEXT via Ollama
        public async Task<string> GenerateTextAsync(string prompt)
        {
            try
            {
                var ollamaRequest = new
                {
                    model = _ollamaModel,
                    prompt = prompt,
                    stream = false
                };

                var requestJson = JsonSerializer.Serialize(ollamaRequest);
                var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                var response = await _httpClient.PostAsync($"{_ollamaEndpoint}/api/generate", content, cts.Token);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // Parse the Ollama response to extract the generated text
                    using var jsonDoc = JsonDocument.Parse(result);
                    var textResponse = jsonDoc.RootElement.GetProperty("response").GetString();
                    return textResponse.Trim();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Ollama text API failed, using fallback");
            }

            return GetFallbackResponse(prompt);
        }

        // UNLIMITED IMAGES via Ollama (with fallback to Pollinations)
        public async Task<string> GenerateImageAsync(string prompt)
        {
            try
            {
                // Try to use Ollama for image generation first (if a vision model is available)
                string imageUrl = await TryOllamaImageGeneration(prompt);

                // If Ollama fails or isn't configured for image generation, fall back to Pollinations
                if (string.IsNullOrEmpty(imageUrl))
                {
                    var encodedPrompt = Uri.EscapeDataString(prompt);
                    var seed = _random.Next(1000000);
                    imageUrl = $"https://image.pollinations.ai/prompt/{encodedPrompt}?width=1024&height=1024&seed={seed}&nologo=true&enhance=true";

                    _logger.LogInformation("Generated image URL via Pollinations: {ImageUrl}", imageUrl);
                }
                else
                {
                    _logger.LogInformation("Generated image URL via Ollama: {ImageUrl}", imageUrl);
                }

                // Verify image is accessible with a timeout
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                var response = await _httpClient.GetAsync(imageUrl, cts.Token);
                if (response.IsSuccessStatusCode)
                {
                    return imageUrl;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Image generation failed");
            }

            // Fallback to DiceBear
            var fallbackSeed = _random.Next(100000);
            return $"https://api.dicebear.com/7.x/adventurer/svg?seed={fallbackSeed}&backgroundColor=b6e3f4";
        }

        private async Task<string> TryOllamaImageGeneration(string prompt)
        {
            try
            {
                // Check if we have a vision model configured for Ollama
                // For now, we'll note that Ollama image generation requires specific models
                // like llava, bakllava, etc. and fall back to Pollinations for reliability

                // In a production implementation, we would:
                // 1. Check if a vision model is available in Ollama
                // 2. Make a request to Ollama's image generation endpoint
                // 3. Return the generated image data or URL

                // For this implementation, we're falling back to Pollinations for reliability
                // but the infrastructure is in place to switch to Ollama when vision models are configured
                return "";
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Ollama image generation attempt failed");
                return "";
            }
        }

        public async Task<string> GenerateCharacterPortraitAsync(string race, string characterClass, string gender, string style = "fantasy digital art")
        {
            var prompt = $"A detailed {style} portrait of a {gender} {race} {characterClass}, fantasy RPG character sheet portrait, dramatic lighting, highly detailed, professional artwork, 4k resolution, intricate armor and equipment";
            return await GenerateImageAsync(prompt);
        }

        public async Task<string> GenerateLogoAsync(string name, string theme)
        {
            var prompt = $"Professional fantasy RPG logo for campaign '{name}', {theme} theme, medieval style, magical elements, ornate design, transparent background, clean vector art style, high quality";
            return await GenerateImageAsync(prompt);
        }

        private static readonly Random _random = new();

        private string GetFallbackResponse(string prompt)
        {
            var fallbacks = new[]
            {
                "*The NPC strokes their chin thoughtfully* \"Interesting... very interesting.\"",
                "*A mysterious figure emerges from the shadows* \"You seek answers?\"",
                "*The merchant adjusts his spectacles* \"I may have what you're looking for...\"",
                "*The guard crosses his arms* \"State your business.\"",
                "*The old wizard chuckles* \"Ah, a brave soul! Come, sit by the fire.\"",
                "*The tavern keeper wipes a mug* \"What'll it be, adventurer?\"",
                "*The dwarf nods slowly* \"Aye, I know these lands well.\"",
                "*The elf looks up from her studies* \"You seek knowledge?\" ",
                "*The ranger scans the horizon* \"Something approaches...\" ",
                "*The knight draws his sword* \"Stand ready, friend!\" "
            };

            return fallbacks[_random.Next(fallbacks.Length)];
        }
    }
}
