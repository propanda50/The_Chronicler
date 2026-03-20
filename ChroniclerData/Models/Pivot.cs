using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public enum Type
    {
        Character = 0,
        Location = 1,
        Event = 2,
    }
    public class Pivot //SessionEntities
    {
        public int SessionID { get; set; }
        public Type EntityType { get; set; }
        public int EntityID { get; set; }
        public Session? Session { get; set; }
    }
}
