using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ProjectMohiDatabase.Models
{
    public class TicketSupport
    {
        [Key]
        public int TicketSupportID { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(Package))]
        public int PackageID { get; set; }
        public virtual Package Package { get; set; }

        [ForeignKey(nameof(TicketStatus))]
        public int StatusID { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [ForeignKey(nameof(Priority))]
        public int PriorityID { get; set; }
        public virtual Priority Priority { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [StringLength(250)]
        public string Subject { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketSupportStatusHistory> TicketSupportStatusHistories { get; set; }
        public virtual ICollection<TicketManagement> TicketManagements { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
