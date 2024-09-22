using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models
{
    public class Package
    {
        [Key]
        public int PackageID { get; set; }

        [StringLength(100)]
        public string PackageName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        // Navigation properties
        public virtual ICollection<TicketSupport> TicketSupports { get; set; }
    }
}
