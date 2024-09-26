using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class TicketSupportStatusHistory
    {
        public int TicketSupportStatusHistoryID { get; set; }
        [ForeignKey(nameof(TicketSupport))]
        public int TicketSupportID { get; set; }
        public virtual TicketSupport TicketSupport { get; set; }

        [ForeignKey(nameof(TicketStatus))]
        public int StatusID { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }

        
        public DateTime UpdatedAt { get; set; }
    }
}
