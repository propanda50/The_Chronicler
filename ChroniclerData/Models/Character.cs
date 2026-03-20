using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public enum Status
    {
        Alive = 0,
        Dead = 1,
        Missing = 2,
        Unknown = 3
    }
    public class Character
    {
        public int ID { get; set; }
        public int CampaignID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Role { get; set; }
        public Status Status { get; set; }
        public bool IsNPC { get; set; }
        public Campaign? Campaign { get; set; }
    }
}
