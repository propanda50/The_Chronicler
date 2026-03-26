namespace TheChronicler.Web.Services
{
    public class RandomNameGenerator
    {
        private static readonly string[] FirstNames = {
            "Aldric", "Brenna", "Cedric", "Dara", "Eldric", "Freya", "Gareth", "Helena",
            "Idris", "Jasmine", "Kael", "Luna", "Magnus", "Nadia", "Orion", "Petra",
            "Quinn", "Rowan", "Soren", "Thalia", "Ulric", "Vera", "Wren", "Xander",
            "Yara", "Zephyr", "Aerith", "Bjorn", "Cassia", "Dorian", "Elena", "Finn",
            "Greta", "Hector", "Iris", "Jasper", "Kira", "Leon", "Mira", "Nikolai"
        };

        private static readonly string[] ElfFirstNames = {
            "Aelindra", "Caelum", "Eryndor", "Faelyn", "Galadriel", "Ilrath",
            "Laethiel", "Miriel", "Nimrodel", "Silvanus", "Thranduil", "Aelora", "Celeborn"
        };

        private static readonly string[] DwarfFirstNames = {
            "Balin", "Dwalin", "Fili", "Gimli", "Kili", "Thorin", "Durin", "Gloin",
            "Oin", "Bofur", "Bombur", "Nori", "Dori", "Ori", "Bifur"
        };

        private static readonly string[] LastNames = {
            "Blackwood", "Stormwind", "Ironforge", "Shadowmere", "Brightblade", "Darkhollow",
            "Frostborn", "Goldleaf", "Hillcrest", "Moonwhisper", "Riverdale", "Stoneheart",
            "Thornwood", "Whitecliff", "Ashenfall", "Braveheart", "Coldfire", "Dragonbane",
            "Emberforge", "Flameheart", "Grimwald", "Hawkwind", "Ivorypeak", "Jadefire"
        };

        private static readonly string[] ElfLastNames = {
            "Starweaver", "Moonbow", "Sunleaf", "Dawnstrider", "Nightbloom", "Windwalker"
        };

        private static readonly string[] DwarfLastNames = {
            "Stonehelm", "Ironfoot", "Anvilmar", "Deepdelver", "Forgefire", "Mountainheart"
        };

        private static readonly Random _random = new();

        public static string GenerateName(string race = "")
        {
            string firstName, lastName;

            switch (race.ToLower())
            {
                case "elf":
                case "elven":
                    firstName = ElfFirstNames[_random.Next(ElfFirstNames.Length)];
                    lastName = ElfLastNames[_random.Next(ElfLastNames.Length)];
                    break;
                case "dwarf":
                case "dwarven":
                    firstName = DwarfFirstNames[_random.Next(DwarfFirstNames.Length)];
                    lastName = DwarfLastNames[_random.Next(DwarfLastNames.Length)];
                    break;
                default:
                    firstName = FirstNames[_random.Next(FirstNames.Length)];
                    lastName = LastNames[_random.Next(LastNames.Length)];
                    break;
            }

            return $"{firstName} {lastName}";
        }

        public static string GeneratePlaceName()
        {
            string[] prefixes = {
                "Ash", "Black", "Cold", "Dark", "East", "Elder", "Fire", "Frost",
                "Golden", "Green", "Grey", "High", "Iron", "Long", "Misty", "Moon",
                "North", "Red", "River", "Shadow", "Silver", "Snow", "South", "Star",
                "Storm", "Sun", "Thunder", "Water", "West", "White", "Wild", "Wind"
            };

            string[] suffixes = {
                "barrow", "castle", "cave", "crossing", "dale", "fell", "ford", "gate",
                "haven", "hold", "keep", "lake", "march", "moor", "peak", "port",
                "reach", "ridge", "run", "shire", "spring", "stone", "tower", "vale"
            };

            return $"{prefixes[_random.Next(prefixes.Length)]}{suffixes[_random.Next(suffixes.Length)]}";
        }
    }
}
