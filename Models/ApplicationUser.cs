using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TheChronicler.Web.Models
{
    public enum PlayStyle
    {
        Solo,
        LookingForPlayers,
        LookingForGameMaster,
        OrganizingGames,
        ReadyToPlay
    }

    public enum ExperienceLevel
    {
        Newbie,
        SomeExperience,
        Experienced
    }

    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? AvatarUrl { get; set; }

        public string? AvatarData { get; set; }

        public string? AvatarContentType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(10)]
        public string PreferredLanguage { get; set; } = "en";

        public PlayStyle? PlayStyle { get; set; }

        public ExperienceLevel? ExperienceLevel { get; set; }

        [MaxLength(1000)]
        public string? Bio { get; set; }

        public bool ProfileCompleted { get; set; } = false;

        public ICollection<Campaign> OwnedCampaigns { get; set; } = new List<Campaign>();
        public ICollection<CampaignMember> CampaignMemberships { get; set; } = new List<CampaignMember>();
    }
}
