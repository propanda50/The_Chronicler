using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TheChronicler.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Campaign> OwnedCampaigns { get; set; } = new List<Campaign>();
        public ICollection<CampaignMember> CampaignMemberships { get; set; } = new List<CampaignMember>();
    }
}
