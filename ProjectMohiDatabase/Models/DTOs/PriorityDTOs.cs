using System.ComponentModel.DataAnnotations;

namespace ProjectMohiDatabase.Models.DTOs
{
    public class PriorityDTOs
    {
        public int PriorityID { get; set; }

        [StringLength(50)]
        public string PriorityName { get; set; }
    }
}
