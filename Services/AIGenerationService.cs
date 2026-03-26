using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace TheChronicler.Web.Services
{
    public interface IAIGenerationService
    {
        string GenerateRandomCharacterName(string race = "", string characterClass = "");
        string GenerateRandomCharacterPortrait(string race, string characterClass, string gender = "");
        string GenerateRandomCampaignLogo(string theme = "");
        string GenerateRandomClassName(string race = "");
        string GenerateRandomBackground();
        string GenerateRandomPersonalityTrait();
        string GetExtensiveFallbackName(string entityType, string theme);
    }

    public class AIGenerationService : IAIGenerationService
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
            // Use Pollinations AI for realistic, full body portraits based on race/class
            var raceLower = race?.ToLower() ?? "human";
            var classLower = characterClass?.ToLower() ?? "fighter";
            var genderLower = gender?.ToLower() ?? "";
            
            // Build detailed prompt for full body character portrait
            string poseAndStyle = "full body portrait, standing pose, detailed, RPG character art, fantasy artwork";
            string raceDescription = GetRaceDescription(raceLower);
            string classGear = GetClassGear(classLower);
            string genderDesc = GetGenderDescription(genderLower);
            
            var prompt = $"Fantasy RPG {genderDesc} {raceDescription} {classGear}, {poseAndStyle}, professional digital painting, intricate details, dramatic lighting, epic fantasy style, high quality, masterpiece";
            
            // Use Pollinations AI for image generation
            var encodedPrompt = Uri.EscapeDataString(prompt);
            return $"https://image.pollinations.ai/prompt/{encodedPrompt}?width=512&height=768&nologo=true&seed={_random.Next(10000)}";
        }

        private string GetRaceDescription(string race)
        {
            return race switch
            {
                "elf" or "elven" => "elegant elf with pointed ears, fair skin, long flowing hair",
                "dwarf" or "dwarven" => "stout dwarf with beard, rugged features, muscular build",
                "orc" or "half-orc" => "fierce orc with tusks, green or gray skin, intimidating presence",
                "halfling" or "hobbit" => "small halfling with curly hair, round friendly face",
                "tiefling" => "tiefling with horns, tail, crimson or purple skin, infernal features",
                "dragonborn" => "dragonborn with scales, draconic features, imposing stance",
                "gnome" => "small gnome with large eyes, eccentric appearance, magical aura",
                "half-elf" => "half-elf with elegant features, striking eyes, versatile appearance",
                _ => "human adventurer with detailed facial features"
            };
        }

        private string GetClassGear(string characterClass)
        {
            return characterClass switch
            {
                "fighter" or "warrior" or "knight" => "wearing armor, carrying sword and shield, battle-ready stance",
                "wizard" or "mage" or "sorcerer" => "clad in robes, carrying staff, mystical arcane symbols, magical aura",
                "rogue" or "assassin" or "thief" => "wearing leather armor, daggers, hooded cloak, stealthy appearance",
                "cleric" or "priest" => "in holy vestments, carrying holy symbol, divine aura",
                "paladin" or "holy" => "in plate armor, bearing holy sword, radiant divine glow",
                "ranger" or "hunter" => "in forest colors, bow and arrows, wilderness gear",
                "barbarian" or "berserker" => "muscular, minimal armor, dual wielding axes, fierce wild expression",
                "bard" or "performer" => "in colorful garb, musical instruments, charismatic presence",
                "druid" or "nature" => "in nature-themed clothing, wooden staff, earth tones, leaf crown",
                "monk" or "martial" => "in simple robes, unarmed combat stance, serene expression",
                "warlock" or "witch" => "in dark robes, dark magic artifacts, ominous presence",
                _ => "in adventuring gear, well-prepared for adventure"
            };
        }

        private string GetGenderDescription(string gender)
        {
            return gender.ToLower() switch
            {
                "male" or "man" or "masculine" => "male",
                "female" or "woman" or "feminine" => "female",
                _ => ""
            };
        }

        public string GenerateRandomCampaignLogo(string theme = "")
        {
            // Use Pollinations AI for campaign logos/covers
            var themeLower = theme?.ToLower() ?? "fantasy";
            
            var prompt = themeLower switch
            {
                "medieval" => "medieval fantasy map, castles, knights, rolling hills, ancient forests, detailed cartography style",
                "scifi" or "sci-fi" => "futuristic sci-fi world map, space stations, alien planets, neon cities, cyberpunk style",
                "horror" => "dark horror fantasy map, haunted lands, dark forests, eerie castles, gothic style",
                "steampunk" => "steampunk world map, airships, clockwork cities, industrial fantasy",
                "modern" => "modern fantasy world map, contemporary cities, urban fantasy atmosphere",
                _ => "fantasy world map with kingdoms, castles, mountains, forests, rivers, medieval cartography style, ancient parchment"
            };
            
            var encodedPrompt = Uri.EscapeDataString(prompt);
            return $"https://image.pollinations.ai/prompt/{encodedPrompt}?width=1024&height=512&nologo=true&seed={_random.Next(10000)}";
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

        public string GetExtensiveFallbackName(string entityType, string theme)
        {
            // Extensive fallback list with 100+ unique names
            var prefixes = new[] { "The", "Ancient", "Lost", "Hidden", "Dark", "Golden", "Silver", "Frozen", "Burning", "Whispering", "Shadow", "Crystal", "Iron", "Storm", "Star", "Moon", "Sun", "Blood", "Bone", "Death", "Eternal", "Forgotten", "Mystic", "Cursed", "Blessed", "Sacred", "Forbidden", "Endless", "Silent", "Eternal", "Divine", "Astral", "Celestial", "Demonic", "Infernal", "Hallowed", "Verdant", "Crimson", "Azure", "Obsidian", "Emerald", "Sapphire", "Amethyst", "Topaz", "Onyx", "Jade", "Pearl", "Amber", "Copper", "Bronze", "Platinum", "Mithril", "Adamantite" };
            
            var campaignNouns = new[] { "Kingdom", "Realm", "Empire", "Chronicles", "Saga", "Tales", "Quest", "Journey", "Legacy", "Prophecy", "Legend", "Myth", "Odyssey", "Epic", "Adventure", "War", "Peace", "Dawn", "Twilight", "Eternity", "Harmony", "Destiny", "Fate", "Glory", "Honor", "Triumph", "Victory", "Conquest", "Dominion", "Supremacy", "Ascension", "Renaissance", "Genesis", "Genesis", "Apocalypse", "Eruption", "Genesis", "Eternal", "Immortal", "Undying", "Infinite", "Boundless", "Timeless" };
            
            var sessionNouns = new[] { "Beginning", "Discovery", "Confrontation", "Revelation", "Journey", "Trial", "Challenge", "Victory", "Defeat", "Turning Point", "Climax", "Resolution", "Mystery", "Encounter", "Battle", "Awakening", "Emergence", "Transformation", "Culmination", "Finale", "Prologue", "Epilogue", "Interlude", "Crossroads", "Threshold", "Transition", "Metamorphosis", "Catalyst", "Inflection", "Pivotal", "Critical", "Crucial", "Decisive", "Momentous", "Historic", "Legendary", "Epic", "Mythic", "Fabled", "Sagas", "Chronicles", "Annals", "Records" };
            
            var eventNouns = new[] { "Festival", "Ceremony", "Battle", "Ritual", "Gathering", "Council", "Wedding", "Funeral", "Tournament", "Market", "Invasion", "Alliance", "Treaty", "Eclipse", "Storm", "Carnival", "Fair", "Celebration", "Riot", "Rebellion", "Revolution", "Coup", "Coronation", "Dethronement", "Ascension", "Inauguration", "Anniversary", "Milestone", "Threshold", "Convergence", "Divergence", "Alignment", "Solstice", "Equinox", "Eclipse", "Supernova", "Cataclysm", "Apocalypse" };
            
            var locationNouns = new[] { "Citadel", "Temple", "Tavern", "Forest", "Cave", "Tower", "Keep", "Sanctuary", "Ruins", "Port", "Village", "City", "Dungeon", "Shrine", "Inn", "Palace", "Library", "Arena", "Graveyard", "Spring", "Mount", "Valley", "Plain", "Desert", "Island", "Volcano", "Glacier", "Swamp", "Marsh", "Peak", "Canyon", "Gorge", "Waterfall", "Lake", "River", "Ocean", "Sea", "Cove", "Bay", "Peninsula", "Archipelago", "Fortress", "Bastion", "Stronghold", "Haven", "Refuge", "Retreat", "Asylum", "Sanctum", "Domain", "Territory", "Realm", "Province", "County", "Duchy", "Principality" };
            
            var suffixes = new[] { "of Shadows", "of Light", "of Dreams", "of Doom", "of Hope", "of Fear", "of Time", "of Eternity", "of the Ancients", "of the Lost", "of the Damned", "of the Forgotten", "of the Void", "of the Stars", "of the Moon", "of the Sun", "of the Dawn", "of the Dusk", "of the Night", "of the Day", "of Winter", "of Summer", "of Spring", "of Autumn", "of the East", "of the West", "of the North", "of the South", "of the Beyond", "of the Unknown", "of the Hidden", "of the Sacred", "of the Profane", "of the Mortal", "of the Divine", "of the Celestial", "of the Infernal" };

            var noun = entityType.ToLower() switch
            {
                "campaign" or "world" or "setting" => campaignNouns[_random.Next(campaignNouns.Length)],
                "session" or "adventure" or "chapter" => sessionNouns[_random.Next(sessionNouns.Length)],
                "event" or "happening" => eventNouns[_random.Next(eventNouns.Length)],
                "location" or "place" or "region" => locationNouns[_random.Next(locationNouns.Length)],
                _ => locationNouns[_random.Next(locationNouns.Length)]
            };

            // Randomly choose format: Prefix + Noun, Prefix + Noun + Suffix, or just Noun
            var format = _random.Next(4);
            string name;
            
            switch (format)
            {
                case 0:
                    name = $"{prefixes[_random.Next(prefixes.Length)]} {noun}";
                    break;
                case 1:
                    name = $"{prefixes[_random.Next(prefixes.Length)]} {noun} {suffixes[_random.Next(suffixes.Length)]}";
                    break;
                case 2:
                    name = $"{noun} {suffixes[_random.Next(suffixes.Length)]}";
                    break;
                default:
                    name = noun;
                    break;
            }

            return name.Trim();
        }
    }
}