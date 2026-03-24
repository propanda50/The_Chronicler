using System.Text;
using System.Text.Json;

namespace ChroniclerWeb.Services.AIGeneration
{
    public class AIGenerationService :  IAIGenerationService
    {
        private static readonly Random _random = new();
        private readonly IConfiguration _configuration;

        public AIGenerationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static readonly string[] HumanNames = {
            "Aldric", "Brenna", "Cedric", "Dara", "Eldric", "Freya", "Gareth", "Helena",
            "Idris", "Jasmine", "Kael", "Luna", "Magnus", "Nadia", "Orion", "Petra",
            "Quinn", "Rowan", "Soren", "Thalia", "Ulric", "Vera", "Wren", "Xander"
        };

        private static readonly string[] ElfNames = {
            "Aelindra", "Caelum", "Eryndor", "Faelyn", "Galadriel", "Ilrath",
            "Laethiel", "Miriel", "Nimrodel", "Silvanus", "Thranduil"
        };

        private static readonly string[] DwarfNames = {
            "Balin", "Dwalin", "Fili", "Gimli", "Kili", "Thorin", "Durin", "Gloin",
            "Oin", "Bofur", "Bombur", "Nori", "Dori", "Ori", "Bifur"
        };

        private static readonly string[] OrcNames = {
            "Grom", "Thrash", "Ugluk", "Mogul", "Gashnak", "Grishnak", "Lurtz",
            "Bolg", "Azog", "Boldog", "Golfimbul", "Radbug"
        };

        private static readonly string[] LastNames = {
            "Blackwood", "Stormwind", "Ironforge", "Shadowmere", "Brightblade", "Darkhollow",
            "Frostborn", "Goldleaf", "Hillcrest", "Moonwhisper", "Riverdale", "Stoneheart"
        };

        private static readonly string[] Classes = {
            "Fighter", "Wizard", "Rogue", "Cleric", "Paladin", "Ranger", "Barbarian",
            "Bard", "Druid", "Monk", "Sorcerer", "Warlock"
        };

        private static readonly string[] Backgrounds = {
            "Acolyte", "Charlatan", "Criminal", "Entertainer", "Folk Hero", "Guild Artisan",
            "Hermit", "Noble", "Outlander", "Sage", "Sailor", "Soldier", "Urchin"
        };

        private static readonly string[] PersonalityTraits = {
            "I am always calm, no matter what the situation. I raise my voice only when people won't listen.",
            "I judge others by their actions, not their words.",
            "I feel that emotions overrule logic.",
            "I take great pains to always look my best and follow the latest fashions.",
            "My favor, once lost, is lost forever.",
            "I put my dead relatives in a good light.",
            "I am a night owl.",
            "I am a dreamer who often lets my thoughts wander.",
            "I enjoy being clean and tidy.",
            "I have a hearty laugh."
        };

        public string GenerateRandomCharacterName(string race = "", string characterClass = "")
        {
            // Try to use Ollama for name generation first
            string aiGeneratedName = TryOllamaNameGeneration(race, characterClass);

            // If Ollama fails, fall back to the traditional method
            if (!string.IsNullOrEmpty(aiGeneratedName))
            {
                return aiGeneratedName;
            }

            // Fallback to traditional random name generation
            string firstName;
            string lastName = LastNames[_random.Next(LastNames.Length)];

            switch (race.ToLower())
            {
                case "elf":
                case "elven":
                    firstName = ElfNames[_random.Next(ElfNames.Length)];
                    break;
                case "dwarf":
                case "dwarven":
                    firstName = DwarfNames[_random.Next(DwarfNames.Length)];
                    break;
                case "orc":
                case "orcish":
                    firstName = OrcNames[_random.Next(OrcNames.Length)];
                    break;
                default:
                    firstName = HumanNames[_random.Next(HumanNames.Length)];
                    break;
            }

            return $"{firstName} {lastName}";
        }

        private string TryOllamaNameGeneration(string race, string characterClass)
        {
            try
            {
                // Construct a prompt for Ollama to generate a fantasy character name
                string prompt = $"Generate a fantasy {race} {characterClass} name suitable for a tabletop RPG character. Return only the name, nothing else.";

                // Use the same Ollama configuration as other services
                var ollamaEndpoint = _configuration["Ollama:Endpoint"] ?? "http://localhost:11434";
                var ollamaModel = _configuration["Ollama:Model"] ?? "llama3.2";

                var ollamaRequest = new
                {
                    model = ollamaModel,
                    prompt = prompt,
                    stream = false
                };

                using var httpClient = new HttpClient();
                var requestJson = JsonSerializer.Serialize(ollamaRequest);
                var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                using var cts = new System.Threading.CancellationTokenSource(System.TimeSpan.FromSeconds(10));
                var response = httpClient.PostAsync($"{ollamaEndpoint}/api/generate", content, cts.Token).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Parse the Ollama response to extract the generated text
                    using var jsonDoc = JsonDocument.Parse(result);
                    var textResponse = jsonDoc.RootElement.GetProperty("response").GetString();

                    // Clean up the response - just take the first line/word as the name
                    var cleanedResponse = textResponse.Trim();
                    if (!string.IsNullOrEmpty(cleanedResponse))
                    {
                        // Take first line or first word as the name
                        var nameParts = cleanedResponse.Split(new[] { '\n', ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);
                        if (nameParts.Length > 0)
                        {
                            return nameParts[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error but fall back to traditional method
                // In a real implementation, you'd use proper logging
                System.Diagnostics.Debug.WriteLine($"Ollama name generation failed: {ex.Message}");
            }

            // Return empty string to indicate failure and trigger fallback
            return string.Empty;
        }

        public string GenerateRandomCharacterPortrait(string race, string characterClass, string gender = "")
        {
            // Generate a random portrait URL using placeholder services
            // In production, this could call an AI image generation API
            var seed = _random.Next(100000);
            var style = "adventurer"; // Art style
            return $"https://api.dicebear.com/7.x/{style}/svg?seed={seed}&backgroundColor=b6e3f4";
        }

        public string GenerateRandomCampaignLogo(string theme = "")
        {
            var seed = _random.Next(100000);
            return $"https://api.dicebear.com/7.x/shapes/svg?seed={seed}&backgroundColor=c0aede";
        }

        public string GenerateRandomClassName(string race = "")
        {
            return Classes[_random.Next(Classes.Length)];
        }

        public string GenerateRandomBackground()
        {
            return Backgrounds[_random.Next(Backgrounds.Length)];
        }

        public string GenerateRandomPersonalityTrait()
        {
            return PersonalityTraits[_random.Next(PersonalityTraits.Length)];
        }
    }
}
