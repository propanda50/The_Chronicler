using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public bool IsKeyEvent { get; set; } = false;

        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Campaign
        public int CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public Campaign Campaign { get; set; } = null!;

        // Associated Session (optional)
        public int? SessionId { get; set; }

        [ForeignKey(nameof(SessionId))]
        public Session? Session { get; set; }
    }
}
