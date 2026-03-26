using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Web;

namespace TheChronicler.Web.Services
{
    public interface IAIConversationService
    {
        Task<AIResponse> SendMessageAsync(string message, string? userId = null, int? campaignId = null);
        Task<string> GenerateImageAsync(string prompt, string context = "");
    }

    public class AIConversationService : IAIConversationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AIConversationService> _logger;
        private readonly Random _random = new();
        private readonly string _ollamaEndpoint;

        public AIConversationService(HttpClient httpClient, ILogger<AIConversationService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _ollamaEndpoint = configuration["Ollama:Endpoint"] ?? "http://localhost:11434";
        }

        public async Task<AIResponse> SendMessageAsync(string message, string? userId = null, int? campaignId = null)
        {
            var lowerMessage = message.ToLowerInvariant().Trim();

            // Check for image generation requests (in any language)
            if (IsImageGenerationRequest(lowerMessage))
            {
                return await HandleImageGenerationRequest(lowerMessage, message);
            }

            // Check for character creation requests
            if (IsCharacterCreationRequest(lowerMessage))
            {
                return await HandleCharacterCreation(lowerMessage, campaignId);
            }

            // Check for campaign creation requests
            if (IsCampaignCreationRequest(lowerMessage))
            {
                return new AIResponse
                {
                    Response = GetLocalizedResponse("campaign_create", message),
                    Action = "createCampaign"
                };
            }

            // Check for location creation requests
            if (IsLocationCreationRequest(lowerMessage))
            {
                return await HandleLocationCreation(lowerMessage, campaignId);
            }

            // Check for name generation requests
            if (IsNameGenerationRequest(lowerMessage))
            {
                var name = GenerateRandomName(lowerMessage);
                return new AIResponse
                {
                    Response = GetLocalizedResponse("name_generated", message).Replace("{name}", name)
                };
            }

            // Check for help requests
            if (IsHelpRequest(lowerMessage))
            {
                return new AIResponse
                {
                    Response = GetHelpResponse(message)
                };
            }

            // Check for greetings
            if (IsGreeting(lowerMessage))
            {
                return new AIResponse
                {
                    Response = GetGreetingResponse(message)
                };
            }

            // Check for thanks
            if (IsThanks(lowerMessage))
            {
                return new AIResponse
                {
                    Response = GetThanksResponse(message)
                };
            }

            // Use AI for conversational responses
            return await GetAIConversationResponse(message);
        }

        public async Task<string> GenerateImageAsync(string prompt, string context = "")
        {
            try
            {
                var fullPrompt = string.IsNullOrEmpty(context) 
                    ? prompt 
                    : $"{prompt}, {context}, fantasy RPG style, high quality, detailed";
                
                // Try to use Ollama for image generation first (if a vision model is available)
                string imageUrl = await TryOllamaImageGeneration(fullPrompt);
                
                // If Ollama fails or isn't configured for image generation, fall back to Pollinations
                if (string.IsNullOrEmpty(imageUrl))
                {
                    var encodedPrompt = HttpUtility.UrlEncode(fullPrompt);
                    var seed = _random.Next(1000000);
                    imageUrl = $"https://image.pollinations.ai/prompt/{encodedPrompt}?width=1024&height=1024&seed={seed}&nologo=true&enhance=true";
                    
                    _logger.LogInformation("Generated image URL via Pollinations: {ImageUrl}", imageUrl);
                }
                else
                {
                    _logger.LogInformation("Generated image URL via Ollama: {ImageUrl}", imageUrl);
                }
                
                // Just return the URL - the browser will load it
                // No need to make a request to it
                return await Task.FromResult(imageUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Image generation failed: {Message}", ex.Message);
                return "";
            }
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

        private async Task<AIResponse> GetAIConversationResponse(string message)
        {
            try
            {
                var systemPrompt = @"You are The Chronicler AI, an all-knowing assistant for a tabletop RPG campaign management platform. You know EVERYTHING about this application and can help users with ANY question or task.

## WHAT THE CHRONICLER DOES:
The Chronicler is a Campaign Intelligence System for tabletop RPG players. It helps manage:
- **Campaigns**: Create and manage RPG campaigns (D&D, Pathfinder, etc.)
- **Sessions**: Log game sessions with notes and summaries
- **Characters**: Create player characters and NPCs with portraits
- **Locations**: Track places in your world (cities, dungeons, taverns)
- **Events**: Record important story events
- **Timeline**: Visual timeline of campaign events
- **Forum**: Discussion boards for each campaign
- **AI Assistant**: Integrated AI to help with everything
- **Image Generation**: Generate character portraits and campaign covers with AI

## HOW TO HELP USERS:
1. Answer questions about ANY feature of the app
2. Guide users through creating characters, campaigns, etc.
3. Explain tabletop RPG concepts
4. Help with game mechanics questions
5. Generate names, backstories, descriptions
6. Explain how to use specific features
7. Troubleshoot common issues

## SAFETY RULES:
You MUST refuse to help with:
- Software cracks, piracy, or illegal downloads
- Hate speech, harassment, or harmful content
- Content that could harm the creators or platform

Otherwise, you help with EVERYTHING else!

## RESPONSE STYLE:
- Be friendly and enthusiastic
- Use the same language as the user (French, English, Dutch, etc.)
- Be concise but helpful
- Use emojis sparingly for visual interest
- If unsure, ask for clarification

Current user message: " + message;

                // Use Ollama for text generation
                var ollamaRequest = new
                {
                    model = "llama3.2", // You can configure this
                    prompt = systemPrompt,
                    stream = false
                };

                var requestJson = JsonSerializer.Serialize(ollamaRequest);
                var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(45));
                var response = await _httpClient.PostAsync($"{_ollamaEndpoint}/api/generate", content, cts.Token);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // Parse the Ollama response to extract the generated text
                    using var jsonDoc = JsonDocument.Parse(result);
                    var textResponse = jsonDoc.RootElement.GetProperty("response").GetString();
                    return new AIResponse { Response = textResponse.Trim() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "AI conversation failed, using fallback");
            }

            // Fallback response
            return new AIResponse
            {
                Response = GetFallbackResponse(message)
            };
        }

        private async Task<AIResponse> HandleImageGenerationRequest(string lowerMessage, string originalMessage)
        {
            string imageType;
            string prompt;

            if (lowerMessage.Contains("character") || lowerMessage.Contains("personnage") || lowerMessage.Contains("portrait"))
            {
                imageType = "character portrait";
                prompt = "fantasy RPG character portrait, detailed digital art, dramatic lighting, professional artwork";
            }
            else if (lowerMessage.Contains("campaign") || lowerMessage.Contains("campagne") || lowerMessage.Contains("cover"))
            {
                imageType = "campaign cover";
                prompt = "fantasy RPG campaign cover art, epic scene, medieval fantasy, professional digital art";
            }
            else if (lowerMessage.Contains("location") || lowerMessage.Contains("lieu"))
            {
                imageType = "location illustration";
                prompt = "fantasy RPG location illustration, detailed environment art, atmospheric";
            }
            else if (lowerMessage.Contains("event") || lowerMessage.Contains("événement"))
            {
                imageType = "event illustration";
                prompt = "fantasy RPG event illustration, dramatic scene, cinematic composition";
            }
            else
            {
                imageType = "fantasy illustration";
                prompt = "fantasy RPG illustration, detailed digital art, magical atmosphere";
            }

            var imageUrl = await GenerateImageAsync(prompt, originalMessage);
            
            if (!string.IsNullOrEmpty(imageUrl))
            {
                return new AIResponse
                {
                    Response = $"Voici une {imageType} générée pour toi! Clique sur l'image pour l'agrandir.",
                    ImageUrl = imageUrl
                };
            }

            return new AIResponse
            {
                Response = "Désolé, je n'ai pas pu générer l'image. Réessaye dans quelques instants."
            };
        }

        private async Task<AIResponse> HandleCharacterCreation(string lowerMessage, int? campaignId)
        {
            if (!campaignId.HasValue)
            {
                return new AIResponse
                {
                    Response = "Pour créer un personnage, tu dois d'abord être dans une campagne. Veux-tu créer une campagne? Dis 'créer une campagne'."
                };
            }

            var race = DetectRace(lowerMessage);
            var charClass = DetectClass(lowerMessage);
            var gender = DetectGender(lowerMessage);
            var name = GenerateRandomNameForRace(race);

            return new AIResponse
            {
                Response = $"Parfait! Je vais créer un personnage {race} {charClass} nommé {name}. Je te redirige vers la page de création...",
                Action = "createCharacter",
                Data = new { campaignId = campaignId.Value, name, race, @class = charClass }
            };
        }

        private async Task<AIResponse> HandleLocationCreation(string lowerMessage, int? campaignId)
        {
            if (!campaignId.HasValue)
            {
                return new AIResponse
                {
                    Response = "Pour créer un lieu, tu dois d'abord être dans une campagne."
                };
            }

            var name = GeneratePlaceName();
            return new AIResponse
            {
                Response = $"Je vais créer un lieu nommé '{name}'. C'est parti!",
                Action = "createLocation",
                Data = new { campaignId = campaignId.Value, name }
            };
        }

        private bool IsImageGenerationRequest(string message)
        {
            var keywords = new[] { "image", "générer", "generate", " créer une image", "picture", "photo", "portrait", "cover", "illustration", "dessin", "art" };
            return keywords.Any(k => message.Contains(k));
        }

        private bool IsCharacterCreationRequest(string message)
        {
            var keywords = new[] { "character", "personnage", "créer un personnage", "create character", "nouveau personnage", "new character" };
            return keywords.Any(k => message.Contains(k));
        }

        private bool IsCampaignCreationRequest(string message)
        {
            var keywords = new[] { "campaign", "campagne", "créer une campagne", "create campaign", "nouvelle campagne", "new campaign" };
            return keywords.Any(k => message.Contains(k));
        }

        private bool IsLocationCreationRequest(string message)
        {
            var keywords = new[] { "location", "lieu", "créer un lieu", "create location", "nouveau lieu", "new location" };
            return keywords.Any(k => message.Contains(k));
        }

        private bool IsNameGenerationRequest(string message)
        {
            var keywords = new[] { "name", "nom", "générer un nom", "generate name", "random name", "nom aléatoire" };
            return keywords.Any(k => message.Contains(k));
        }

        private bool IsHelpRequest(string message)
        {
            var keywords = new[] { "help", "aide", "comment", "how", "what can you do", "que peux-tu faire", "quoi faire" };
            return keywords.Any(k => message.Contains(k));
        }

        private bool IsGreeting(string message)
        {
            var keywords = new[] { "hello", "hi", "salut", "bonjour", "coucou", "hey", "bonsoir", "yo" };
            return keywords.Any(k => message.Contains(k));
        }

        private bool IsThanks(string message)
        {
            var keywords = new[] { "thanks", "thank", "merci", "thanks", "ty", "thx" };
            return keywords.Any(k => message.Contains(k));
        }

        private string DetectRace(string message)
        {
            if (message.Contains("elf") || message.Contains("elfe")) return "Elf";
            if (message.Contains("dwarf") || message.Contains("nain")) return "Dwarf";
            if (message.Contains("orc")) return "Orc";
            if (message.Contains("dragon") || message.Contains("dracon")) return "Dragonborn";
            if (message.Contains("half") || message.Contains("demi")) return "Half-Elf";
            return "Human";
        }

        private string DetectClass(string message)
        {
            if (message.Contains("fighter") || message.Contains("guerrier")) return "Fighter";
            if (message.Contains("wizard") || message.Contains("mage") || message.Contains("sorcier")) return "Wizard";
            if (message.Contains("rogue") || message.Contains("voleur")) return "Rogue";
            if (message.Contains("cleric") || message.Contains("prêtre") || message.Contains("pretre")) return "Cleric";
            if (message.Contains("paladin")) return "Paladin";
            if (message.Contains("ranger")) return "Ranger";
            if (message.Contains("barbarian") || message.Contains("barbare")) return "Barbarian";
            if (message.Contains("bard") || message.Contains("barde")) return "Bard";
            if (message.Contains("druid") || message.Contains("druide")) return "Druid";
            return "Fighter";
        }

        private string DetectGender(string message)
        {
            if (message.Contains("female") || message.Contains("femme") || message.Contains("elle")) return "Female";
            return "Male";
        }

        private string GenerateRandomName(string message)
        {
            var names = new[] { "Aldric", "Brenna", "Cedric", "Dara", "Eldric", "Freya", "Gareth", "Helena", "Idris", "Jasmine", "Kael", "Luna", "Magnus", "Nadia", "Orion", "Petra" };
            return names[_random.Next(names.Length)];
        }

        private string GenerateRandomNameForRace(string race)
        {
            string[] names = race switch
            {
                "Elf" => new[] { "Aelindra", "Caelum", "Eryndor", "Faelyn", "Galadriel", "Ilrath" },
                "Dwarf" => new[] { "Balin", "Dwalin", "Fili", "Gimli", "Kili", "Thorin" },
                "Orc" => new[] { "Grom", "Thrash", "Ugluk", "Mogul", "Gashnak" },
                _ => new[] { "Aldric", "Brenna", "Cedric", "Dara", "Eldric", "Freya" }
            };
            return names[_random.Next(names.Length)];
        }

        private string GeneratePlaceName()
        {
            var prefixes = new[] { "Dark", "Silver", "Golden", "Shadow", "Crystal", "Storm", "Iron", "Dragon" };
            var suffixes = new[] { "wood", "hold", "vale", "keep", "reach", "haven", "fell", "watch" };
            return prefixes[_random.Next(prefixes.Length)] + suffixes[_random.Next(suffixes.Length)];
        }

        private string GetLocalizedResponse(string type, string originalMessage)
        {
            // Detect if user is likely French
            var isFrench = originalMessage.Any(c => "àâäéèêëïîôùûüÿçœæ".Contains(char.ToLower(c))) ||
                          new[] { "le ", "la ", "les ", "un ", "une ", "des ", "du ", "je ", "tu ", "nous ", "vous ", "ils ", "elles " }
                          .Any(w => originalMessage.ToLower().Contains(w));

            return type switch
            {
                "campaign_create" => isFrench 
                    ? "Parfait! Je vais te rediriger vers la page de création de campagne. Tu pourras y configurer tous les détails!"
                    : "Perfect! I'll redirect you to the campaign creation page.",
                "name_generated" => isFrench
                    ? "Voici un nom généré: {name}. Veux-tu que je génère un autre?"
                    : "Here's a generated name: {name}. Want another one?",
                _ => "Comment puis-je t'aider?"
            };
        }

        private string GetHelpResponse(string message)
        {
            var isFrench = message.Any(c => "àâäéèêëïîôùûüÿçœæ".Contains(char.ToLower(c)));
            
            return isFrench
                ? @"Je peux t'aider avec:
• Créer des personnages (dis 'créer un personnage')
• Créer des campagnes (dis 'créer une campagne')
• Créer des lieux (dis 'créer un lieu')
• Générer des noms (dis 'générer un nom')
• Générer des images (dis 'générer une image')
• Répondre à tes questions sur les JDR

Tu peux aussi juste discuter avec moi!"
                : @"I can help you with:
• Creating characters (say 'create character')
• Creating campaigns (say 'create campaign')
• Creating locations (say 'create location')
• Generating names (say 'generate name')
• Generating images (say 'generate image')
• Answering questions about RPGs

You can also just chat with me!";
        }

        private string GetGreetingResponse(string message)
        {
            var isFrench = message.Contains("salut") || message.Contains("bonjour") || message.Contains("coucou") || message.Contains("bonsoir");
            
            var greetings = isFrench
                ? new[] { "Salut! Comment puis-je t'aider aujourd'hui?", "Bonjour! Que puis-je faire pour toi?", "Hey! Ravi de te voir!" }
                : new[] { "Hey there! How can I help you today?", "Hello! What can I do for you?", "Hi! Great to see you!" };
            
            return greetings[_random.Next(greetings.Length)];
        }

        private string GetThanksResponse(string message)
        {
            var isFrench = message.Contains("merci");
            return isFrench 
                ? "De rien! N'hésite pas si tu as d'autres questions!" 
                : "You're welcome! Feel free to ask if you need anything else!";
        }

        private string GetFallbackResponse(string message)
        {
            var isFrench = message.Any(c => "àâäéèêëïîôùûüÿçœæ".Contains(char.ToLower(c)));
            
            return isFrench
                ? "Je ne suis pas sûr de comprendre. Tu peux essayer de reformuler ou demander 'aide' pour voir ce que je peux faire!"
                : "I'm not sure I understand. Try rephrasing or ask 'help' to see what I can do!";
        }
    }

    public class AIResponse
    {
        public string Response { get; set; } = "";
        public string? Action { get; set; }
        public object? Data { get; set; }
        public string? ImageUrl { get; set; }
    }
}