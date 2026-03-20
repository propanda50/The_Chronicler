using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public class Location
    {
        public int ID { get; set; }
        public int CampaignID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Region { get; set; }
        public Campaign? Campaign { get; set; }
    }
}
