using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public enum CampaignStatus
    {
        Active = 0,
        Paused = 1,
        Completed = 2
    }
    public class Campaign
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public CampaignStatus Status { get; set; }
        public int OwnerID { get; set; }
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
