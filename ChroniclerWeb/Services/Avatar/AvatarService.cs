namespace ChroniclerWeb.Services.Avatar
{
    public class AvatarService : IAvatarService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly List<string> _defaultAvatars;
        private readonly IConfiguration _configuration;

        public AvatarService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
            _defaultAvatars = GetDefaultAvatarList();
        }

        public IEnumerable<string> GetDefaultAvatars()
        {
            return _defaultAvatars;
        }

        public string GetRandomDefaultAvatar()
        {
            var random = new Random();
            return _defaultAvatars[random.Next(_defaultAvatars.Count)];
        }

        private List<string> GetDefaultAvatarList()
        {
            // Return a list of 30 default avatar SVG data URIs
            // Each represents a different fantasy-themed avatar
            var avatars = new List<string>();

            // Fantasy-themed avatar designs using SVG
            for (int i = 1; i <= 30; i++)
            {
                // Create different colored circle backgrounds with symbols
                string symbol = GetSymbolForIndex(i);
                string color = GetColorForIndex(i);
                string svg = $@"<svg xmlns='http://www.w3.org/2000/svg' width='200' height='200'>
                    <circle cx='100' cy='100' r='90' fill='{color}'/>
                    <text x='100' y='115' font-family='Arial, sans-serif' font-size='60' fill='white' text-anchor='middle'>{symbol}</text>
                </svg>";

                // Convert SVG to data URI
                string encodedSvg = Uri.EscapeDataString(svg);
                avatars.Add($"data:image/svg+xml;base64,{Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(svg))}");
            }

            return avatars;
        }

        private string GetSymbolForIndex(int index)
        {
            string[] symbols = { "🗡️", "⚔️", "🛡️", "👑", "🧙‍♂️", "🧝‍♀️", "🧟‍♂️", "🧛‍♀️", "🐉", "🦄",
                               "🔮", "🧪", "⚖️", "🎭", "🪄", "🕯️", "📜", "🪙", "🔑", "🪓",
                               "🪃", "🛶", "🪓", "🏹", "🪓", "🛡️", "⚕️", "⚗️", "🪔", "🪕" };
            return symbols[index % symbols.Length];
        }

        private string GetColorForIndex(int index)
        {
            string[] colors = { "#8b5cf6", "#7c3aed", "#6d28d9", "#5b21b6", "#4c1d95",
                               "#06b6d4", "#0891b2", "#0e7490", "#155e75", "#164e63",
                               "#d97706", "#b45309", "#92400e", "#78350f", "#652d0a",
                               "#10b981", "#059669", "#047857", "#065f46", "#064e3b",
                               "#ef4444", "#dc2626", "#b91c1c", "#991b1b", "#7f1d1d",
                               "#f59e0b", "#d97706", "#b45309", "#92400e", "#78350f" };
            return colors[index % colors.Length];
        }

        public bool IsDefaultAvatar(string avatarPath)
        {
            return avatarPath.StartsWith("data:image/svg+xml;base64,");
        }

        public string GetAvatarUrl(string avatarIdentifier)
        {
            if (string.IsNullOrEmpty(avatarIdentifier))
                return null;

            // If it's already a full URL or data URI, return as-is
            if (avatarIdentifier.StartsWith("http://") ||
                avatarIdentifier.StartsWith("https://") ||
                avatarIdentifier.StartsWith("data:"))
                return avatarIdentifier;

            // Otherwise, treat it as a path relative to wwwroot
            return $"/{avatarIdentifier.TrimStart('/')}";
        }
    }
}
