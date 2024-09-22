using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ProjectMohiDatabase.Models
{
    public class TicketManagement
    {
        [Key]
        public int TicketManagementID { get; set; }

        [ForeignKey(nameof(TicketSupport))]
        public int TicketSupportID { get; set; }
        public virtual TicketSupport TicketSupport { get; set; }

        [StringLength(250)]
        public string AssignedTo { get; set; }

        [ForeignKey(nameof(Person))]
        public int ManagedByPersonID { get; set; }
        public virtual Person Person { get; set; }
    }
}
