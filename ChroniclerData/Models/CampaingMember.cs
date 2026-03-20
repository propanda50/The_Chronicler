using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public enum Role
    {
        Player = 0,
        GameMaster = 1
    }
    public class CampaingMember
    {
        public int CampaignID { get; set; } 
        public int UserID { get; set; }
        public Campaign? Campaign { get; set; }
        public User? User { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.Now;  
        public bool CanAddNotes { get; set; }
    }
}
