using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class Priority
    {
        [Key]
        public int PriorityID { get; set; }

        [StringLength(50)]
        public string PriorityName { get; set; }

        public virtual ICollection<TicketSupport> TicketSupports { get; set; }
    }
}
