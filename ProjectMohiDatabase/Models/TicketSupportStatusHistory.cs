using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class TicketSupportStatusHistory
    {
        [ForeignKey(nameof(TicketSupport))]
        public int TicketSupportID { get; set; }
        public virtual TicketSupport TicketSupport { get; set; }

        [ForeignKey(nameof(TicketStatus))]
        public int StatusID { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }

        [Key, Column(Order = 0)]
        public DateTime UpdatedAt { get; set; }
    }
}
