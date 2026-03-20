using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public class Session
    {
        public int ID { get; set; }
        public int CampaignID { get; set; }
        public string Title { get; set; }
        public DateTime SessionDate { get; set; }
        public string Notes { get; set; }
        public bool IsKeySession { get; set; }
        public int CreatedBy { get; set; }
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
