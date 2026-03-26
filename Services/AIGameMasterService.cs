using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace TheChronicler.Web.Services
{
    public interface IAIGameMasterService
    {
        Task<string> GenerateNPCResponseAsync(string npcName, string npcPersonality, string playerMessage, string campaignContext);
        Task<string> GenerateQuestAsync(string campaignContext, string playerLevel, string region);
        Task<string> GenerateCombatNarrationAsync(string attackerName, string targetName, int damage, bool isCritical);
        Task<string> GenerateRandomEncounterAsync(string biome, string playerLevel);
        Task<string> GenerateLootAsync(string encounterType, string playerLevel);
        Task<string> GenerateTextAsync(string prompt);
        Task<string> GenerateImageAsync(string prompt);
        Task<string> GenerateCharacterPortraitAsync(string race, string characterClass, string gender, string style);
        Task<string> GenerateLogoAsync(string name, string theme);
        Task<string> GenerateRandomNameAsync(string entityType, string theme, string? context = null);
        Task<string> GenerateDescriptionAsync(string entityType, string entityName, string? details = null);
    }

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
            // Build detailed prompt for full body character portrait
            string raceDesc = GetDetailedRaceDescription(race.ToLower());
            string classGear = GetDetailedClassGear(characterClass.ToLower());
            string genderDesc = gender.ToLower() == "female" ? "female" : (gender.ToLower() == "male" ? "male" : "");
            
            var prompt = $"FULL BODY portrait of {genderDesc} {raceDesc} {classGear}, fantasy RPG character, " +
                        $"standing full length view, detailed face, hands visible, " +
                        $"professional digital painting, epic fantasy art, " +
                        $"dramatic lighting, intricate details, masterpiece, " +
                        $"8k resolution, high quality";
            
            return await GenerateImageAsync(prompt);
        }

        private string GetDetailedRaceDescription(string race)
        {
            return race switch
            {
                "elf" or "elven" => "elegant elf with pointed ears, long flowing silver hair, pale skin, graceful slender build",
                "dwarf" or "dwarven" => "stout dwarf with magnificent braided beard, rugged weathered face, muscular stocky build, wearing traditional dwarven garb",
                "orc" or "half-orc" => "fierce orc with prominent tusks, green-gray skin, intimidation muscles, war paint markings",
                "halfling" or "hobbit" => "cheerful halfling with curly brown hair, round friendly face, small but sturdy build, comfortable clothing",
                "tiefling" => "tiefling with curved horns, long tail, crimson skin, elegant demonic features, mysterious aura",
                "dragonborn" => "dragonborn with gleaming scales, draconic facial features, imposing armored build, wings folded on back",
                "gnome" => "eccentric gnome with wild hair, large expressive eyes, small but energetic build, tinker's apron",
                "half-elf" => "handsome half-elf with striking violet eyes, versatile features, athletic build, travel-worn clothes",
                _ => "human adventurer with determined expression, well-traveled appearance, versatile gear"
            };
        }

        private string GetDetailedClassGear(string characterClass)
        {
            return characterClass switch
            {
                "fighter" or "warrior" or "knight" => "wearing polished plate armor, sword at hip, shield decorated with heraldry, battle-ready stance",
                "wizard" or "mage" or "sorcerer" => "clad in flowing magical robes embroidered with arcane symbols, carrying ornate wooden staff crowned with crystal, mystical energy aura",
                "rogue" or "assassin" or "thief" => "wearing dark leather armor with multiple pockets, twin daggers at ready, hood thrown back, sneaky confident pose",
                "cleric" or "priest" => "in blessed holy vestments with sacred symbol prominently displayed, healing staff in hand, divine light emanating",
                "paladin" or "holy" => "in radiant plate armor glowing with holy energy, holy sword raised, noble determined expression, angelic wings hint",
                "ranger" or "hunter" => "in forest-green cloak and practical leather, longbow across back, quiver full of arrows, wilderness survival gear",
                "barbarian" or "berserker" => "wearing minimal animal-skin armor showing muscular form, dual-wielding massive axes, fierce wild battle cry expression",
                "bard" or "performer" => "in flamboyant colorful coat with feathered cap, lute strapped to back, charming charismatic smile, performer accessories",
                "druid" or "nature" => "in earth-toned nature robes woven with leaves, wooden staff with living vines, forest creature companions, natural magic glow",
                "monk" or "martial" => "in simple loose martial arts gi, fists raised in combat stance, serene zen expression, aura of inner peace and power",
                "warlock" or "witch" => "in dark mysterious robes with eldritch sigils, pact blade or otherworldly familiar nearby, ominous magical corruption aura",
                "necromancer" => "in dark robes of bone and shadow, skeletal minions nearby, necrotic green energy, ominous ominous presence",
                _ => "in practical adventuring clothes with versatile equipment, well-prepared for any challenge, confident adventurous stance"
            };
        }

        public async Task<string> GenerateLogoAsync(string name, string theme)
        {
            var themePrompt = theme.ToLower() switch
            {
                "medieval" or "fantasy" => "medieval fantasy kingdom, castles on hills, knights and dragons, ancient parchment map style",
                "scifi" or "sci-fi" => "futuristic space colony, neon cities, alien planets, sleek spacecraft, cyberpunk aesthetic",
                "horror" => "dark gothic horror realm, haunted castles, foggy graveyards, twisted trees, eerie atmosphere",
                "steampunk" => "steampunk airship city, clockwork gears, Victorian industrial revolution with magic",
                "modern" => "modern urban fantasy city, magicalhidden in everyday, neon and magic combined",
                "pirate" => "pirate treasure island, sailing ships, tropical paradise, buried treasure map",
                "post-apocalyptic" => "wasteland survival world, ruined cities, survivors, salvage and hope",
                _ => "epic fantasy world map with kingdoms, castles, dragons, ancient forests, majestic mountains, medieval cartography"
            };
            
            var prompt = $"Professional RPG campaign logo for '{name}', {themePrompt}, " +
                        $"emblematic crest design, iconic memorable symbol, " +
                        $"high quality illustration, bold colors, clean composition, " +
                        $"suitable for branding and merchandise";
            
            return await GenerateImageAsync(prompt);
        }

        private static readonly Random _random = new();

        public async Task<string> GenerateRandomNameAsync(string entityType, string theme, string? context = null)
        {
            // Use Pollinations AI text for unlimited name generation
            var prompt = $"Generate a unique, creative {theme} fantasy {entityType} name. ";
            if (!string.IsNullOrEmpty(context))
                prompt += $"Context: {context}. ";
            prompt += "Return ONLY the name, no explanation. Make it memorable and fitting for a RPG.";

            try
            {
                var encodedPrompt = Uri.EscapeDataString(prompt);
                var response = await _httpClient.GetStringAsync($"https://text.pollinations.ai/{encodedPrompt}");
                var lines = response.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length > 0)
                {
                    var name = lines[0].Trim();
                    if (name.Length > 2 && name.Length < 60)
                        return name;
                }
            }
            catch { }

            return GetFallbackName(entityType, theme);
        }

        public async Task<string> GenerateDescriptionAsync(string entityType, string entityName, string? details = null)
        {
            var prompt = $"Generate a detailed, immersive {entityType} description for \"{entityName}\" in a fantasy RPG. ";
            if (!string.IsNullOrEmpty(details))
                prompt += $"Details: {details}. ";
            prompt += "Make it 2-3 sentences, atmospheric and evocative.";

            try
            {
                var encodedPrompt = Uri.EscapeDataString(prompt);
                var response = await _httpClient.GetStringAsync($"https://text.pollinations.ai/{encodedPrompt}");
                return response.Trim();
            }
            catch { }

            return GetFallbackDescription(entityType);
        }

        private string GetFallbackName(string entityType, string theme)
        {
            var prefixes = new[] { "The", "Ancient", "Lost", "Hidden", "Dark", "Golden", "Silver", "Frozen", "Burning", "Whispering", "Shadow", "Crystal", "Iron", "Storm", "Star", "Moon", "Sun", "Blood", "Bone", "Death" };
            var campaignNouns = new[] { "Kingdom", "Realm", "Empire", "Chronicles", "Saga", "Tales", "Quest", "Journey", "Legacy", "Prophecy", "Legend", "Myth", "Odyssey", "Epic", "Adventure", "War", "Peace", "Dawn", "Twilight", "Eternity" };
            var sessionNouns = new[] { "Beginning", "Discovery", "Confrontation", "Revelation", "Journey", "Trial", "Challenge", "Victory", "Defeat", "Turning Point", "Climax", "Resolution", "Mystery", "Encounter", "Battle" };
            var eventNouns = new[] { "Festival", "Ceremony", "Battle", "Ritual", "Gathering", "Council", "Wedding", "Funeral", "Tournament", "Market", "Invasion", "Alliance", "Treaty", "Eclipse", "Storm" };
            var locationNouns = new[] { "Citadel", "Temple", "Tavern", "Forest", "Cave", "Tower", "Keep", "Sanctuary", "Ruins", "Port", "Village", "City", "Dungeon", "Shrine", "Inn", "Palace", "Library", "Arena", "Graveyard", "Spring" };
            var suffixes = new[] { "of Shadows", "of Light", "of Dreams", "of Doom", "of Hope", "of Fear", "of Time", "of Eternity", "of the Ancients", "of the Lost", "of the Damned", "of the Forgotten", "of the Void", "of the Stars", "of the Moon" };

            var noun = entityType.ToLower() switch
            {
                "campaign" or "world" => campaignNouns[_random.Next(campaignNouns.Length)],
                "session" or "adventure" => sessionNouns[_random.Next(sessionNouns.Length)],
                "event" => eventNouns[_random.Next(eventNouns.Length)],
                "location" or "place" => locationNouns[_random.Next(locationNouns.Length)],
                _ => locationNouns[_random.Next(locationNouns.Length)]
            };

            var prefix = prefixes[_random.Next(prefixes.Length)];
            var suffix = _random.Next(3) == 0 ? suffixes[_random.Next(suffixes.Length)] : "";

            return $"{prefix} {noun} {suffix}".Trim();
        }

        private string GetFallbackDescription(string entityType)
        {
            var descriptions = new[]
            {
                "A mysterious place shrouded in legend and whispered tales.",
                "The air is thick with anticipation as secrets linger in every shadow.",
                "Ancient stones hold the memories of countless generations.",
                "A sense of wonder and danger permeates the very atmosphere.",
                "历史 is written in every corner of this legendary place."
            };
            return descriptions[_random.Next(descriptions.Length)];
        }

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
