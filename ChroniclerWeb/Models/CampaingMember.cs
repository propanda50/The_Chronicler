using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerWeb.Models
{
    public enum CampaignRole
    {
        GameMaster,
        Player
    }

    public class CampaignMember
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public Campaign Campaign { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public CampaignRole Role { get; set; } = CampaignRole.Player;

        public bool CanAddNotes { get; set; } = false;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
