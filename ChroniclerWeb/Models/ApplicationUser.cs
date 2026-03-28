using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerWeb.Models
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
        [Required]
        [MaxLength(50)]
        public string Pseudo { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string NormalizedPseudo { get; set; } = string.Empty;

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
