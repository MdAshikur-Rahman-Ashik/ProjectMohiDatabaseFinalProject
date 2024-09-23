using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ProjectMohiDatabase.Models
{
    public class Reply
    {
        [Key]
        public int ReplyID { get; set; }

        [ForeignKey(nameof(TicketSupport))]
        public int TicketSupportID { get; set; }
        public virtual TicketSupport TicketSupport { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser  ApplicationUser { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<ReplyAttachment> ReplyAttachments { get; set; }
    }
}
