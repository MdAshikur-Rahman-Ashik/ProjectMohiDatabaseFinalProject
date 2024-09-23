using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public virtual ICollection<TicketSupport> TicketSupports { get; set; }
        
    }
}
