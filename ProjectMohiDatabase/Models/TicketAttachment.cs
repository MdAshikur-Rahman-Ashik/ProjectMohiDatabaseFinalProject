using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class TicketAttachment
    {
        [Key]
        public int TicketAttachID { get; set; }

        [ForeignKey(nameof(TicketSupport))]
        public int TicketSupportID { get; set; }
        public virtual TicketSupport TicketSupport { get; set; }

        public string AttachFile { get; set; }
    }
}
