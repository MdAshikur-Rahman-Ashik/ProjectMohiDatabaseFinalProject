using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        // Navigation properties
        public virtual ICollection<TicketSupport> TicketSupports { get; set; }
        public virtual ICollection<TicketManagement> TicketManagements { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
