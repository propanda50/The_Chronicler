using System.ComponentModel.DataAnnotations.Schema;

namespace TheChronicler.Web.Models
{
    public class SessionLocation
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; } = null!;

        public int LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; } = null!;
    }
}
