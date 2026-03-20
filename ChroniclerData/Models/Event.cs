using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public class Event
    {
        public int ID { get; set; }
        public int CampaignID { get; set; }
        public int SessionID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int EventOrder { get; set; }
    }
}
