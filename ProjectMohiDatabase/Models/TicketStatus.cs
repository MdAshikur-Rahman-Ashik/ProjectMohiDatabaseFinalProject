using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class TicketStatus
    {
        [Key]
        public int StatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        public virtual ICollection<TicketSupport> TicketSupports { get; set; }
        public virtual ICollection<TicketSupportStatusHistory> TicketSupportStatusHistories { get; set; } 
    }
}
