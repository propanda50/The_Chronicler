using System.ComponentModel.DataAnnotations.Schema;

namespace TheChronicler.Web.Models
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
