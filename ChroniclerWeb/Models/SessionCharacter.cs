using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public class SessionCharacter
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; } = null!;

        public int CharacterId { get; set; }

        [ForeignKey(nameof(CharacterId))]
        public Character Character { get; set; } = null!;
    }
}
